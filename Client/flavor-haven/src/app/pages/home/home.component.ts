import { Component } from '@angular/core';
import { SortingParameters } from '../../models/dtos/dish/sorting-parameters.dto';
import {DishListComponent} from '../dish/dish-list.component';
import {FilterComponent} from './filter/filter.component';
import {SearchComponent} from './search/search.component';
import {SortComponent} from './sort/sort.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  standalone: true,
  imports: [
    DishListComponent,
    FilterComponent,
    SearchComponent,
    SortComponent
  ],
})
export class HomeComponent {
  categoryId: string | undefined;
  searchName: string | undefined;
  sorting: SortingParameters = SortingParameters.None;

  onCategoryChange(categoryId: string | undefined): void {
    this.categoryId = categoryId;
  }

  onSearchChange(searchName: string): void {
    this.searchName = searchName;
  }

  onSortingChange(sorting: SortingParameters): void {
    this.sorting = sorting;
  }
}
