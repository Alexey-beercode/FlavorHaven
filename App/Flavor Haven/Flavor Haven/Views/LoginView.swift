import SwiftUI
import SwiftJWT

struct LoginView: View {
    @State private var userName: String = ""
    @State private var password: String = ""
    @State private var errorMessage: String?
    @State private var isLogin = false
    @State private var isRegisterViewPresented = false
    @Environment(\.dismiss) var dismiss
    
    var body: some View {
        NavigationStack {
            VStack(spacing: 20) {
                Text("Добро пожаловать!")
                    .font(.title)
                    .bold()
                    .foregroundColor(.orange)
                    .padding(.bottom, 20)
                Text("Войдите, чтобы оформить заказ")
                    .padding(.bottom, 20)
                TextField("Логин", text: $userName)
                    .textFieldStyle(RoundedBorderTextFieldStyle())
                    .autocapitalization(.none)
                    .padding(.horizontal)
                
                SecureField("Пароль", text: $password)
                    .textFieldStyle(RoundedBorderTextFieldStyle())
                    .padding(.horizontal)
                
                if let errorMessage = errorMessage {
                    Text("Ой! Мы не можем вас узнать. Проверьте логин и пароль.")
                        .foregroundColor(.red)
                        .font(.caption)
                        .multilineTextAlignment(.center)
                        .padding(.horizontal)
                }
                
                Button(action: login) {
                    Text("Войти")
                        .frame(maxWidth: .infinity)
                        .padding()
                        .background(Color.orange)
                        .foregroundColor(.white)
                        .cornerRadius(8)
                }
                .padding(.horizontal)
                
                Button(action: {
                    isRegisterViewPresented = true
                }) {
                    Text("Зарегистрироваться")
                        .foregroundColor(.orange)
                        .padding(.top, 10)
                }
                .sheet(isPresented: $isRegisterViewPresented) {
                    RegisterView()
                }
            }
            .padding()
        }
    }
    
    func login() {
        AuthService.shared.login(userName: userName, password: password) { result in
            DispatchQueue.main.async {
                switch result {
                case .success(let response):
                    print("Успешный вход: \(response.accessToken)")
                    TokenManager.shared.saveToken(response.accessToken, key: "accessToken")
                    decodeUserClaims()
                    print("Id: \(UserSession.shared.userId)")
                    isLogin = true;
                    dismiss()

                case .failure(let error):
                    errorMessage = "Ошибка: \(error.localizedDescription)"
                }
            }
            
        }
    }
}

#Preview {
    LoginView()
}
