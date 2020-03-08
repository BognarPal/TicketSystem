import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) { }

  getList(url: string): Observable<any> {
    return this.http.get<any[]>(`${environment.apiUrl}/admin/${url}`)
      .pipe(map(data => {
        return data;
      }));
  }

  getPartner(id: number): Observable<any> {
    return this.http.get<any>(`${environment.apiUrl}/admin/partner/${id}`)
      .pipe(map(data => {
        return data;
      }));
  }

  savePartner(url: string, partner:any): Observable<any> {
    return this.http.post<any[]>(`${environment.apiUrl}/admin/${url}`, partner)
      .pipe(map(data => {
        return data;
      }));
  }
}
