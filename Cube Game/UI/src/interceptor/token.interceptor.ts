import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { AuthService } from 'src/Services/auth.service';
import { NgToastComponent, NgToastService } from 'ng-angular-popup';
import { Router } from '@angular/router';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private myService : AuthService , private toast : NgToastService , private router : Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    const myToken = this.myService.getToken();
      if(myToken){

        // Edit the request header to send token
        request = request.clone({
          setHeaders : {Authorization : `Bearer ${myToken}`}
        })
      }
    return next.handle(request).pipe(
      catchError((err : any)=>{
        if(err instanceof HttpResponse){
          if(err.status === 401 ){
            this.toast.warning({detail:"Warning",summary:'Login Again ',duration:5000});
            this.router.navigate(['Login']);
          }
        }
        // return throwError(()=>  new Error("some Error occured"))

        console.log(err); // log the error object
        return throwError(err); // return the error object
      })
    );
  }
}
