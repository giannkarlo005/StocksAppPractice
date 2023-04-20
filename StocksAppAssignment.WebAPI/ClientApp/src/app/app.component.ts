import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';

import { PageLinksServiceService } from './services/page-links-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  pageLinks: PageLinksServiceService;
  paramsSubscription: any;
  routerLinkStocksList: string = "/stocks";

  constructor(private _pageLinksService: PageLinksServiceService,
              private _router: Router,
              private _title: Title) {
    this.pageLinks = _pageLinksService;
  }

  ngOnInit() {
    this._title.setTitle("Home Page");
  }

  onStockListLinkClick(): void {
    this._router.navigate(['/stocks']);
  }

  onPopularStocksListCLicked(): void {
    this._router.navigate(['/popular-stocks']);
  }

  onTradeLinkClick(): void {
    const stockSymbol = this._pageLinksService.getStockSymbol();
    this._router.navigate([`/trade/${stockSymbol}`]);
  }

  onOrdersLinkClick(): void {
    const stockSymbol = this._pageLinksService.getStockSymbol();
    this._router.navigate([`/order/${stockSymbol}`]);
  }
}
