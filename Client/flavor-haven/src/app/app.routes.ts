import { Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import {ProfileComponent} from './pages/profile/profile.component';
import {CartComponent} from './pages/cart/cart.component';
import {AuthComponent} from './pages/auth/auth.component';
import {HomeComponent} from './pages/home/home.component';
import {AdminHomeComponent} from './pages/admin/admin-home/admin-home.component';
import {AddToCartComponent} from './pages/dish/add-to-cart/add-to-cart.component';

export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' }, // По умолчанию переход на Home
  { path: 'login', component: AuthComponent },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'cart', component: CartComponent, canActivate: [AuthGuard] },
  { path: 'admin', component: AdminHomeComponent, canActivate: [AuthGuard] },
  { path: 'home', component: HomeComponent },
];


