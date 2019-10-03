import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NgbDate, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { IParticipant } from 'src/app/shared/models/IParticipant';

@Component({
  selector: 'app-edit-participant-modal',
  templateUrl: './edit-participant-modal.component.html',
  styleUrls: ['./edit-participant-modal.component.css']
})
export class EditParticipantModalComponent implements OnInit {

  form: FormGroup = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.maxLength(32)]),
    startDate: new FormControl('', [Validators.required]),
    endDate: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email, Validators.maxLength(32)]),
  });

  minDate: NgbDate;
  maxDate: NgbDate;

  _participant: IParticipant;

  public set participant(participant: IParticipant) {
    this._participant = participant;
    const startDate = new Date(this._participant.startDate);
    const endDate = new Date(this._participant.endDate);

    this.form.setValue({
      name: this._participant.name,
      email: this._participant.email,
      startDate: new NgbDate(startDate.getFullYear(), startDate.getMonth() + 1, startDate.getDate()),
      endDate: new NgbDate(endDate.getFullYear(), endDate.getMonth() + 1, endDate.getDate()),
    });

    this.onDateChange();
  }

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit() {
  }

  onDateChange() {
    const startDate: NgbDate = this.form.get('startDate').value;
    const endDate: NgbDate = this.form.get('endDate').value;

    this.minDate = new NgbDate(startDate.year, startDate.month, startDate.day + 1);
    this.maxDate = new NgbDate(endDate.year, endDate.month, endDate.day - 1);
  }

}
