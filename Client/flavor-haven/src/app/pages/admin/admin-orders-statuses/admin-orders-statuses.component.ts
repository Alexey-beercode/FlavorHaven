import { Component, OnInit } from '@angular/core';
import { OrderStatusService } from '../../../services/order-status.service';
import { OrderStatusDTO } from '../../../models/dtos/order/order-status.dto';
import { OrderStatusAddModalComponent } from './order-status-add-modal/order-status-add-modal.component';
import { CommonModule } from '@angular/common';
import {ErrorMessageComponent} from '../../../components/error-message/error-message.component';

@Component({
  selector: 'app-admin-orders-statuses',
  templateUrl: './admin-orders-statuses.component.html',
  styleUrls: ['./admin-orders-statuses.component.css'],
  standalone: true,
  imports: [OrderStatusAddModalComponent, CommonModule, ErrorMessageComponent],
})
export class AdminOrdersStatusesComponent implements OnInit {
  statuses: OrderStatusDTO[] = [];
  isLoading: boolean = false;
  error: string | null = null;
  isAddModalVisible: boolean = false;

  constructor(private orderStatusService: OrderStatusService) {}

  ngOnInit(): void {
    this.loadStatuses();
  }

  // Загрузка всех статусов
  loadStatuses(): void {
    this.isLoading = true;
    this.orderStatusService.getAllOrderStatuses().subscribe({
      next: (data) => {
        this.statuses = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Ошибка при загрузке статусов:', err);
        this.error = 'Не удалось загрузить статусы.';
        this.isLoading = false;
      },
    });
  }

  // Удаление статуса
  deleteStatus(id: string): void {
      this.orderStatusService.deleteOrderStatus(id).subscribe({
        next: () => {
          this.statuses = this.statuses.filter((status) => status.id !== id);
        },
        error: (err) => {
          console.error('Ошибка удаления статуса:', err);
          this.error = 'Не удалось удалить статус.';
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

  // Обработка добавления нового статуса
  handleStatusAdded(): void {
    this.loadStatuses(); // Перезагрузка списка статусов
    this.closeAddModal();
  }
}
