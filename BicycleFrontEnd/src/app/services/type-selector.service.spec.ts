import { TestBed } from '@angular/core/testing';

import { TypeSelectorService } from './type-selector.service';

describe('TypeSelectorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TypeSelectorService = TestBed.get(TypeSelectorService);
    expect(service).toBeTruthy();
  });
});
