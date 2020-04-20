import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from '../_models/user';

@Injectable()
export class AuthService {

  baseUrl = environment.apiUrl+'auth/';
  decodedToken: any;
  currentUser: User;
  jwtHelper = new JwtHelperService();

    constructor(private http: HttpClient) {}


    login(model: any) {
      return this.http.post(this.baseUrl + "login", model).
        pipe(
          map((response: any) => {
            const user = response;
            if (user) {
              localStorage.setItem('token', user.token);
              localStorage.setItem('user', JSON.stringify(user.user));
              this.decodedToken = this.jwtHelper.decodeToken(user.token);
              this.currentUser = user.user;
            }

          })
      );
  }


    loggedIn() {
      const token = localStorage.getItem('token');
      return !this.jwtHelper.isTokenExpired(token);
    }
}
