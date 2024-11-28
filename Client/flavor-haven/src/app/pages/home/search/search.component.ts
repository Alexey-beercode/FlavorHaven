import { Component, Output, EventEmitter } from '@angular/core';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css'],
  standalone: true,
  imports: [
    FormsModule
  ]
})
export class SearchComponent {
  searchName: string = '';

  @Output() searchChanged = new EventEmitter<string>();

  onSearchChange(): void {
    this.searchChanged.emit(this.searchName);
  }
}
