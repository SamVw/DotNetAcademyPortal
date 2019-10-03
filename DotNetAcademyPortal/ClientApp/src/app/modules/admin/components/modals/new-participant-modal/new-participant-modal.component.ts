import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, EmailValidator } from '@angular/forms';
import { NgbActiveModal, NgbDate, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-new-participant-modal',
  templateUrl: './new-participant-modal.component.html',
  styleUrls: ['./new-participant-modal.component.css']
})
export class NewParticipantModalComponent implements OnInit {

  form: FormGroup = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.maxLength(32)]),
    startDate: new FormControl('', [Validators.required]),
    endDate: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email, Validators.maxLength(32)]),
  });

  minDate: NgbDate;
  maxDate: NgbDate;

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit() {
  }

  onDateChange(event: NgbDate) {
    const startDate: NgbDate = this.form.get('startDate').value;
    const endDate: NgbDate = this.form.get('endDate').value;

    if (startDate.day === event.day && startDate.month === event.month && startDate.year === event.year) {
      this.minDate = new NgbDate(startDate.year, startDate.month, startDate.day + 1);
    }

    if (endDate.day === event.day && endDate.month === event.month && endDate.year === event.year) {
      this.maxDate = new NgbDate(endDate.year, endDate.month, endDate.day - 1);
    }
  }

}
