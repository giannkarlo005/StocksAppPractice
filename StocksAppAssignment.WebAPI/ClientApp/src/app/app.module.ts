import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routing.module';
import { AllStocksComponent } from './components/all-stocks/all-stocks.component';
import { PopularStocksComponent } from './components/popular-stocks/popular-stocks.component';
import { OrderListComponent } from './components/order-list/order-list.component';
import { TradeComponent } from './components/trade/trade.component';

@NgModule({
  declarations: [
    AppComponent,
    AllStocksComponent,
    TradeComponent,
    OrderListComponent,
    PopularStocksComponent,
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
