import SwiftUI

struct RegisterView: View {
    @State private var userName: String = ""
    @State private var email: String = ""
    @State private var password: String = ""
    @State private var errorMessage: String?
    @State private var isRegistered: Bool = false
    @Environment(\.dismiss) var dismiss

    var body: some View {
        VStack(spacing: 20) {
            Text("Регистрация")
                .font(.largeTitle)
                .foregroundColor(.orange)
                .bold()
                .padding(.bottom, 30)
            Text("Чтобы вкусно покушать, осталось совсем чуть-чуть")
                .multilineTextAlignment(.center)
                .padding(.bottom, 30)
            TextField("Логин", text: $userName)
                .textFieldStyle(RoundedBorderTextFieldStyle())
                .autocapitalization(.none)
                .padding(.horizontal)
            
            TextField("Email", text: $email)
                .textFieldStyle(RoundedBorderTextFieldStyle())
                .keyboardType(.emailAddress)
                .autocapitalization(.none)
                .padding(.horizontal)
            
            SecureField("Пароль", text: $password)
                .textFieldStyle(RoundedBorderTextFieldStyle())
                .padding(.horizontal)
            
            if let errorMessage = errorMessage {
                Text(errorMessage)
                    .foregroundColor(.red)
                    .font(.caption)
                    .multilineTextAlignment(.center)
                    .padding(.horizontal)
            }
            
            Button(action: register) {
                Text("Зарегистрироваться")
                    .frame(maxWidth: .infinity)
                    .padding()
                    .background(Color.orange)
                    .foregroundColor(.white)
                    .cornerRadius(8)
            }
            .padding(.horizontal)
            
            if isRegistered {
                Text("Регистрация успешна!")
                    .foregroundColor(.green)
                    .font(.caption)
                    .padding(.top, 10)
            }
        }
        .padding()
        .onChange(of: isRegistered) { newValue in
            if newValue {
                dismiss()
            }
        }
    }
    
    func register() {
        AuthService.shared.register(userName: userName, email: email, password: password) { errorType in
            DispatchQueue.main.async {
                if errorType == .success {
                    isRegistered = true
                } else if errorType == .invalidPassword {
                    errorMessage = "Пароль должен быть не менее 6 символов."
                    isRegistered = false
                } else if errorType == .invalidEmail {
                    errorMessage = "Неверный формат почты."
                    isRegistered = false
                } else if errorType == .unknown {
                    if userName.isEmpty {
                        errorMessage = "Пожалуйста, введите корректные данные."
                        isRegistered = false
                    } else {
                        errorMessage = "Пользователь с таким логином уже существует."
                        isRegistered = false
                    }
                }
            }
        }
    }
}

#Preview {
    RegisterView()
}
