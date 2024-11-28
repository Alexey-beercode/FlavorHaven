import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-error-message',
  templateUrl: './error-message.component.html',
  styleUrls: ['./error-message.component.css'],
  standalone: true,
})
export class ErrorMessageComponent {
  @Input() message: string = 'An unexpected error occurred.';
}