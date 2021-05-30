import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private accountServie: AccountService, private toastServicve : ToastrService){

  }
  canActivate(): Observable<boolean> {
    return this.accountServie.currentUser$.pipe(map(
      (user)=>{
        if(user)  return true;
        this.toastServicve.error("your are not authorized");
       }
    ));
  }

}
