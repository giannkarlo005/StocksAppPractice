import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  paramsSubscription: any;

  constructor(private _activatedRoute: ActivatedRoute,
              private _router: Router) {
  }

  onStockListLinkClick(): void {
    this._router.navigate(['/stocks']);
  }

  onPopularStocksListCLicked(): void {
  }

  onTradeLinkClick(): void {
  }

  onOrdersLinkClick(): void {
  }
}
