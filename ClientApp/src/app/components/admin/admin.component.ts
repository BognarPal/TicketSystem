import { Component, OnInit, NgZone } from '@angular/core';
import { NavbarBase } from '../navbar-base';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html'  
})
export class AdminComponent extends NavbarBase implements OnInit {

  constructor(
    authenticationService: AuthenticationService,
    router: Router,
    zone: NgZone) {
    super(authenticationService, router, zone);
  }
 

}
