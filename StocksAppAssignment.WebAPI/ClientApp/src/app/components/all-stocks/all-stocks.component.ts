import { Component, OnInit } from '@angular/core';

import { UsExchange } from '../../models/us-exchange';
import { StocksService } from '../../services/stocks.service';

@Component({
  selector: 'app-all-stocks',
  templateUrl: './all-stocks.component.html',
  styleUrls: ['./all-stocks.component.css']
})
export class AllStocksComponent implements OnInit {
  stocks: UsExchange[] = [];

  constructor(private _stocksService: StocksService) {
  }

  ngOnInit(): void {
    this.fetchStocksList();
  }

  private fetchStocksList(): void {
    this._stocksService.fetchAllStockData().subscribe({
      next: (response: UsExchange[]) => {
        this.stocks = response;
      },
      error: (error: Error) => {
        console.log('fetchStocksList');
        console.log(error);
      },
      complete: () => {
      }
    });
  }
}
