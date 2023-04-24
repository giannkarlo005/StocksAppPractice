import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const API_BASE_URL = 'http://localhost:5204/api/';
@Injectable({
  providedIn: 'root'
})
export class OrderService {
  constructor(private _httpClient: HttpClient) { }

  public getOrders(stockSymbol: string): Observable<any> {
    return this._httpClient.get<any>(`${API_BASE_URL}v1/Trade/get-orders/${stockSymbol}`);
  }

  public viewPDF(stockSymbol: string): Observable<any> {
    return this._httpClient.get<any>(`${API_BASE_URL}v1/Trade/orders-pdf/${stockSymbol}`);
  }
}
