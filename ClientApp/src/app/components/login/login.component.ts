import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formData = {
    email: '',
    password: '',
    error: '',
    loading: false
  };
  returnUrl = '/';

  constructor(
    private authenticationService: AuthenticationService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    if (this.authenticationService.currentUser) {
      this.router.navigate(['/']);
    } else {
      this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }
  }

  submit() {
    this.formData.error = '';
    this.formData.loading = true;
    this.authenticationService.login(this.formData.email, this.formData.password).subscribe(
      data => this.router.navigate([this.returnUrl]),
      err => {
        this.formData.loading = false;
        this.formData.error = err.error.error;
      }
    );
  }
}
