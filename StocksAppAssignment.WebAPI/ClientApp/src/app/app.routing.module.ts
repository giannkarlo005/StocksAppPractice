import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AllStocksComponent } from './components/all-stocks/all-stocks.component';
import { LoginComponent } from './components/login/login.component';
import { OrderListComponent } from './components/order-list/order-list.component';
import { PopularStocksComponent } from './components/popular-stocks/popular-stocks.component';
import { RegisterComponent } from './components/register/register.component';
import { TradeComponent } from './components/trade/trade.component';

const routes: Routes = [
  { path: 'stocks', component: AllStocksComponent },
  { path: 'order/:stockSymbol', component: OrderListComponent },
  { path: 'popular-stocks', component: PopularStocksComponent },
  { path: 'trade/:stockSymbol', component: TradeComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
