import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Item } from '../Models/Item';
import { CartService } from '../Services/cart.service';
import { ItemService } from '../Services/item.service';

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.css']
})
export class ItemDetailComponent implements OnInit {
  itemId: number | null = null;
  item: Item | null = null;
  selectedQuantity: number = 0 ;
  cartItems: Item[] = [];

  constructor(private route: ActivatedRoute, private router: Router,
    private itemService: ItemService,
    private cartService: CartService) {
   
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.itemId = +params['id'];
      this.loadItemDetails();
    });
  }
  loadItemDetails() {
    if (this.itemId !== null) {
      this.itemService.getItem(this.itemId).subscribe(
        (item) => {          
          this.item = item;
        },
        (error) => {
          console.error('Error fetching item details:', error);
          // Handle error, e.g., display an error message or redirect to an error page.
        }
      );
    }
  }
 
  addToCart(item: Item, quantity: number): void {

    item.quantity = quantity;
    this.cartService.addCartItem(item).subscribe(
      () => {
        console.log('Item added to cart');
        this.cartService.cartItemsChanged.emit();
      },
      (error) => {
        console.error('Error adding item to cart', error);
      }
    );
  }
  continueShopping() {
    this.router.navigate(['/']); 
  }
}
