import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { UsExchange } from '../models/us-exchange';
import { Stock } from '../models/stock';

const API_BASE_URL = 'http://localhost:5204/api/';
@Injectable({
  providedIn: 'root'
})
export class StocksService {
  constructor(private _httpClient: HttpClient) { }

  public fetchAllStockData(): Observable<UsExchange[]> {
    return this._httpClient.get<UsExchange[]>(`${API_BASE_URL}v1/Trade/get-all-stocks`);
  }

  public fetchPopularStockData(): Observable<Stock[]> {
    return this._httpClient.get<Stock[]>(`${API_BASE_URL}v1/Stocks/explore`);
  }
}
