import { Component, Input } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-back-button',
  templateUrl: './back-button.component.html',
  styleUrls: ['./back-button.component.css'],
  standalone: true,
})
export class BackButtonComponent {
  @Input() label: string = 'Back';

  constructor(private location: Location) {}

  goBack(): void {
    this.location.back();
  }
}
