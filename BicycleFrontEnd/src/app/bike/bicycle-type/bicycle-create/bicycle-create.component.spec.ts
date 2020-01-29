import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BicycleCreateComponent } from './bicycle-create.component';

describe('BicycleCreateComponent', () => {
  let component: BicycleCreateComponent;
  let fixture: ComponentFixture<BicycleCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BicycleCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BicycleCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
