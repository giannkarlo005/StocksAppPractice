import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AllStocksComponent } from './components/all-stocks/all-stocks.component';
import { OrderListComponent } from './components/order-list/order-list.component';
import { PopularStocksComponent } from './components/popular-stocks/popular-stocks.component';
import { TradeComponent } from './components/trade/trade.component';

const routes: Routes = [
  { path: 'stocks', component: AllStocksComponent },
  { path: 'order/:stockSymbol', component: OrderListComponent },
  { path: 'popular-stocks', component: PopularStocksComponent },
  { path: 'trade/:stockSymbol', component: TradeComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
