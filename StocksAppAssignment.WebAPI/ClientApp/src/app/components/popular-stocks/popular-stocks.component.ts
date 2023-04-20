import { Component, OnInit } from '@angular/core';

import { CompanyProfile } from '../../models/company-profile';
import { Stock } from '../../models/stock';
import { StocksService } from '../../services/stocks.service';

@Component({
  selector: 'app-popular-stocks',
  templateUrl: './popular-stocks.component.html',
  styleUrls: ['./popular-stocks.component.css']
})
export class PopularStocksComponent implements OnInit {
  popularStocks: Stock[] = [];
  selectedStock: CompanyProfile | null = null;

  constructor(private _stocksService: StocksService) { }

  ngOnInit(): void {
    this.fetchPopularStockData();
  }

  private fetchPopularStockData(): void {
    this._stocksService.fetchPopularStockData().subscribe({
      next: (response: Stock[]) => {
        this.popularStocks = response;
      },
      error: (error: Error) => {

      },
      complete: () => {

      }
    });
  }

  onStockSelected(stockSymbol: any): void {
    this._stocksService.fetchSelectedStockData(stockSymbol).subscribe({
      next: (response: CompanyProfile) => {
        console.log(response);
        this.selectedStock = response;
      },
      error: (error: Error) => {
        console.log(error);
      },
      complete: () => {

      }
    });
  }
}

