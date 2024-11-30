import { Component, OnInit } from '@angular/core';
import { RoleService } from '../../../services/role.service';
import { RoleDTO } from '../../../models/dtos/role/role.dto';
import { RoleAddModalComponent } from './role-add-modal/role-add-modal.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-roles',
  templateUrl: './admin-roles.component.html',
  styleUrls: ['./admin-roles.component.css'],
  standalone: true,
  imports: [
    RoleAddModalComponent,CommonModule
  ]
})
export class AdminRolesComponent implements OnInit {
  roles: RoleDTO[] = [];
  isLoading: boolean = false;
  error: string | null = null;
  isAddModalVisible: boolean = false;

  constructor(private roleService: RoleService) {}

  ngOnInit(): void {
    this.loadRoles();
  }

  // Загрузка всех ролей
  loadRoles(): void {
    this.isLoading = true;
    this.roleService.getAllRoles().subscribe({
      next: (data) => {
        this.roles = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Ошибка при загрузке ролей:', err);
        this.error = 'Не удалось загрузить роли.';
        this.isLoading = false;
      },
    });
  }

  // Удаление роли
  deleteRole(id: string): void {
      this.roleService.deleteRole(id).subscribe({
        next: () => {
          this.roles = this.roles.filter((role) => role.id !== id);
        },
        error: (err) => {
          console.error('Ошибка удаления роли:', err);
          this.error = 'Не удалось удалить роль.';
        },
      });
  }

  // Открытие модалки добавления
  openAddModal(): void {
    this.isAddModalVisible = true;
  }

  // Закрытие модалки добавления
  closeAddModal(): void {
    this.isAddModalVisible = false;
  }

  // Обработка добавления новой роли
  handleRoleAdded(newRole: RoleDTO): void {
    this.roles.push(newRole); // Добавляем новую роль в массив
    this.closeAddModal(); // Закрываем модалку
  }
}
