import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse } from "@angular/common/http";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";


@Injectable()
export class AuthInterceptor implements HttpInterceptor {


  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const usr = JSON.parse(localStorage.getItem('currentUser'));
    if (usr && usr.token) {
      const cloned = req.clone({
        setHeaders: { Authorization: usr.token }
      });
      return next.handle(cloned)
        .pipe(
          tap((event: HttpEvent<any>) => {
            if (event instanceof HttpResponse && event.status === 200) {
              const user = (event.headers.get('session'));
              if (user) {
                localStorage.setItem('currentUser', user);
              }
            }
            return event;
          })
        );
    }
    return next.handle(req);
  }




}
