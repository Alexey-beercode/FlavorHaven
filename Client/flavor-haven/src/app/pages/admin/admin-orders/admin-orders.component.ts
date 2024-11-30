import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../../services/order.service';
import { UserService } from '../../../services/user.service';
import { OrderStatusService } from '../../../services/order-status.service';
import { OrderDTO } from '../../../models/dtos/order/order.dto';
import { OrderStatusDTO } from '../../../models/dtos/order/order-status.dto';
import { CommonModule } from '@angular/common';
import { ErrorMessageComponent } from '../../../components/error-message/error-message.component';
import { OrderReviewComponent } from './order-review/order-review.component';
import { OrderItemsComponent } from './order-items/order-items.component';
import { forkJoin, of, switchMap } from 'rxjs';

@Component({
  selector: 'app-admin-orders',
  templateUrl: './admin-orders.component.html',
  styleUrls: ['./admin-orders.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    ErrorMessageComponent,
    OrderReviewComponent,
    OrderItemsComponent,
  ],
})
export class AdminOrdersComponent implements OnInit {
  orders: (OrderDTO & { userName?: string })[] = [];
  statuses: OrderStatusDTO[] = [];
  isLoading: boolean = false;
  error: string | null = null;

  selectedOrderIdForReview: string | null = null;
  selectedOrderIdForItems: string | null = null;

  constructor(
    private orderService: OrderService,
    private userService: UserService,
    private statusService: OrderStatusService
  ) {}

  ngOnInit(): void {
    this.loadOrdersAndStatuses();
  }

  // Загрузка статусов и заказов по каждому статусу
  loadOrdersAndStatuses(): void {
    this.isLoading = true;
    this.error = null;

    this.statusService.getAllOrderStatuses().pipe(
      switchMap((statuses) => {
        this.statuses = statuses;

        const orderRequests = statuses.map((status) =>
          this.orderService.getOrdersByStatus(status.id)
        );

        // Выполнить все запросы на заказы по статусам
        return forkJoin(orderRequests);
      })
    ).subscribe({
      next: (ordersByStatus) => {
        const combinedOrders = ordersByStatus.flat();


        const userRequests = combinedOrders.map((order) =>
          this.userService.getUserById(order.userId)
        );

        // Загрузить пользователей для всех заказов
        forkJoin(userRequests).subscribe({
          next: (users) => {
            this.orders = combinedOrders.map((order, index) => ({
              ...order,
              userName: users[index]?.userName || 'Неизвестный пользователь',
            }));
            this.isLoading = false;
          },
          error: (err) => {
            console.error('Ошибка при загрузке пользователей:', err);
            this.error = 'Не удалось загрузить данные пользователей.';
            this.isLoading = false;
          },
        });
      },
      error: (err) => {
        console.error('Ошибка при загрузке статусов или заказов:', err);
        this.error = 'Не удалось загрузить данные заказов и статусов.';
        this.isLoading = false;
      },
    });
  }

  // Изменение статуса заказа
  updateStatus(orderId: string, statusId: string): void {
    this.orderService.updateOrderStatus(orderId, { statusId }).subscribe({
      next: () => {
        this.loadOrdersAndStatuses(); // Перезагрузка данных
      },
      error: (err) => {
        console.error('Ошибка при обновлении статуса заказа:', err);
        this.error = 'Не удалось обновить статус заказа.';
      },
    });
  }

  // Открытие/скрытие списка блюд
  viewItems(orderId: string): void {
    this.selectedOrderIdForItems =
      this.selectedOrderIdForItems === orderId ? null : orderId;
  }

  // Открытие/скрытие отзыва
  viewReview(orderId: string): void {
    this.selectedOrderIdForReview =
      this.selectedOrderIdForReview === orderId ? null : orderId;
  }
}
