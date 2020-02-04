import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formData = {
    email: '',
    password: '',
    error: ''
  };

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  submit() {
    this.formData.error = '';
    this.http.post('https://localhost:44337/api/Auth/Login', this.formData).subscribe(
      (data: any) => {
        localStorage.setItem('currentUser', JSON.stringify(data));
        console.log(data);
      },
      err => {
        this.formData.error = err.error.error;
      }
    );
  }
}
