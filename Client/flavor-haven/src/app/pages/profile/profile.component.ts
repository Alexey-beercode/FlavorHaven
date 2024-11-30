import { Component, OnInit } from '@angular/core';
import { BackButtonComponent } from '../../components/back-button/back-button.component';
import { UserInfoTabComponent } from './tabs/user-info-tab/user-info-tab.component';
import { BalanceTabComponent } from './tabs/balance-tab/balance-tab.component';
import { ReviewsTabComponent } from './tabs/reviews-tab/reviews-tab.component';
import { OrderTabComponent } from './tabs/orders-tab/orders-tab.component';
import { CommonModule } from '@angular/common';
import { TokenService } from '../../services/token.service';
import {AuthService} from '../../services/auth.service';
import {FaIconComponent} from '@fortawesome/angular-fontawesome';
import {faSignOutAlt} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    BackButtonComponent,
    UserInfoTabComponent,
    BalanceTabComponent,
    ReviewsTabComponent,
    OrderTabComponent,
    FaIconComponent,
  ],
})
export class ProfileComponent implements OnInit {
  activeTab: string = 'info';
  userId: string | null = ''; // ID пользователя
  orderId: string | null = '';

  constructor(private tokenService: TokenService, private authService:AuthService) {}

  ngOnInit(): void {
    this.loadUserId();
  }

  private loadUserId(): void {
    this.userId = this.tokenService.getUserIdFromToken();
    console.log(`User ID: ${this.userId}`);
  }

  setActiveTab(tabName: string): void {
    this.activeTab = tabName;
  }

  switchTab(tab: string): void {
    this.activeTab = tab;
  }

  // Обработчик выбора заказа
  selectOrder(orderId: string): void {
    this.orderId = orderId;
    this.switchTab('reviews');  // Переключаем на вкладку с отзывами
  }

  logout(): void {
    this.authService.revoke(this.userId).subscribe({
      next: () => {
        this.authService.logout(); // Удаляет токены
        window.location.href = '/login'; // Перенаправляем на страницу логина
      },
      error: (err) => {
        console.error('Ошибка при выходе:', err);
        this.authService.logout(); // Все равно очищаем токены
        window.location.href = '/login'; // Перенаправляем на страницу логина
      },
    });
  }

  protected readonly faSignOutAlt = faSignOutAlt;
}
