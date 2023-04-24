import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const API_BASE_URL = 'http://localhost:5204/api/';
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  constructor(private httpClient: HttpClient) {
  }

  registerUser(registerData: any): Observable<any> {
    let headers = new HttpHeaders({
      'accept': 'application/json'
    });
    return this.httpClient.post<any>(`${API_BASE_URL}/v1/Account/register`,
      registerData,
      { headers: headers });
  }
}
