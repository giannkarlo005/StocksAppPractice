import { Component, Input, OnInit } from '@angular/core';

import { CompanyProfile } from '../../../models/company-profile';

@Component({
  selector: 'app-selected-stock',
  templateUrl: './selected-stock.component.html',
  styleUrls: ['./selected-stock.component.css']
})
export class SelectedStockComponent implements OnInit {
  @Input() selectedStock: CompanyProfile = new CompanyProfile();

  constructor() {
    console.log('SelectedStockComponent');
  }

  ngOnInit(): void {
    console.log(this.selectedStock);
  }
}
