import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../../services/order.service';
import { UserService } from '../../../services/user.service';
import { OrderStatusService } from '../../../services/order-status.service';
import { OrderDTO } from '../../../models/dtos/order/order.dto';
import { OrderStatusDTO } from '../../../models/dtos/order/order-status.dto';
import { UserDTO } from '../../../models/dtos/user/user.dto';
import {switchMap, forkJoin, map} from 'rxjs';
import {FormsModule} from '@angular/forms';
import {ErrorMessageComponent} from '../../../components/error-message/error-message.component';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-admin-orders',
  templateUrl: './admin-orders.component.html',
  styleUrls: ['./admin-orders.component.css'],
  standalone: true,
  imports: [
    FormsModule,
    ErrorMessageComponent,
    CommonModule
  ]
})
export class AdminOrdersComponent implements OnInit {
  orders: (OrderDTO & { userName?: string })[] = [];
  statuses: OrderStatusDTO[] = [];
  isLoading: boolean = false;
  error: string = '';

  constructor(
    private orderService: OrderService,
    private userService: UserService,
    private orderStatusService: OrderStatusService
  ) {}

  ngOnInit(): void {
    this.loadOrders();
    this.loadStatuses();
  }

  loadOrders(): void {
    this.isLoading = true;
    this.error = '';

    this.orderService.getAll().pipe(
      switchMap((orders: OrderDTO[]) => {
        const userRequests = orders.map((order) =>
          this.userService.getUserById(order.userId)
        );
        return forkJoin(userRequests).pipe(
          map((users) => {
            return orders.map((order, index) => ({
              ...order,
              userName: users[index]?.userName || 'Неизвестный пользователь',
            }));
          })
        );
      })
    ).subscribe({
      next: (orders) => {
        this.orders = orders;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Ошибка при загрузке заказов:', err);
        this.error = 'Не удалось загрузить заказы.';
        this.isLoading = false;
      },
    });
  }

  loadStatuses(): void {
    this.orderStatusService.getAllOrderStatuses().subscribe({
      next: (statuses: OrderStatusDTO[]) => {
        this.statuses = statuses;
      },
      error: (err) => {
        console.error('Ошибка при загрузке статусов:', err);
        this.error = 'Не удалось загрузить статусы.';
      },
    });
  }

  updateStatus(orderId: string, statusId: string): void {
    this.orderService.updateOrderStatus(orderId, { statusId }).subscribe({
      next: () => {
        this.loadOrders(); // Обновить заказы после изменения статуса
      },
      error: (err) => {
        console.error('Ошибка при обновлении статуса заказа:', err);
        this.error = 'Не удалось обновить статус заказа.';
      },
    });
  }
}
