import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliveryeditComponent } from './deliveryedit.component';

describe('DeliveryeditComponent', () => {
  let component: DeliveryeditComponent;
  let fixture: ComponentFixture<DeliveryeditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeliveryeditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeliveryeditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
