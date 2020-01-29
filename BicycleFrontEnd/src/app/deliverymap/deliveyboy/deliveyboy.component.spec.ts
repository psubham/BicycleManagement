import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliveyboyComponent } from './deliveyboy.component';

describe('DeliveyboyComponent', () => {
  let component: DeliveyboyComponent;
  let fixture: ComponentFixture<DeliveyboyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeliveyboyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeliveyboyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
