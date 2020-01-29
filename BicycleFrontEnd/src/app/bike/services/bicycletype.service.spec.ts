import { TestBed } from '@angular/core/testing';

import { BicycleTypeService } from './bicycletype.service';

describe('BicycletypeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BicycleTypeService = TestBed.get(BicycleTypeService);
    expect(service).toBeTruthy();
  });
});
