import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {JwtHelperService} from '@auth0/angular-jwt'

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private userPayload : any
  private baseURL = "https://localhost:7121/api/Auth/"
  constructor(private http : HttpClient) {
    this.userPayload = this.decodedToken();
  }

  signUp(userObj : any){
    return this.http.post<any>(`${this.baseURL}register` , userObj )
  }

  Login(userObj : any){
    return this.http.post<any>(`${this.baseURL}token` , userObj)
  }
  logOut(){
    localStorage.clear();
  }

  storeToken(tokenValue : string){
    localStorage.setItem('token' , tokenValue)
  }

  getToken(){
   return localStorage.getItem('token')
  }

  IsLoggedIn() : boolean{
    return !!localStorage.getItem('token')
  }

  decodedToken(){
    const jwtHelper = new JwtHelperService();
    const token = this.getToken()!;
    console.log(jwtHelper.decodeToken(token));
    return jwtHelper.decodeToken(token);
  }

  getFullNameFromToken(){
    if(this.userPayload){
      return this.userPayload.sub;
    }
  }

  getRoleFromToken(){

    var roles = this.decodedToken().roles;
    if(roles === "Admin"){
      return roles
    }
  }

}
