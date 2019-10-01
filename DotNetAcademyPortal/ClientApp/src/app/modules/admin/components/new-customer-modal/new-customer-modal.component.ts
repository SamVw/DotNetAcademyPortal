import { Component, OnInit, Input } from '@angular/core';
import { ModalDismissReasons, NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-new-customer-modal',
  templateUrl: './new-customer-modal.component.html',
  styleUrls: ['./new-customer-modal.component.css']
})
export class NewCustomerModalComponent implements OnInit {

  form: FormGroup = new FormGroup({
    name: new FormControl('', Validators.required),
    address: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    maxParticipants: new FormControl('', [Validators.required, Validators.max(100), Validators.min(1)]),
  });

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit() {
  }
}
