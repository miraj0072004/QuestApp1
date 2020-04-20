import { Component } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  model: any = {};

  constructor(private authService: AuthService,
              private alertifyService: AlertifyService,
              private router: Router ) {  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  login() {
    this.authService.login(this.model).subscribe(
      next => {
        this.alertifyService.success('Logged in successfully');
      },
      error => 
      {
        this.alertifyService.error(error);
      }
      );
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logOut()
  {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.currentUser = null;
    this.authService.decodedToken = null;
    //console.log('logged out');
    this.alertifyService.message('logged out');
    this.router.navigate(['/']);
  }
}
