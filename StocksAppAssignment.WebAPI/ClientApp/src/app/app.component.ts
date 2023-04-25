import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';

import { AppService } from './services/app-service';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  pageLinks: AppService;
  paramsSubscription: any;
  routerLinkStocksList: string = "/stocks";

  constructor(private _appService: AppService,
              private _accountService: AccountService,
              private _router: Router,
              private _title: Title
  ) {
    this.pageLinks = _appService;
  }

  ngOnInit() {
    this._title.setTitle("Home");
  }

  onStockListLinkClick(): void {
    this._appService.setOrderLinkVisibility(false);
    this._appService.setTradeLinkVisibility(false);

    this._router.navigate(['/stocks']);
  }

  onPopularStocksListCLicked(): void {
    this._appService.setTradeLinkVisibility(false);
    this._appService.setOrderLinkVisibility(false);

    this._router.navigate(['/popular-stocks']);
  }

  onTradeLinkClick(): void {
    const stockSymbol = this._appService.getStockSymbol();

    if (stockSymbol) {
      this._appService.setTradeLinkVisibility(true);
      this._appService.setOrderLinkVisibility(true);

      this._router.navigate([`/trade/${stockSymbol}`]);
    }
  }

  onOrdersLinkClick(): void {
    const stockSymbol = this._appService.getStockSymbol();

    if (stockSymbol) {
      this._appService.setTradeLinkVisibility(true);
      this._appService.setOrderLinkVisibility(true);

      this._router.navigate([`/order/${stockSymbol}`]);
    }
  }

  onLoginButtonClicked(): void {
    this._appService.setIsUserLoggedIn(false);
    this._router.navigate(['/login']);
  }

  onRegisterButtonClicked(): void {
    this._appService.setIsUserLoggedIn(false);
    this._router.navigate(['/register']);
  }

  onLogoutButtonClicked(): void {
    this._accountService.logoutUser().subscribe({
      next: () => {
        this._appService.setIsUserLoggedIn(false);
        this._router.navigate(['']);
      },
      error: (error: Error) => {
        console.log(error);
      },
      complete: () => {
      }
    });
  }
}
