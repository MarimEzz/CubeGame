import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private baseURL = "https://localhost:7121/api/Order"

  constructor(private http : HttpClient) {}

  // GetAllOrders - Admin
  GetAllOrder(){
    return this.http.get(`${this.baseURL}/GetAllOrders`)
  }

  GetOrderForUser(){
    return this.http.get(`${this.baseURL}/GetUserOrder`)
  }

  AddOrder(){
    return this.http.post(`${this.baseURL}/AddOrder`, {})
  }
}
