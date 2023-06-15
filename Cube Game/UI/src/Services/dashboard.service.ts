import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

   // Category
   private baseURL = "https://localhost:7121/api/Category"
   constructor(private client: HttpClient) { }

   GetAllCategories(){
     return this.client.get<any>(`${this.baseURL}`)
   }

   addCategory(categoryObj : any){
     return this.client.post<any>(`${this.baseURL}` , categoryObj)
   }

   UpdateCategory(id : number , categoryObj : any){
     const url = `${this.baseURL}/${id}`;
     return this.client.put(url, categoryObj)
   }

   deleteCategory(id : any){
     return this.client.delete<any>(`${this.baseURL}/${id}`)
   }


   // Product
   private baseURLP = "https://localhost:7121/api/Product"

   GetAllProducts(){
     return this.client.get<any>(`${this.baseURLP}`)
   }

   GetAllProductsWithoutImages(){
     return this.client.get<any>(`${this.baseURLP}/ProductsWithoutImages`)
   }

   addProduct(productObj: any) {
    return this.client.post<any>(`${this.baseURLP}`, productObj)
      .pipe(
        catchError(error => {
          console.error('Error adding product:', error);
          throw error; // Rethrow the error to propagate it to the calling component
        })
      );
  }

   UpdateProduct(id : number , productObj : any){
     const url = `${this.baseURLP}/${id}`;
     return this.client.put(url, productObj)
   }

   deleteProduct(id : any){
     return this.client.delete<any>(`${this.baseURLP}/DeleteProduct/${id}`)
   }

   AddImage(id : any , file : any){
     return this.client.post<any>(`${this.baseURLP}/AddImage/${id}`, file)
   }
}
