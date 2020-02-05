import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

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

        const header = new HttpHeaders({
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + data.token
        });

        this.http.get('https://localhost:44337/api/Auth/teszt', {headers: header}).subscribe(
          (message: any) => {
            alert(message.message);
          },
          err => {
            alert(err.message);
          }
        )
      },
      err => {
        this.formData.error = err.error.error;
      }
    );
  }
}
