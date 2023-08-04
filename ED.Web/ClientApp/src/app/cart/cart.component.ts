import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CartItem } from '../Models/CartItem';
import { Item } from '../Models/Item';
import { CartService } from '../Services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  cartItems: any[] = [];
  item: Item | undefined;
  total: number = 0;
  constructor(private cartService: CartService, private router: Router) { }

  ngOnInit(): void {
    this.loadCartItems();
  }

  

  loadCartItems(): void {
    this.cartService.getCartItems().subscribe(cartItems => {
      this.cartItems = cartItems;
      this.total = this.cartItems.reduce((total, cartItem) => total + (cartItem.price * cartItem.quantity), 0);
    });
  }

  updateItem(updatedItem: CartItem): void {
   

    this.cartService.updateCartItem(updatedItem.id, updatedItem).subscribe(() => {
      // Reload cart items after update
      this.loadCartItems();
      this.cartService.cartItemsChanged.emit();
    });
  }

  deleteItem(itemId: number): void {
    this.cartService.deleteCartItem(itemId).subscribe(() => {
      // Reload cart items after delete
      this.loadCartItems();
      this.cartService.cartItemsChanged.emit();
      //this.router.navigate(['/']); 
    });
  }
  continueShopping() {
    this.router.navigate(['/']);
  }
}
