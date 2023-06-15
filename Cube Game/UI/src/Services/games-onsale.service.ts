import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GamesOnsaleService {

  constructor(private sale:HttpClient) { }
  private Base_URL ='https://localhost:7121/api/Product/GetAllGameOnSale';
  GetAllGamesOnSale()
  {
    return this.sale.get(this.Base_URL);
  }
}
