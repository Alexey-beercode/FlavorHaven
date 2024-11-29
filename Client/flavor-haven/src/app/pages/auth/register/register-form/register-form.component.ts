import { Component } from '@angular/core';
import { AuthService } from '../../../../services/auth.service';
import { Router } from '@angular/router';
import { RegisterRequestDTO } from '../../../../models/dtos/auth/register-request.dto';
import {FormsModule} from '@angular/forms';
import { CommonModule } from '@angular/common';
import {ErrorMessageComponent} from '../../../../components/error-message/error-message.component';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrl:'./register-form.component.css',
  imports: [
    FormsModule,
    CommonModule,
    ErrorMessageComponent
  ],
  standalone: true
})
export class RegisterFormComponent {
  name: string = ''; // Имя пользователя
  email: string = ''; // Email
  password: string = ''; // Пароль
  confirmPassword: string = ''; // Подтверждение пароля

  isLoading: boolean = false; // Состояние загрузки
  error: string | null = null; // Ошибка

  constructor(private authService: AuthService, private router: Router) {}

  register(): void {
    // Проверяем, совпадают ли пароли
    if (this.password !== this.confirmPassword) {
      this.error = 'Пароли не совпадают';
      return;
    }

    this.isLoading = true;
    const request: RegisterRequestDTO = {
      userName: this.name,
      email: this.email,
      password: this.password,
    };

    this.authService.register(request).subscribe({
      next: () => {
        this.isLoading = false;
        this.router.navigate(['/profile']); // Перенаправляем на профиль
      },
      error: (err) => {
        this.isLoading = false;
        this.error = 'Ошибка при регистрации. Попробуйте позже.';
        console.error('Ошибка регистрации:', err);
      },
    });
  }
}
