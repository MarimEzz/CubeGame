import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router} from '@angular/router';
import { Observable } from 'rxjs';

import { Injectable, Injector } from '@angular/core';
import { AuthService } from 'src/Services/auth.service';
@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private injector: Injector, private router: Router){};
  canActivate(
   ): boolean {
    let isLoggedIn = this.injector.get(AuthService).IsLoggedIn();
    if (isLoggedIn){
      return true
    } else {
      this.router.navigate(['/Login']);
      return false;
    }
  }

}
