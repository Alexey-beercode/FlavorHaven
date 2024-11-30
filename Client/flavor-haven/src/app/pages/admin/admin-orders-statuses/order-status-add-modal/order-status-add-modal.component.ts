import { Component, EventEmitter, Output } from '@angular/core';
import { OrderStatusService } from '../../../../services/order-status.service';
import { OrderStatusRequestDTO } from '../../../../models/dtos/order/order-status-request.dto';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import {ErrorMessageComponent} from '../../../../components/error-message/error-message.component';

@Component({
  selector: 'app-order-status-add-modal',
  templateUrl: './order-status-add-modal.component.html',
  styleUrls: ['./order-status-add-modal.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule, ErrorMessageComponent],
})
export class OrderStatusAddModalComponent {
  statusName: string = ''; // Название нового статуса
  isLoading: boolean = false;
  error: string | null = null;

  @Output() closeModal = new EventEmitter<void>(); // Закрытие модалки
  @Output() statusAdded = new EventEmitter<void>(); // Уведомление об успешном добавлении статуса

  constructor(private orderStatusService: OrderStatusService) {}

  // Создание нового статуса заказа
  createStatus(): void {
    if (!this.statusName.trim()) {
      this.error = 'Название статуса не может быть пустым.';
      return;
    }

    this.isLoading = true;
    const request: OrderStatusRequestDTO = { name: this.statusName };

    this.orderStatusService.createOrderStatus(request).subscribe({
      next: () => {
        this.isLoading = false;
        this.statusAdded.emit();
        this.closeModal.emit();
      },
      error: (err) => {
        console.error('Ошибка при создании статуса:', err);
        this.error = 'Не удалось создать статус.';
        this.isLoading = false;
      },
    });
  }

  // Закрытие модального окна
  close(): void {
    this.closeModal.emit();
  }
}
