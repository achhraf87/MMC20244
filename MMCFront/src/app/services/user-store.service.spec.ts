import { TestBed } from '@angular/core/testing';

import { EtudiantStoreService } from './user-store.service';

describe('EtudiantStoreService', () => {
  let service: EtudiantStoreService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EtudiantStoreService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
