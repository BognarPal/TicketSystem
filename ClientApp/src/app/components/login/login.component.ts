import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';

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

  constructor(private authenticationService: AuthenticationService) { }

  ngOnInit() {
  }

  submit() {
    this.formData.error = '';
    this.formData.loading = true;
    this.authenticationService.login(this.formData.email, this.formData.password).subscribe(
      (data: any) => {
        this.formData.loading = false;
        alert('sikeres login');
      },
      err => {
        this.formData.loading = false;
        this.formData.error = err.error.error;
      }
    );
  }
}
