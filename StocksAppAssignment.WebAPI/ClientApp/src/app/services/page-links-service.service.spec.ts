import { TestBed } from '@angular/core/testing';

import { PageLinksServiceService } from './page-links-service.service';

describe('PageLinksServiceService', () => {
  let service: PageLinksServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PageLinksServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
