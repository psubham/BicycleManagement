import { HttpInterceptor, HttpRequest, HttpHandler, HttpUserEvent, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { tap, retryWhen } from 'rxjs/operators';
import { UserService } from '../../services/user.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private router: Router,
    private userService: UserService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let user = JSON.parse(this.userService.getUser());
    if(user) {
      const clonedreq = req.clone({
        headers: req.headers.set('Authorization', 'Bearer ' + JSON.parse(user.token))
      });
      console.log(clonedreq);

      return next.handle(clonedreq).pipe(tap(
        succ => { },
        err => {
          if (err.status === 401) {
            this.router.navigateByUrl('/home');
          }
        }
      ));
    }
    return next.handle(req.clone());

  }
}
