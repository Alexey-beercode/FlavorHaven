import { Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import {ProfileComponent} from './pages/profile/profile.component';
import {CartComponent} from './pages/cart/cart.component';
import {AuthComponent} from './pages/auth/auth.component';
import {HomeComponent} from './pages/home/home.component';

export const routes: Routes = [
  { path: 'login', component: AuthComponent },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'cart', component: CartComponent, canActivate: [AuthGuard] },
  {path: '', component:HomeComponent,},
];
