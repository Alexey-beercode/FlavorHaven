import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { CartItemRequestDTO } from '../models/dtos/cart/cart-item-request.dto';
import { CartDTO } from '../models/dtos/cart/cart.dto';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private baseUrl = environment.baseApiUrl;
  private cartUrls = environment.apiUrls.cart;

  constructor(private http: HttpClient) {}

  addToCart(userId: string, item: CartItemRequestDTO): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}${this.cartUrls.addByUserId}/${userId}`, item);
  }

  removeFromCart(userId: string, item: CartItemRequestDTO): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}${this.cartUrls.removeByUserId}/${userId}`, {
      body: item,
    });
  }

  clearCart(userId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}${this.cartUrls.clearByUserId}/${userId}`);
  }

  getCartByUserId(userId: string): Observable<CartDTO[]> {
    console.log(`приходящий userid : ${userId}`)
    return this.http.get<CartDTO[]>(`${this.baseUrl}${this.cartUrls.getByUserId}/${userId}`);
  }
}
