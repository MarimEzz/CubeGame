import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MostPopularService {

  constructor(private pop:HttpClient) { }
  private Base_URL ='https://localhost:7121/api/Product/GetAllMostPopular';
  GetAllMostPopular()
  {
    return this.pop.get(this.Base_URL);
  }
}
