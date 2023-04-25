import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { CompanyProfile } from '../../../models/company-profile';
import { AppService } from '../../../services/app-service';

@Component({
  selector: 'app-selected-stock',
  templateUrl: './selected-stock.component.html',
  styleUrls: ['./selected-stock.component.css']
})
export class SelectedStockComponent implements OnInit {
  @Input() selectedStock: CompanyProfile = new CompanyProfile();

  constructor(private _appService: AppService,
              private _router: Router) {
  }

  ngOnInit(): void {
  }

  onTradeNowButtonClicked(): void {
    const stockSymbol = this._appService.getStockSymbol();

    if (stockSymbol) {
      this._appService.setTradeLinkVisibility(true);
      this._appService.setOrderLinkVisibility(true);

      this._router.navigate([`/trade/${stockSymbol}`]);
    }
  }
}
