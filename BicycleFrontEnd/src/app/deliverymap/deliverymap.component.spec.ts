import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliverymapComponent } from './deliverymap.component';

describe('DeliverymapComponent', () => {
  let component: DeliverymapComponent;
  let fixture: ComponentFixture<DeliverymapComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeliverymapComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeliverymapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
