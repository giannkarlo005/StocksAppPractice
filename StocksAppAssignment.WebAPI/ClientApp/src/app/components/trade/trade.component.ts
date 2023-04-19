import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { StockTrade } from '../../models/stock-trade';
import { TradeService } from '../../services/trade.service';

@Component({
  selector: 'app-order',
  templateUrl: './trade.component.html',
  styleUrls: ['./trade.component.css']
})
export class TradeComponent implements OnInit, OnDestroy {
  paramsSubscription: any;

  stockTrade: StockTrade = new StockTrade();

  constructor(private _activatedRoute: ActivatedRoute,
              private _tradeService: TradeService) { }

  ngOnInit(): void {
    this.paramsSubscription = this._activatedRoute.paramMap.subscribe((params) => {
      const stockSymbol = params.get('stockSymbol')?.toString();

      if (stockSymbol) {
        this.getCompanyStockPrice(stockSymbol);
      }
    });
  }

  ngOnDestroy(): void {
    if (this.paramsSubscription) {
      this.paramsSubscription.unsubscribe();
    }
  }

  private createOrderRequest(orderQuantity: number): any {
    let orderRequest = {
      stockName: this.stockTrade.stockName,
      stockSymbol: this.stockTrade.stockSymbol,
      orderQuantity: orderQuantity || 0.0,
      orderPrice: this.stockTrade.price || 0.0,
      dateAndTimeOfOrder: null
    };

    return orderRequest;
  }

  getCompanyStockPrice(stockSymbol: string): void {
    this._tradeService.getCompanyStockPrice(stockSymbol).subscribe({
      next: (response: StockTrade) => {
        this.stockTrade = response;
        console.log(response);
      },
      error: (error: Error) => {
        console.log(error);
      },
      complete: () => {

      }
    });
  }

  public onBuyOrderClick(): void {
    console.log("onBuyOrderClick");
    const orderQuantity = (document.getElementById('order-quantity') as HTMLInputElement).value;
    if (!orderQuantity) {
      return;
    }

    let orderRequest = this.createOrderRequest(parseFloat(orderQuantity));
    this._tradeService.buyOrder(orderRequest).subscribe({
      next: (response: any) => {
        console.log(response);
      },
      error: (error: Error) => {
        console.log(error);
      },
      complete: () => {

      }
    });
  }

  public onSellOrderClick(): void {
    const orderQuantity = (document.getElementById('order-quantity') as HTMLInputElement).value;
    if (!orderQuantity) {
      return;
    }

    let orderRequest = this.createOrderRequest(parseFloat(orderQuantity));
    this._tradeService.sellOrder(orderRequest).subscribe({
      next: (response: any) => {
        console.log(response);
      },
      error: (error: Error) => {
        console.log(error);
      },
      complete: () => {

      }
    });
  }
}
