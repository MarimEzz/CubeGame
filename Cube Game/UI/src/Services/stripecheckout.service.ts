import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StripecheckoutService {

  constructor(private client:HttpClient) { }

  makePayment(stripeToken: any) : Observable<any>{
      const url = "http://localhost:5000/checkout";

      return this.client.post<any>(url,{token:stripeToken})

      // return this.client.post<any>(url,{stripeToken})
  }

}
