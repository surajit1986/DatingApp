import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastServ: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error =>{
        if(error){
          switch (error.status) {
            case 400:
              if(error.error.errors){
                const modelStateError = [];
                for(const key in error.error.errors){
                  if(error.error.errors[key]){
                    modelStateError.push(error.error.errors[key]);
                  }
                }
                throw modelStateError;
              }
              else{
                this.toastServ.error(error.statusText, error.status);
              }

              break;

            case 401:
              this.toastServ.error(error.statusText, error.status);
              break;

            case 404:
              this.router.navigateByUrl('/not-found');
              break;

            case 500:
              const navigationExtras: NavigationExtras = {state: {error: error.error}}
              this.router.navigateByUrl('/server-error', navigationExtras);
              break;

            default:
              this.toastServ.error('something wrong happen!!!');
              console.log(error);
              break;
          }

        }
        return throwError(error);
      })
      )

  }
}
