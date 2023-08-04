import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Item } from '../Models/Item';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  public items: Item[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private router: Router) {    
    http.get<Item[]>(baseUrl + 'api/items').subscribe(result => {
      this.items = result;
    }, error => console.error(error));
  }
  onViewButtonClick(itemId: number) {
    this.router.navigate(['/items', itemId]); 
  }
}


