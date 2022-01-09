import { TestBed } from '@angular/core/testing';

import { TextSearchService } from './text-search.service';

describe('TextSearchService', () => {
  let service: TextSearchService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TextSearchService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
