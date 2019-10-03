import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-edit-customer-modal',
  templateUrl: './edit-customer-modal.component.html',
  styleUrls: ['./edit-customer-modal.component.css']
})
export class EditCustomerModalComponent implements OnInit {

  _nameO: string;
  _nameC = '';
  public set name(name: string) {
    this._nameO = name;
    this._nameC = name;
  }

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit() {
  }

  AnyErrors() {
    return this._nameC.length === 0 || this._nameC.length > 32;
  }

}
