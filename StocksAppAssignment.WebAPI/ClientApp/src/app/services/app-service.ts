import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AppService {
  //page links
  private isTradeLinkVisible: boolean = false;
  private isOrderLinkVisible: boolean = false;
  private stockSymbol: string = "";

  //log in status
  private isUserLoggedIn: boolean = false;

  constructor() { }

  //Stock Symbol
  public getStockSymbol(): string {
    return this.stockSymbol;
  }

  public setStockSymbol(stockSymbol: string): void {
    this.stockSymbol = stockSymbol;
  }

  //Trade Link Visibility
  public getTradeLinkVisibility(): boolean {
    return this.isTradeLinkVisible;
  }

  public setTradeLinkVisibility(isVisibile: boolean): void {
    this.isTradeLinkVisible = isVisibile;
  }

  //Order Link Visibility
  public getOrderLinkVisibility(): boolean {
    return this.isOrderLinkVisible;
  }

  public setOrderLinkVisibility(isVisibile: boolean): void {
    this.isOrderLinkVisible = isVisibile;
  }

  //Is User Logged In
  public getIsUserLoggedIn(): boolean {
    return this.isUserLoggedIn;
  }

  public setIsUserLoggedIn(isUserLoggedIn: boolean): void {
    this.isUserLoggedIn = isUserLoggedIn;
  }
}
