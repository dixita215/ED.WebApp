import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { CartService } from '../Services/cart.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  cartService!: CartService; // Mark the property with definite assignment assertion
  cartItemCount: number = 0;

  private cartItemsSubscription: Subscription = new Subscription;
  constructor(cartService: CartService) {
    this.cartService = cartService; // Initialize the property in the constructor
  }
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  ngOnInit(): void {
    this.updateCartCount();

    // Subscribe to changes in the cart items and update the count
    this.cartService.cartItemsChanged.subscribe(() => {
      this.updateCartCount();
    });
  }
  private updateCartCount(): void {
    this.cartService.getCartItems().subscribe(cartItems => {
      this.cartItemCount = cartItems.reduce((total, item) => total + item.quantity, 0);
    });
  }
}
