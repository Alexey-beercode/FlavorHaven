import { Component, Input, OnInit } from '@angular/core';
import { OrderService } from '../../../../services/order.service';
import { OrderItemDTO } from '../../../../models/dtos/order/order-item.dto';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-order-items',
  templateUrl: './order-items.component.html',
  styleUrls: ['./order-items.component.css'],
  standalone: true,
  imports: [CommonModule],
})
export class OrderItemsComponent implements OnInit {
  @Input() orderId!: string;
  items: OrderItemDTO[] = [];

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.loadItems();
  }

  loadItems(): void {
    this.orderService.getOrderById(this.orderId).subscribe({
      next: (order) => {
        this.items = order.orderItems;
      },
      error: (err) => {
        console.error('Ошибка при загрузке блюд заказа:', err);
      },
    });
  }
}
