import {DishCategoryDTO} from '../dish-category/dish-category.dto';

export interface DishDTO {
  id: string;
  name: string;
  description: string;
  price: number;
  imageUrl: string;
  category: DishCategoryDTO;
}
