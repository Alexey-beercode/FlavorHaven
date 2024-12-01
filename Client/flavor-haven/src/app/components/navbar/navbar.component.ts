import { Component, HostListener } from '@angular/core';
import { faHome, faShoppingCart, faUser } from '@fortawesome/free-solid-svg-icons';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {Router, RouterLink} from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
  standalone: true,
  imports: [FontAwesomeModule, RouterLink],
})
export class NavbarComponent {
  constructor(private router: Router) {
  }
  isCollapsed = false; // Состояние сворачивания Navbar
  lastScrollTop = 0; // Предыдущее положение скролла

  // Иконки
  faHome = faHome;
  faShoppingCart = faShoppingCart;
  faUser = faUser;

  @HostListener('window:scroll', [])
  onWindowScroll(): void {
    const currentScroll = window.pageYOffset || document.documentElement.scrollTop;
    this.isCollapsed = currentScroll > this.lastScrollTop; // Свернуть при скролле вниз
    this.lastScrollTop = currentScroll <= 0 ? 0 : currentScroll; // Защита от отрицательных значений
  }
  goHome(){
    this.router.navigate(['/']);
  }
}
