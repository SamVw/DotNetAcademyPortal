import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewCustomerModalComponent } from './new-customer-modal.component';

describe('NewCustomerModalComponent', () => {
  let component: NewCustomerModalComponent;
  let fixture: ComponentFixture<NewCustomerModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewCustomerModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewCustomerModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
