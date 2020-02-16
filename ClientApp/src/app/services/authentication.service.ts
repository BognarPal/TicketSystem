// elsődleges forrás: https://jasonwatmore.com/post/2019/06/22/angular-8-jwt-authentication-example-tutorial

import { Injectable, EventEmitter } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '@environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private currentUserChanged = new EventEmitter();

  constructor(private http: HttpClient) {
    this.checktoken();
  }

  public get currentUser(): User {
    const usr = new User();
    usr.loadFromLocalStorage();
    if (usr.id) {
      return usr;
    }
    return null;
  }

  login(email: string, password: string) {
    return this.http.post(`${environment.apiUrl}/Auth/Login`, { email, password })
      .pipe(map(
        (user: any) => {
          if (user && user.token) {
            localStorage.setItem('currentUser', JSON.stringify(user));
            this.currentUserChanged.emit(this.currentUser);
          }
          return user;
        }
      ));
  }

  logout() {
    this.currentUserChanged.emit(null);
    localStorage.removeItem('currentUser');
  }

  checktoken() {
    if (this.currentUser) {
      const header = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': this.currentUser.token
      });

      this.http.get(`${environment.apiUrl}/Auth/checktoken`, { headers: header }).subscribe(
        message => this.currentUserChanged.emit(this.currentUser),
        err => this.logout()
      );
    }
  }
}
