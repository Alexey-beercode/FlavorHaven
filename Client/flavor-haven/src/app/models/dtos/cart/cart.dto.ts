import { DishDTO } from '../dish/dish.dto';

export interface CartDTO {
  id: string;
  count: number;
  dish: DishDTO;
}
