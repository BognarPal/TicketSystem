import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { HttpHeaders, HttpClient } from '@angular/common/http';

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

  constructor(private authenticationService: AuthenticationService, private http: HttpClient) { }

  ngOnInit() {
  }

  submit() {
    this.formData.error = '';
    this.formData.loading = true;
    this.authenticationService.login(this.formData.email, this.formData.password).subscribe(
      (data: any) => {
        this.formData.loading = false;
        alert('sikeres login');

        //védett oldal hívása

        let token = JSON.parse(localStorage.getItem('currentUser')).token;
        const header = new HttpHeaders({
          'Content-Type': 'application/json',
          'Authorization': token
        });

        this.http.get('https://localhost:44337/api/Auth/teszt', { headers: header }).subscribe(
          (message: any) => {
            alert(message.message);
          },
          err => {
            alert(err.message);
          })


      },
      err => {
        this.formData.loading = false;
        this.formData.error = err.error.error;
      }
    );
  }
}
