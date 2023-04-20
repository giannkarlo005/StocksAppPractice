import { TestBed } from '@angular/core/testing';

import { PageLinksService } from './page-links-service';

describe('PageLinksServiceService', () => {
  let service: PageLinksService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PageLinksService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
