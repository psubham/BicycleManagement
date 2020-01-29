import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BicycleCardComponent } from './bicycle-card.component';

describe('BicycleCardComponent', () => {
  let component: BicycleCardComponent;
  let fixture: ComponentFixture<BicycleCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BicycleCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BicycleCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
