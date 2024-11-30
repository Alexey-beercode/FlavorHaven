import { Component, EventEmitter, Output } from '@angular/core';
import { RoleRequestDTO } from '../../../../models/dtos/role/role-request.dto';
import { RoleDTO } from '../../../../models/dtos/role/role.dto';
import { RoleService } from '../../../../services/role.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-role-add-modal',
  templateUrl: './role-add-modal.component.html',
  styleUrls: ['./role-add-modal.component.css'],
  standalone: true,
  imports: [
    FormsModule,CommonModule
  ]
})
export class RoleAddModalComponent {
  roleName: string = ''; // Название новой роли
  isLoading: boolean = false;
  error: string | null = null;

  @Output() closeModal = new EventEmitter<void>(); // Закрытие модалки
  @Output() roleAdded = new EventEmitter<RoleDTO>(); // Уведомление об успешном добавлении роли

  constructor(private roleService: RoleService) {}

  // Создание новой роли
  createRole(): void {
    if (!this.roleName.trim()) {
      this.error = 'Название роли не может быть пустым.';
      return;
    }

    this.isLoading = true;
    const request: RoleRequestDTO = { name: this.roleName };

    this.roleService.createRole(request).subscribe({
      next: () => {
        const newRole: RoleDTO = { id: crypto.randomUUID(), name: this.roleName }; // Моковая роль, сервер может вернуть ID
        this.roleAdded.emit(newRole);
        this.closeModal.emit();
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Ошибка при создании роли:', err);
        this.error = 'Не удалось создать роль.';
        this.isLoading = false;
      },
    });
  }

  // Закрытие модального окна
  close(): void {
    this.closeModal.emit();
  }
}
