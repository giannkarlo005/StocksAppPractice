import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { LoginUserData } from '../models/login-user-data';
import { RegisterUserData } from '../models/register-user-data';

const API_BASE_URL = 'http://localhost:5204/api/';
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  constructor(private _httpClient: HttpClient) {
  }

  registerUser(registerData: RegisterUserData): Observable<any> {
    let headers = new HttpHeaders({
      'accept': 'application/json'
    });
    return this._httpClient.post<any>(`${API_BASE_URL}v1/Account/register`,
      registerData,
      { headers: headers });
  }

  loginUser(loginData: LoginUserData): Observable<any> {
    let headers = new HttpHeaders({
      'accept': 'application/json'
    });
    return this._httpClient.post<any>(`${API_BASE_URL}v1/Account/login`,
      loginData,
      { headers: headers });
  }

  logoutUser(): Observable<void> {
    return this._httpClient.get<void>(`${API_BASE_URL}v1/Account/logout`);
  }
}
