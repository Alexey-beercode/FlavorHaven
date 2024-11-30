import { Component, Input, OnInit } from '@angular/core';
import { UserService } from '../../../../services/user.service';
import { UserDTO } from '../../../../models/dtos/user/user.dto';
import { LoadingSpinnerComponent } from '../../../../components/loading-spinner/loading-spinner.component';
import { ErrorMessageComponent } from '../../../../components/error-message/error-message.component';
import { CommonModule } from '@angular/common';
import {TokenService} from '../../../../services/token.service';
import {RoleService} from '../../../../services/role.service';

@Component({
  selector: 'app-user-info-tab',
  templateUrl: './user-info-tab.component.html',
  styleUrls: ['./user-info-tab.component.css'],
  standalone: true,
  imports: [CommonModule, LoadingSpinnerComponent, ErrorMessageComponent],
})
export class UserInfoTabComponent implements OnInit {
  @Input() userId!: string | null;

  user: UserDTO | null = null;
  userIsAdmin:boolean=false;
  isLoading: boolean = false;
  error: string | null = null;

  constructor(private userService: UserService,private tokenService: TokenService, private roleService: RoleService) {}

  ngOnInit(): void {
    this.loadUserId();
    this.loadUserInfo();
    this.checkIsUserAdmin();
  }

  checkIsUserAdmin(): void {
    if (!this.userId) {
      console.warn('ID пользователя отсутствует для проверки роли администратора.');
      return;
    }

    this.roleService.getRolesByUserId(this.userId).subscribe({
      next: (roles) => {
        this.userIsAdmin = roles.some((role) => role.name === 'Admin');
      },
      error: (err) => {
        console.error('Ошибка при проверке ролей пользователя:', err);
        this.userIsAdmin = false;
      },
    });
  }

  loadUserInfo(): void {
    if (!this.userId) {
      this.error = 'ID пользователя отсутствует.';
      return;
    }

    this.isLoading = true;
    this.userService.getUserById(this.userId).subscribe({
      next: (user) => {
        this.user = user;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Ошибка загрузки данных пользователя:', err);
        this.error = 'Не удалось загрузить данные пользователя.';
        this.isLoading = false;
      },
    });
  }

  private loadUserId() {
    this.userId=this.tokenService.getUserIdFromToken();
    console.log(`user id : ${this.userId}`);
  }

  navigateToAdminPanel(): void {
    window.location.href = '/admin';
  }

}
