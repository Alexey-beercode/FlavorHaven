import { Component } from '@angular/core';
import {CommonModule} from '@angular/common';
import {AdminCategoriesComponent} from '../admin-categories/admin-categories.component';
import {AdminDishesComponent} from '../admin-dishes/admin-dishes.component';
import {AdminPaymentsComponent} from '../admin-payments/admin-payments.component';
import {AdminOrdersComponent} from '../admin-orders/admin-orders.component';
import {AdminRolesComponent} from '../admin-roles/admin-roles.component';
import {AdminUsersComponent} from '../admin-users/admin-users.component';
import {AdminOrdersStatusesComponent} from '../admin-orders-statuses/admin-orders-statuses.component';

@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.css'],
  standalone: true,
  imports: [
    // Включаем все подкомпоненты для вкладок
    CommonModule,
    AdminCategoriesComponent,
    AdminDishesComponent,
    AdminPaymentsComponent,
    AdminOrdersComponent,
    AdminOrdersStatusesComponent,
    AdminRolesComponent,
    AdminUsersComponent,
  ],
})
export class AdminHomeComponent {
  activeTab: string = 'categories'; // По умолчанию открыты категории

  // Список вкладок
  tabs = [
    { name: 'categories', label: 'Категории' },
    { name: 'dishes', label: 'Блюда' },
    { name: 'payments', label: 'Платежи' },
    { name: 'orders', label: 'Заказы' },
    { name: 'order-statuses', label: 'Статусы заказов' },
    { name: 'roles', label: 'Роли' },
    { name: 'users', label: 'Пользователи' },
  ];

  // Переключение вкладок
  switchTab(tabName: string): void {
    this.activeTab = tabName;
  }
}
