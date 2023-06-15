import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
@Injectable({
  providedIn: 'root'
})
export class FreegamesService {

  constructor(private free:HttpClient) { }
  private Base_URL ='https://localhost:7121/api/Product/GetAllFreeGames';
  GetAllFreeGames()
  {
    return this.free.get(this.Base_URL);
  }
}
