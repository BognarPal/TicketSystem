import { Component, OnInit, NgZone, ViewChild, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
})
export class NavbarComponent implements OnInit {
  @ViewChild('labelTimeOut', { static: false }) labelTimeOut: ElementRef;
  timeToTimeout = '';

  constructor(
    public authenticationService: AuthenticationService,
    private router: Router,
    private zone: NgZone) { }

  ngOnInit() {
    this.authenticationService.currentUserChanged.subscribe(
      usr => {
        if (!usr) {
          this.router.navigate(['/login']);
        }
      });
    this.authenticationService.checktoken();
    this.zone.runOutsideAngular( () =>
      setInterval(() => this.printLoginTimeout(), 1000));
  }

  logout() {
    this.authenticationService.logout();
  }

  printLoginTimeout() {
    if (this.authenticationService.currentUser) {
      const difInSeconds = Math.floor((this.authenticationService.currentUser.validTo.getTime() - new Date().getTime()) / 1000);
      this.timeToTimeout = '(' +
        ('00' + Math.floor(difInSeconds / 60).toString()).slice(-2) +
        ':' +
        ('00' + Math.floor(difInSeconds % 60).toString()).slice(-2) + ')';

      if (difInSeconds < 0) {
        this.zone.run( () => this.logout());
      }
    } else {
      this.timeToTimeout = '';
    }
    try {
      this.labelTimeOut.nativeElement.innerHTML = this.timeToTimeout;
    }
    catch (e) { }
  }


}
