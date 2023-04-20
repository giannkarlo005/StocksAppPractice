import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';

import { PageLinksService } from './services/page-links-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  pageLinks: PageLinksService;
  paramsSubscription: any;
  routerLinkStocksList: string = "/stocks";

  constructor(private _pageLinksService: PageLinksService,
              private _router: Router,
              private _title: Title
  ) {
    this.pageLinks = _pageLinksService;
  }

  ngOnInit() {
    this._title.setTitle("Home");
  }

  onStockListLinkClick(): void {
    this._pageLinksService.setOrderLinkVisibility(false);
    this._pageLinksService.setTradeLinkVisibility(false);

    this._router.navigate(['/stocks']);
  }

  onPopularStocksListCLicked(): void {
    this._pageLinksService.setTradeLinkVisibility(false);
    this._pageLinksService.setOrderLinkVisibility(false);

    this._router.navigate(['/popular-stocks']);
  }

  onTradeLinkClick(): void {
    const stockSymbol = this._pageLinksService.getStockSymbol();

    if (stockSymbol) {
      this._pageLinksService.setTradeLinkVisibility(true);
      this._pageLinksService.setOrderLinkVisibility(true);

      this._router.navigate([`/trade/${stockSymbol}`]);
    }
  }

  onOrdersLinkClick(): void {
    const stockSymbol = this._pageLinksService.getStockSymbol();

    if (stockSymbol) {
      this._pageLinksService.setTradeLinkVisibility(true);
      this._pageLinksService.setOrderLinkVisibility(true);

      this._router.navigate([`/order/${stockSymbol}`]);
    }
  }
}
