import { DishDTO } from '../dish/dish.dto';

export interface OrderItemDTO {
  id: string;
  count: number;
  dish: DishDTO;
}
