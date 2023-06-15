import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, catchError, of, tap, throwError } from 'rxjs';

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

export class CartService {

private baseURL = "https://localhost:7121/api/Cart/"

private cartItems: Product[] = [];
cartItems$ = new BehaviorSubject<Product[]>(this.cartItems);
private isCartFetched = false;
constructor(private client: HttpClient) {}


//---------- Get Cart Items ----------------------
GetCart(): Observable<Product[]> {
  if (!this.isCartFetched) {
    return this.client.get<any>(`${this.baseURL}`)
    .pipe(
      tap((response: Product[]) => {
        this.cartItems = response;
        this.cartItems$.next(this.cartItems);
        this.isCartFetched = true;
        return response;
      }),
      catchError((error) => {
        console.error('Error:', error);
        return throwError(error);
      })
    );
  } else {
    return of(this.cartItems);
  }
}


//---------- Add To Cart ----------------------

AddTCart(id: number): Observable<Product> {
return this.client.post<Product>(`${this.baseURL}AddToCart/${id}`, id).pipe(
tap((response => {
this.cartItems.push(response);
this.cartItems$.next(this.cartItems);

})),
catchError((error) => {
console.error('Error:', error);
return throwError(error);
})
);
}

//---------- Remove From Cart ---------------------

RemoveFCart(idd: number): Observable<any> {
return this.client.delete<any>(`${this.baseURL}RemoveFromCart/${idd}`)
.pipe(
tap(() => {
const index = this.cartItems.findIndex(item => item.id === idd);
if (index !== -1) {
this.cartItems.splice(index, 1);
this.cartItems$.next(this.cartItems);
}
}))
}

//---------- Clear Cart --------------------

ClearCart(): Observable<any>{
  return this.client.delete<any>(`${this.baseURL}ClearCart`).pipe(
    tap(() => {
      this.cartItems = []
      this.cartItems$.next(this.cartItems);
    }))
}
}
