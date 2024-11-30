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
  userId:string;
  status: OrderStatusDTO;
  orderItems: OrderItemDTO[];
}
