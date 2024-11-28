import { Component, Output, EventEmitter } from '@angular/core';
import { SortingParameters } from '../../../models/dtos/dish/sorting-parameters.dto';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-sort',
  templateUrl: './sort.component.html',
  styleUrls: ['./sort.component.css'],
  standalone: true,
  imports: [
    FormsModule
  ]
})
export class SortComponent {
  sorting: SortingParameters = SortingParameters.None;

  @Output() sortingChanged = new EventEmitter<SortingParameters>();

  onSortingChange(): void {
    this.sortingChanged.emit(this.sorting);
  }

  protected readonly SortingParameters = SortingParameters;
}
