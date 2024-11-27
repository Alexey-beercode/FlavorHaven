import { OrderStatusDTO } from './order-status.dto';
import { OrderItemDTO } from './order-item.dto';

export interface OrderDTO {
  id: string;
  orderNumber: string;
  createdTime: Date;
  amount: number;
  note: string;
  address: string;
  isPayed: boolean;
  status: OrderStatusDTO;
  orderItems: OrderItemDTO[];
}
