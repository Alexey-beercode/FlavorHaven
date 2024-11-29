import { Component } from '@angular/core';
import { AuthService } from '../../../../services/auth.service';
import { Router } from '@angular/router';
import {FormsModule} from '@angular/forms';
import { CommonModule } from '@angular/common';
import {ErrorMessageComponent} from '../../../../components/error-message/error-message.component';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css'],
  imports: [
    FormsModule,
    CommonModule,
    ErrorMessageComponent
  ],
  standalone: true
})
export class LoginFormComponent {
  userName: string = '';
  password: string = '';
  isLoading = false;
  error: string | null = null;

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    this.isLoading = true;
    this.authService
      .login({  password: this.password,userName: this.userName })
      .subscribe({
        next: () => {
          this.isLoading = false;
          this.router.navigate(['/profile']); // Перенаправляем на профиль
        },
        error: () => {
          this.isLoading = false;
          this.error = 'Неверное имя пользователя или пароль';
        },
      });
  }
}
