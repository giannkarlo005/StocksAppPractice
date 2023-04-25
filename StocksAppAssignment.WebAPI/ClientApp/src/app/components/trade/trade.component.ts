import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Title } from '@angular/platform-browser';

import { StockTrade } from '../../models/stock-trade';
import { AppService } from '../../services/app-service';
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
              private _router: Router,
              private _title: Title,
              private _appService: AppService,
              private _tradeService: TradeService
   ) {
    this._title.setTitle("Trade");
    this.paramsSubscription = this._activatedRoute.paramMap.subscribe((params) => {
      const stockSymbol = params.get('stockSymbol')?.toString();

      if (stockSymbol) {
        this._appService.setOrderLinkVisibility(true);
        this._appService.setTradeLinkVisibility(true);
        this._appService.setStockSymbol(stockSymbol);

        this.getCompanyStockPrice(stockSymbol);
      }
    });
  }

  ngOnInit() {
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
      },
      error: (error: Error) => {
        console.log(error);
      },
      complete: () => {

      }
    });
  }

  public onBuyOrderClick(): void {
    const orderQuantity = (document.getElementById('order-quantity') as HTMLInputElement).value;
    if (!orderQuantity) {
      return;
    }

    const confirmMessage = `You are about to buy stocks from ${this.stockTrade.stockName}. Proceed?`
    if (confirm(confirmMessage)) {
      let orderRequest = this.createOrderRequest(parseFloat(orderQuantity));
      this._tradeService.buyOrder(orderRequest).subscribe({
        next: (response: any) => {
          this._router.navigate([`/order/${this.stockTrade.stockSymbol}`]);
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

  public onSellOrderClick(): void {
    const orderQuantity = (document.getElementById('order-quantity') as HTMLInputElement).value;
    if (!orderQuantity) {
      return;
    }

    const confirmMessage = `You are about to sell stocks from ${this.stockTrade.stockName}. Proceed?`
    if (confirm(confirmMessage)) {
      let orderRequest = this.createOrderRequest(parseFloat(orderQuantity));
      this._tradeService.sellOrder(orderRequest).subscribe({
        next: (response: any) => {
          this._router.navigate([`/order/${this.stockTrade.stockSymbol}`]);
        },
        error: (error: Error) => {
          console.log(error);
        },
        complete: () => {

        }
      });
    }
  }
}
