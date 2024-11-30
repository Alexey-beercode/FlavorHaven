import { Component, Input, OnInit } from '@angular/core';
import { UserService } from '../../../../services/user.service';
import { TokenService } from '../../../../services/token.service';
import { UserDTO } from '../../../../models/dtos/user/user.dto';
import { UpdateUserBalanceRequestDTO } from '../../../../models/dtos/user/update-user-balance-request.dto';
import { CommonModule } from '@angular/common';
import { LoadingSpinnerComponent } from '../../../../components/loading-spinner/loading-spinner.component';
import { ErrorMessageComponent } from '../../../../components/error-message/error-message.component';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-balance-tab',
  templateUrl: './balance-tab.component.html',
  styleUrls: ['./balance-tab.component.css'],
  standalone: true,
  imports: [CommonModule, LoadingSpinnerComponent, ErrorMessageComponent, FormsModule],
})
export class BalanceTabComponent implements OnInit {
  @Input() userId: string | null = '';
  user: UserDTO | null = null;
  loading: boolean = false;
  errorMessage: string = '';
  successMessage: string = '';
  depositAmount: number = 0;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadUserInfo();
  }

  // Загрузка информации о пользователе
  private loadUserInfo(): void {
    if (this.userId) {
      this.loading = true;
      this.userService.getUserById(this.userId).subscribe({
        next: (user) => {
          this.user = user;
          this.loading = false;
        },
        error: (error) => {
          this.errorMessage = 'Не удалось загрузить информацию о пользователе';
          this.loading = false;
        },
      });
    }
  }

  // Обработчик пополнения баланса
  onDeposit(): void {
    if (this.depositAmount <= 0) {
      this.errorMessage = 'Сумма пополнения должна быть больше нуля';
      return;
    }

    if (this.user) {
      this.loading = true;
      const updateRequest: UpdateUserBalanceRequestDTO = {
        count: this.depositAmount,
      };

      this.userService.updateUserBalance(this.user.id, updateRequest).subscribe({
        next: () => {
          this.successMessage = `Баланс успешно пополнен на ${this.depositAmount} руб.`;
          this.loadUserInfo(); // Обновляем информацию о пользователе
          this.depositAmount = 0; // Очищаем поле ввода
          this.loading = false;
        },
        error: (error) => {
          this.errorMessage = 'Не удалось пополнить баланс';
          this.loading = false;
        },
      });
    }
  }
}
