import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { StockTrade } from '../models/stock-trade';

const API_BASE_URL = 'http://localhost:5204/api/';
@Injectable({
  providedIn: 'root'
})
export class TradeService {
  constructor(private _httpClient: HttpClient) { }

  public getCompanyStockPrice(stockSymbol: string): Observable<StockTrade> {
    return this._httpClient.get<StockTrade>(`${API_BASE_URL}v1/Trade/get-company-stockPrice/${stockSymbol}`);
  }

  public buyOrder(orderRequest: any): Observable<any> {
    let headers = new HttpHeaders({
      'accept': 'application/json'
    });
    return this._httpClient.post<any>(`${API_BASE_URL}v1/Trade/buy-order`,
      orderRequest,
      {
        headers: headers
      });
  }

  public sellOrder(orderRequest: any): Observable<any> {
    let headers = new HttpHeaders();
    return this._httpClient.post<any>(`${API_BASE_URL}v1/Trade/sell-order`,
      orderRequest,
      {
        headers: headers
      });
  }
}
