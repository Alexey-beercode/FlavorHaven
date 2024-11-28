import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component'; // Импорт HomeComponent
import { CartComponent } from './pages/cart/cart.component'; // Если есть CartComponent
import { ProfileComponent } from './pages/profile/profile.component'; // Если есть ProfileComponent

export const routes: Routes = [
  { path: '', component: HomeComponent }, // Главная страница
  { path: 'cart', component: CartComponent }, // Страница корзины
  { path: 'profile', component: ProfileComponent }, // Страница профиля
  { path: '**', redirectTo: '' }, // Перенаправление на главную для неизвестных маршрутов
];
