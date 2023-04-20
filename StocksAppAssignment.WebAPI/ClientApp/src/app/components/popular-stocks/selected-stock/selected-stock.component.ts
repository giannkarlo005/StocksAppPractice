import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { CompanyProfile } from '../../../models/company-profile';
import { PageLinksService } from '../../../services/page-links-service';

@Component({
  selector: 'app-selected-stock',
  templateUrl: './selected-stock.component.html',
  styleUrls: ['./selected-stock.component.css']
})
export class SelectedStockComponent implements OnInit {
  @Input() selectedStock: CompanyProfile = new CompanyProfile();

  constructor(private _pageLinksService: PageLinksService,
              private _router: Router) {
  }

  ngOnInit(): void {
  }

  onTradeNowButtonClicked(): void {
    const stockSymbol = this._pageLinksService.getStockSymbol();

    if (stockSymbol) {
      this._pageLinksService.setTradeLinkVisibility(true);
      this._pageLinksService.setOrderLinkVisibility(true);

      this._router.navigate([`/trade/${stockSymbol}`]);
    }
  }
}
