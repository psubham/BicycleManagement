import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BicycleDetailComponent } from './bicycle-detail.component';

describe('BicycleDetailComponent', () => {
  let component: BicycleDetailComponent;
  let fixture: ComponentFixture<BicycleDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BicycleDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BicycleDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
