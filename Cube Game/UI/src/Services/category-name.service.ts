import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CategoryNameService {

  constructor(private category:HttpClient) { }
  private Base_URL ='https://localhost:7121/api/Category';
  GetAllcategoryname()
  {
    return this.category.get(this.Base_URL);
  }
}
