import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  private baseURL = "https://localhost:7121/api/Order/GetUser"
  constructor(private http : HttpClient) {}

  //getUserDetails

  getUserDetails(){
    return this.http.get(`${this.baseURL}`)
  }
}
