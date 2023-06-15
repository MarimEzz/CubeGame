import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, catchError, map, of, tap, throwError } from 'rxjs';
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
export class WishlistService {

private base_url="https://localhost:7121/api/Wishlist/";

private wishlistItems: any[] = [];
 wishlistItems$ = new BehaviorSubject<any[]>(this.wishlistItems);
 private iswishlistFetched = false;
  constructor(private httpclint : HttpClient) { }
  //-------------------Get Wishlist------------------
  GetWishlist(): Observable<any[]> {
    if (!this.iswishlistFetched) {
    return this.httpclint.get<any>(`${this.base_url}getAll`)
      .pipe(
        tap((response:Product[])=>{
        this.wishlistItems=response;
        this.wishlistItems$.next(this.wishlistItems);
        this.iswishlistFetched = true;
        return response
          }),
          catchError((error) => {
            console.error('Error:', error);
            return throwError(error);
            }));}
            else
            {
              return of(this.wishlistItems);

            }
      }
      //-------------------Add To Wishlist------------------
      AddToWishlist(id: Number): Observable<Product> {
      return this.httpclint.post<any>(`${this.base_url}AddTowishlist/${id}`,id)
      .pipe(
      tap((response) => {
      this.wishlistItems.push(response);
      this.wishlistItems$.next(this.wishlistItems);

      }),
      catchError((error) => {
        console.error('Error:', error);
        return throwError(error);
      }
      ));
    }
    //-------------------Delete From Wishlist------------------
    DeleteFromWishlist(id: Number): Observable<Product> {
      return this.httpclint.delete<Product>(`${this.base_url}RemoveFromwishlist/${id}`)
      .pipe(
      tap(() => {
        const index = this.wishlistItems.findIndex(item => item.id === id);
         if (index !== -1) {
         this.wishlistItems.splice(index, 1);
         this.wishlistItems$.next(this.wishlistItems);
         }
      })
      )
    }
    //---------------clear wishlist----------------
    ClearCart(): Observable<any>{
      return this.httpclint.delete<Product>(`${this.base_url}clearwishlist`).pipe(
        tap(() => {
          this.wishlistItems = []
          this.wishlistItems$.next(this.wishlistItems);
        }))
    }
    }

