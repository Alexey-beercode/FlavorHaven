import { SortingParameters } from './sorting-parameters.dto';

export interface GetDishesRequestDTO {
  categoryId?: string;
  searchName?: string;
  sorting?: SortingParameters;
  pageNumber?: number;
  pageSize?: number;
}
