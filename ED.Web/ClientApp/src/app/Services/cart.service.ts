import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CartItem } from '../Models/CartItem';
import { Item } from '../Models/Item';


@Injectable({
  providedIn: 'root',
})
export class CartService {
  private apiUrl = 'https://localhost:44424/api/Cart';
  cartItemsChanged = new EventEmitter<void>();
  constructor(private http: HttpClient) { }

  //private cartItems: { item: Item; quantity: number }[] = [];

  addToCart(cartItem: CartItem, quantity: number): void {
    //const existingCartItem = this.cartItems.find((cartItem) => cartItem.item.id === item.id);

    //if (existingCartItem) {
    //  existingCartItem.quantity += quantity;
    //} else {
    //  this.cartItems.push({ item, quantity });
    //}
  }

  getCartItems(): Observable<CartItem[]> {
    return this.http.get<CartItem[]>(this.apiUrl);
  }

  //getCartItemCount(): number {
  //  return this.cartItems.reduce((total, cartItem) => total + cartItem.quantity, 0);
  //}

  getCartItem(id: number): Observable<CartItem> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<CartItem>(url);
  }

  getCartItemCount(): Observable<number> {    
    return this.http.get<number>(`${this.apiUrl}/count`);
  }

  addCartItem(item: Item): Observable<Item> {
    return this.http.post<Item>(this.apiUrl, item);
  }

  updateCartItem(id: number, updatedItem: CartItem): Observable<any> {
   
    const url = `${this.apiUrl}/${id}`;
    return this.http.put(url, updatedItem);
  }

  deleteCartItem(id: number): Observable<any> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete(url);
  }

   
}
