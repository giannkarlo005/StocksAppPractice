import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { OrderService } from '../../services/order.service';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.css']
})
export class OrderListComponent implements OnInit, OnDestroy {
  paramSubscription: any;
  buyOrders: any[] = [];
  sellOrders: any[] = [];

  constructor(private _orderService: OrderService,
              private _activatedRoute: ActivatedRoute
  ) {
    this.paramSubscription = this._activatedRoute.paramMap.subscribe((params) => {
      const stockSymbol = params.get('stockSymbol')?.toString();

      if (stockSymbol) {
        this.getOrders(stockSymbol);
      }
    });
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    if (this.paramSubscription) {
      this.paramSubscription.unsubscribe();
    }
  }

  computeTradeAmount(orderQuantity: number, orderPrice: number): number {
    return Math.round(orderPrice * orderQuantity);
  }

  formatDateTime(dateTime: Date): string{
    if (dateTime == null) {
      return "";
    }
    return "";// dateTime.toString("dd MMMM yyyy HH:mm:ss tt");
  }

  getOrders(stockSymbol: string): void {
    this._orderService.getOrders(stockSymbol).subscribe({
      next: (response: any) => {
        if (!response) {
          return;
        }
        this.buyOrders = response.buyOrders || [];
        this.sellOrders = response.sellOrders || [];
      },
      error: (error: Error) => {
        console.log(error);
      },
      complete: () => {
      }
    });
  }
}
