import { Injectable, Injector } from '@angular/core';
import { CanActivate, CanActivateFn, Router } from '@angular/router';
import { AuthService } from 'src/Services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class adminGuard implements CanActivate {
  constructor(private injector: Injector, private router: Router , private auth : AuthService){};
  canActivate(
   ): boolean {

    if (this.auth.getRoleFromToken()==='Admin'){
      return true
    } else {
      return false;
    }
  }

}
