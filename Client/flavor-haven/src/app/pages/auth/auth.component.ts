import {Component, ViewEncapsulation} from '@angular/core';
import { LoginFormComponent } from './login/login-form/login-form.component';
import { RegisterFormComponent } from './register/register-form/register-form.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css'],
  imports: [LoginFormComponent, RegisterFormComponent,CommonModule],
  encapsulation: ViewEncapsulation.Emulated,
  standalone: true,
})
export class AuthComponent {
  isLoginMode = true; // Логин активен по умолчанию

  switchToLogin() {
    this.isLoginMode = true;
  }

  switchToRegister() {
    this.isLoginMode = false;
  }
}
