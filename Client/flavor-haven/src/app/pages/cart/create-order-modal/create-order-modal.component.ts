import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { OrderService } from '../../../services/order.service';
import { CreateOrderRequestDTO } from '../../../models/dtos/order/create-order-request.dto';

@Component({
  selector: 'app-create-order-modal',
  templateUrl: './create-order-modal.component.html',
  styleUrls: ['./create-order-modal.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule],
})
export class CreateOrderModalComponent {
  @Input() userId!: string; // ID пользователя
  @Input() isVisible: boolean = false; // Управление видимостью модалки
  @Output() close = new EventEmitter<void>(); // Закрытие модалки
  @Output() orderCreated = new EventEmitter<void>(); // Успешное создание заказа

  address: string = ''; // Поле для адреса
  note: string = ''; // Поле для примечания
  isLoading: boolean = false; // Индикатор загрузки
  error: string | null = null; // Ошибка

  constructor(private orderService: OrderService) {}

  // Закрытие модального окна
  onClose(): void {
    this.close.emit();
  }

  // Создание заказа
  createOrder(): void {
    if (!this.address.trim()) {
      this.error = 'Адрес не может быть пустым!';
      return;
    }

    this.isLoading = true;
    this.error = null;

    const request: CreateOrderRequestDTO = {
      address: this.address,
      note: this.note || '',
    };

    this.orderService.createOrder(this.userId, request).subscribe({
      next: () => {
        this.isLoading = false;
        this.orderCreated.emit(); // Уведомляем об успешном создании
        this.onClose(); // Закрываем модалку
      },
      error: (err) => {
        this.isLoading = false;
        this.error = 'Не удалось создать заказ. Попробуйте позже.';
        console.error('Ошибка создания заказа:', err);
      },
    });
  }
}
