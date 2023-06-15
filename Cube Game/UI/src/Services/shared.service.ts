import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Product } from './search.service';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  private searchQuerySubject: BehaviorSubject<string> = new BehaviorSubject<string>('');
  private searchResultsSubject: BehaviorSubject<Product[]> = new BehaviorSubject<Product[]>([]);

  public searchQuery$: Observable<string> = this.searchQuerySubject.asObservable();
  public searchResults$: Observable<Product[]> = this.searchResultsSubject.asObservable();

  private allProducts: Product[] = []; // Original array of products

  constructor() {}

  public setAllProducts(products: any): void {
    this.allProducts = products;
    this.applyFilter();
  }

  public emitSearchQuery(query: string): void {
    this.searchQuerySubject.next(query);
    this.applyFilter();
  }

  public updateSearchResults(results: Product[]): void {
    this.searchResultsSubject.next(results);
    this.applyFilter();
  }

  private applyFilter(): void {
    const query = this.searchQuerySubject.getValue().toLowerCase();
    if (query === '') {
      this.searchResultsSubject.next(this.allProducts); // Display all products if query is empty
    } else {
      const filteredProducts = this.allProducts.filter((product) =>
        product.productName.toLowerCase().includes(query)
      );
      this.searchResultsSubject.next(filteredProducts); // Display filtered products
    }
  }
}








// import { BehaviorSubject, Observable, Subject } from 'rxjs';
// import { Product } from './wishlist.service';


// @Injectable({
//   providedIn: 'root'
// })

// export class SearchService {


//   private searchQuerySubject: BehaviorSubject<string> = new BehaviorSubject<string>('');
//   private searchResultsSubject: BehaviorSubject<Product[]> = new BehaviorSubject<any[]>([]);

//   public searchQuery$: Observable<string> = this.searchQuerySubject.asObservable();
//   public searchResults$: Observable<any[]> = this.searchResultsSubject.asObservable();
//   //private allProducts: Product[] = []; // Original array of products
//   constructor() {}

//   public emitSearchQuery(query: string): void {
//     this.searchQuerySubject.next(query);
//   }
//   public updateSearchResults(results: Product[]): void {
//       this.searchResultsSubject.next(results);
//     }

// }

// public updateSearchResults(results: Product[]): void {
//   this.searchResultsSubject.next(results);
// }
//////////////////////////////////////////////
// export class SearchService {
//   private searchQuerySubject = new BehaviorSubject<Product[]>([]);
//   searchQuery$ = this.searchQuerySubject.asObservable();

//   emitSearchQuery(query: Product[]) {
//     this.searchQuerySubject.next(query);
//   }
// }
