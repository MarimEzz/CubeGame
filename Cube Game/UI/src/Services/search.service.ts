import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

export interface Product {
  id: number,
  productName: string,
  discount: number,
  price: number,
  priceAfterDiscount: number,
  image: string
}
@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseURL = 'https://localhost:7121/api/Product';

  constructor(private http: HttpClient) {}

  searchProducts(SearchItem: string): Observable<any[]> {
    const url = `${this.baseURL}/search?SearchItem=${encodeURIComponent(SearchItem)}`;
    return this.http.get<any[]>(url).pipe(
      catchError((error) => {
        console.error('Error:', error);
        return throwError(error);
      })
    );
  }
}
