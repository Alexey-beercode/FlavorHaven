import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { DishService } from '../../services/dish.service';
import { DishDTO } from '../../models/dtos/dish/dish.dto';
import { GetDishesRequestDTO } from '../../models/dtos/dish/get-dishes-request.dto';
import { PaginatedResult } from '../../models/dtos/common/paginated-result.dto';
import { SortingParameters } from '../../models/dtos/dish/sorting-parameters.dto';
import { CurrencyPipe } from '@angular/common';
import { AddToCartComponent } from './add-to-cart/add-to-cart.component';

@Component({
  selector: 'app-dish-list',
  templateUrl: './dish-list.component.html',
  styleUrls: ['./dish-list.component.css'],
  standalone: true,
  imports: [CurrencyPipe, AddToCartComponent],
})
export class DishListComponent implements OnInit, OnChanges {
  @Input() categoryId!: string | undefined;
  @Input() searchName!: string | undefined;
  @Input() sorting!: SortingParameters;

  dishes: DishDTO[] = [];
  currentPage = 1;
  totalPageCount = 0;

  constructor(private dishService: DishService) {}

  ngOnInit(): void {
    this.loadDishes();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if ('categoryId' in changes || 'searchName' in changes || 'sorting' in changes) {
      this.currentPage = 1; // Сбросить на первую страницу при изменении фильтров
      this.loadDishes();
    }
  }


  loadDishes(): void {
    const request: GetDishesRequestDTO = {
      categoryId: this.categoryId,
      searchName: this.searchName,
      sorting: this.sorting,
      pageNumber: this.currentPage,
      pageSize: 10,
    };

    this.dishService.getDishes(request).subscribe({
      next: (result: PaginatedResult<DishDTO>) => {
        this.dishes = result.collection;
        this.currentPage = result.currentPage;
        this.totalPageCount = result.totalPageCount;
      },
      error: (err) => {
        console.error('Ошибка загрузки блюд', err);
      },
    });
  }

  nextPage(): void {
    if (this.currentPage < this.totalPageCount) {
      this.currentPage++;
      this.loadDishes();
    }
  }

  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.loadDishes();
    }
  }
}
