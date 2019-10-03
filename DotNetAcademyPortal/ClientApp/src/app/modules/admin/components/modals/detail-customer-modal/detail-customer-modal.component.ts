import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ICustomer } from 'src/app/shared/models/ICustomer';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-detail-customer-modal',
  templateUrl: './detail-customer-modal.component.html',
  styleUrls: ['./detail-customer-modal.component.css']
})
export class DetailCustomerModalComponent implements OnInit {

  _customer: ICustomer;

  form: FormGroup = new FormGroup({
    address: new FormControl('', [Validators.required, Validators.maxLength(32)]),
    email: new FormControl('', [Validators.required, Validators.email, Validators.maxLength(32)]),
    maxParticipants: new FormControl('', [Validators.required, Validators.max(100), Validators.min(1)]),
  });

  public set customer(customer: ICustomer) {
    this._customer = customer;
    this.form.setValue({
      address: customer.address,
      email: customer.email,
      maxParticipants: customer.maxAllowedParticipants
    });
  }

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit() {
  }

}
