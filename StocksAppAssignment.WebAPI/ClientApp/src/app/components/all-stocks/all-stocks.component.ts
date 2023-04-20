import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { UsExchange } from '../../models/us-exchange';
import { StocksService } from '../../services/stocks.service';

@Component({
  selector: 'app-all-stocks',
  templateUrl: './all-stocks.component.html',
  styleUrls: ['./all-stocks.component.css']
})
export class AllStocksComponent implements OnInit {
  stocks: UsExchange[] = [];
  isShowLoadMoreButtonShown: boolean = false;
  start: number = 0;
  end: number = 0;

  private maxLength: number = 25;

  constructor(private _router: Router,
              private _stocksService: StocksService) {
  }

  ngOnInit(): void {
    this.fetchStocksList();
  }

  private fetchStocksList(): void {
    this._stocksService.fetchAllStockData().subscribe({
      next: (response: UsExchange[]) => {
        this.stocks = response;
        if (Array.isArray(this.stocks)) {
          this.end = this.stocks.length > this.maxLength ? this.maxLength : this.stocks.length;
          this.isShowLoadMoreButtonShown = true;
        }
      },
      error: (error: Error) => {
        console.log(error);
      },
      complete: () => {
      }
    });
  }

  onLoadMoreButtonClicked(): void {
    if (!this.isShowLoadMoreButtonShown) {
      return;
    }
    if (this.end >= this.stocks.length) {
      this.end = this.stocks.length;
      return;
    }
    this.end += this.maxLength;
  }

  onStockDescriptionClick(stockSymbol: any): void {
    this._router.navigate([`/trade/${stockSymbol}`]);
  }
}
