import { SortingParameters } from '../dish/sorting-parameters.dto';

export interface GetDishesRequestDTO {
  categoryId?: string;
  searchName?: string;
  sorting?: SortingParameters;
  pageNumber?: number;
  pageSize?: number;
}
