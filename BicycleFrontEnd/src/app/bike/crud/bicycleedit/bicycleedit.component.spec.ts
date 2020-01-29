import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BicycleeditComponent } from './bicycleedit.component';

describe('BicycleeditComponent', () => {
  let component: BicycleeditComponent;
  let fixture: ComponentFixture<BicycleeditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BicycleeditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BicycleeditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
