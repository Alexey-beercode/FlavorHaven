import { Component, OnInit } from '@angular/core';
import { PaymentService } from '../../../services/payment.service';
import { UserService } from '../../../services/user.service';
import { PaymentDTO } from '../../../models/dtos/payment/payment.dto';
import { UserDTO } from '../../../models/dtos/user/user.dto';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-payments',
  templateUrl: './admin-payments.component.html',
  styleUrls: ['./admin-payments.component.css'],
  standalone: true,
  imports : [CommonModule]
})
export class AdminPaymentsComponent implements OnInit {
  payments: (PaymentDTO & { userName?: string })[] = []; // Платежи с именами пользователей
  isLoading: boolean = false;
  error: string | null = null;

  constructor(
    private paymentService: PaymentService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.loadPayments();
  }

  // Загрузка платежей
  loadPayments(): void {
    this.isLoading = true;

    this.paymentService.getAllPayments().subscribe({
      next: (payments) => {
        this.payments = payments;

        // Подгружаем пользователей для каждого платежа
        payments.forEach((payment) => {
          this.userService.getUserById(payment.userId).subscribe({
            next: (user) => {
              const paymentToUpdate = this.payments.find(p => p.id === payment.id);
              if (paymentToUpdate) {
                paymentToUpdate.userName = user.userName;
              }
            },
            error: (err) => {
              console.error(`Ошибка загрузки пользователя ${payment.userId}:`, err);
            },
          });
        });

        this.isLoading = false;
      },
      error: (err) => {
        console.error('Ошибка загрузки платежей:', err);
        this.error = 'Не удалось загрузить платежи.';
        this.isLoading = false;
      },
    });
  }
}
