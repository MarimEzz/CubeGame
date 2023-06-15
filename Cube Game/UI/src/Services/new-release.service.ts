import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NewReleaseService {

  constructor(private myClient:HttpClient) { }
  private Base_URL = "https://localhost:7121/api/Product/GetAllNewRelease";
  GetSomeNewRelease(): Observable<any[]> {
    return this.myClient.get<any[]>(this.Base_URL)
      .pipe(
        map((response: any[]) => response.slice(0, 5))
      );}

  GetAllNewRelease()
  {
    return this.myClient.get(this.Base_URL);
  }
}
