import { OnInit, NgZone, ElementRef, ViewChild } from "@angular/core";
import { AuthenticationService } from "../services/authentication.service";
import { Router } from "@angular/router";

export abstract class NavbarBase implements OnInit {
  @ViewChild('labelTimeOut', { static: false }) labelTimeOut: ElementRef;
  timeToTimeout = '';

  constructor(
    public authenticationService: AuthenticationService,
    protected router: Router,
    protected zone: NgZone) { }


  ngOnInit() {
    this.authenticationService.currentUserChanged.subscribe(
      usr => {
        if (!usr) {
          this.router.navigate(['/login']);
        }
      });
    this.authenticationService.checktoken();
    this.zone.runOutsideAngular(() =>
      setInterval(() => this.printLoginTimeout(), 1000));
  }

  printLoginTimeout() {
    if (this.authenticationService.currentUser) {
      const difInSeconds = Math.floor((this.authenticationService.currentUser.validTo.getTime() - new Date().getTime()) / 1000);
      this.timeToTimeout = '(' +
        ('00' + Math.floor(difInSeconds / 60).toString()).slice(-2) +
        ':' +
        ('00' + Math.floor(difInSeconds % 60).toString()).slice(-2) + ')';

      if (difInSeconds < 0) {
        this.zone.run(() => this.logout());
      }
    } else {
      this.timeToTimeout = '';
    }
    try {
      this.labelTimeOut.nativeElement.innerHTML = this.timeToTimeout;
    }
    catch (e) { }
  }

  logout() {
    this.authenticationService.logout();
  }
}
