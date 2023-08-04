// item.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Item } from '../Models/Item';


@Injectable({
  providedIn: 'root',
})
export class ItemService {
  private apiUrl = 'https://localhost:44424/api/items'; 

  constructor(private http: HttpClient) {}

  getItem(itemId: number): Observable<Item> {
    const url = `${this.apiUrl}/${itemId}`;
    return this.http.get<Item>(url).pipe(
      catchError((error: any) => {
        console.error('Error fetching item:', error);
        throw error;
      })
    );
  }
}
