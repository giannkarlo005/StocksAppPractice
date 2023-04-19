import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectedStockComponent } from './selected-stock.component';

describe('SelectedStockComponent', () => {
  let component: SelectedStockComponent;
  let fixture: ComponentFixture<SelectedStockComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SelectedStockComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SelectedStockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
