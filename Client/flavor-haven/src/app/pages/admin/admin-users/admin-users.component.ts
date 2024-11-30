import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../services/user.service';
import { RoleService } from '../../../services/role.service';
import { UserDTO } from '../../../models/dtos/user/user.dto';
import { RoleDTO } from '../../../models/dtos/role/role.dto';
import { UserRoleRequestDTO } from '../../../models/dtos/role/user-role-request.dto';
import { CommonModule } from '@angular/common';
import { ErrorMessageComponent } from '../../../components/error-message/error-message.component';

@Component({
  selector: 'app-admin-users',
  templateUrl: './admin-users.component.html',
  styleUrls: ['./admin-users.component.css'],
  standalone: true,
  imports: [CommonModule, ErrorMessageComponent],
})
export class AdminUsersComponent implements OnInit {
  users: (UserDTO & { roles: RoleDTO[] })[] = [];
  allRoles: RoleDTO[] = [];
  isLoading: boolean = false;
  error: string | null = null;

  constructor(
    private userService: UserService,
    private roleService: RoleService
  ) {}

  ngOnInit(): void {
    this.loadUsersAndRoles();
  }

  // Загрузка всех пользователей и ролей
  loadUsersAndRoles(): void {
    this.isLoading = true;
    this.error = null;

    Promise.all([
      this.userService.getAllUsers().toPromise(),
      this.roleService.getAllRoles().toPromise(),
    ])
      .then(([users = [], roles = []]) => {
        this.allRoles = roles;

        const roleRequests = users.map((user) =>
          this.roleService.getRolesByUserId(user.id).toPromise()
        );

        Promise.all(roleRequests)
          .then((rolesByUser) => {
            this.users = users.map((user, index) => ({
              ...user,
              roles: rolesByUser[index] ?? [], // Убедимся, что roles не undefined
            }));
            this.isLoading = false;
          })
          .catch((err) => {
            console.error('Ошибка при загрузке ролей пользователей:', err);
            this.error = 'Не удалось загрузить роли пользователей.';
            this.isLoading = false;
          });
      })
      .catch((err) => {
        console.error('Ошибка при загрузке данных пользователей и ролей:', err);
        this.error = 'Не удалось загрузить данные пользователей и ролей.';
        this.isLoading = false;
      });
  }

  // Проверка наличия роли у пользователя
  hasUserRole(user: UserDTO & { roles: RoleDTO[] }, role: RoleDTO): boolean {
    return user.roles.some((userRole) => userRole.id === role.id);
  }

  // Добавление роли пользователю
  addRoleToUser(userId: string, roleId: string): void {
    const request: UserRoleRequestDTO = { userId, roleId };
    this.roleService.setRoleToUser(request).subscribe({
      next: () => this.loadUsersAndRoles(),
      error: (err) => {
        console.error('Ошибка при добавлении роли:', err);
        this.error = 'Не удалось добавить роль пользователю.';
      },
    });
  }

  // Удаление роли у пользователя
  removeRoleFromUser(userId: string, roleId: string): void {
    const request: UserRoleRequestDTO = { userId, roleId };
    this.roleService.removeRoleFromUser(request).subscribe({
      next: () => this.loadUsersAndRoles(),
      error: (err) => {
        console.error('Ошибка при удалении роли:', err);
        this.error = 'Не удалось удалить роль у пользователя.';
      },
    });
  }
}
