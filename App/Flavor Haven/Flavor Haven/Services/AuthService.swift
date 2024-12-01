import Foundation
import SwiftJWT

struct LoginRequest: Codable {
    let userName: String
    let password: String
}

struct LoginResponse: Codable {
    let accessToken: String
    let refreshToken: String
}

struct RegisterRequest: Codable {
    let userName: String
    let email: String
    let password: String
}

struct RegisterSuccessResponse: Codable {
    let accessToken: String
    let refreshToken: String
}

struct UserClaims: Claims {
    let nameidentifier: String
    let name: String
    let role: String
    let exp: Int
    let iss: String
    let aud: String

    enum CodingKeys: String, CodingKey {
        case nameidentifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
        case name = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
        case role = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        case exp, iss, aud
    }
}

enum RegisterErrorType {
    case success
    case invalidPassword
    case invalidEmail
    case userExists
    case unknown
}

class AuthService {
    static let shared = AuthService()
    private init() {}

    private let loginURL = "http://localhost:8081/api/auth/login"

    func login(userName: String, password: String, completion: @escaping (Result<LoginResponse, Error>) -> Void) {
        guard let url = URL(string: loginURL) else {
            print("Invalid login URL")
            return
        }

        var request = URLRequest(url: url)
        request.httpMethod = "POST"
        request.setValue("application/json", forHTTPHeaderField: "Content-Type")

        let loginData = LoginRequest(userName: userName, password: password)
        do {
            request.httpBody = try JSONEncoder().encode(loginData)
        } catch {
            print("Failed to encode login data: \(error)")
            completion(.failure(error))
            return
        }

        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            if let error = error {
                completion(.failure(error))
                return
            }

            guard let data = data else {
                print("No data received from server")
                return
            }

            do {
                let loginResponse = try JSONDecoder().decode(LoginResponse.self, from: data)
                completion(.success(loginResponse))
            } catch {
                print("Failed to decode response: \(error)")
                completion(.failure(error))
            }
        }

        task.resume()
    }
}

func decodeUserClaims() {
    if let jwtToken = TokenManager.shared.loadToken(key: "accessToken") {
        guard let tokenData = jwtToken.data(using: .utf8) else {
            print("Не удалось преобразовать токен в Data")
            return
        }

        let jwtDecoder = JWTDecoder(jwtVerifier: .none)
        do {
            let decoded = try jwtDecoder.decode(JWT<UserClaims>.self, from: tokenData)
            
            UserSession.shared.userId = decoded.claims.nameidentifier
            UserSession.shared.userName = decoded.claims.name
            UserSession.shared.roles = [decoded.claims.role]

        } catch {
            print("Ошибка декодирования JWT: \(error)")
        }
    } else {
        print("Токен не найден")
    }
}


extension AuthService {
    func register(userName: String, email: String, password: String, completion: @escaping (RegisterErrorType) -> Void) {
        let registerURL = "http://localhost:8081/api/auth/register"
        
        guard let url = URL(string: registerURL) else {
            print("Invalid register URL")
            completion(.unknown)
            return
        }
        
        var request = URLRequest(url: url)
        request.httpMethod = "POST"
        request.setValue("application/json", forHTTPHeaderField: "Content-Type")
        
        let registerData = RegisterRequest(userName: userName, email: email, password: password)
        
        do {
            request.httpBody = try JSONEncoder().encode(registerData)
        } catch {
            print("Failed to encode register data: \(error)")
            completion(.unknown)
            return
        }
        
        let task = URLSession.shared.dataTask(with: request) { data, response, error in
            if let error = error {
                print("Network error: \(error.localizedDescription)")
                completion(.unknown)
                return
            }
            
            guard let httpResponse = response as? HTTPURLResponse else {
                print("No HTTP response received")
                completion(.unknown)
                return
            }
            
            guard let data = data else {
                print("No data received from server")
                completion(.unknown)
                return
            }
            
            if httpResponse.statusCode == 200 {
                do {
                    let successResponse = try JSONDecoder().decode(RegisterSuccessResponse.self, from: data)
                    
                    TokenManager.shared.saveToken(successResponse.accessToken, key: "accessToken")
                    TokenManager.shared.saveToken(successResponse.refreshToken, key: "refreshToken")
                    decodeUserClaims()

                    completion(.success)
                } catch {
                    print("Failed to decode success response: \(error)")
                    completion(.unknown)
                }
                return
            }
            
            do {
                let validationError = try JSONDecoder().decode(ValidationError.self, from: data)
                completion(validationError.classify())
            } catch {
                print("Failed to decode error response: \(error)")
                completion(.unknown)
            }
        }
        
        task.resume()
    }
}
