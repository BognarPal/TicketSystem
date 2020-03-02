import { Component, OnInit, NgZone, ViewChild, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../services/authentication.service';
import { NavbarBase } from '../navbar-base';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
})
export class NavbarComponent extends NavbarBase implements OnInit {
 

  constructor(
    authenticationService: AuthenticationService,
    router: Router,
    zone: NgZone)
  {
    super(authenticationService, router, zone);
  }

  

  

  


}
