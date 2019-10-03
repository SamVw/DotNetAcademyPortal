import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'src/app/core/services/customer.service';
import { ICustomer } from 'src/app/shared/models/ICustomer';
import { RouterStateSnapshot, Router, ActivatedRoute } from '@angular/router';
import { IParticipant } from 'src/app/shared/models/IParticipant';
import { ParticipantService } from 'src/app/core/services/participant.service';
import { NewParticipantModalComponent } from '../../components/modals/new-participant-modal/new-participant-modal.component';
import { NgbModal, NgbDate } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup } from '@angular/forms';
import { EditParticipantModalComponent } from '../../components/modals/edit-participant-modal/edit-participant-modal.component';

@Component({
  selector: 'app-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.css']
})
export class CustomerDetailComponent implements OnInit {
  customer: ICustomer;
  id: string;
  participants: IParticipant[];
  error: string;

  constructor(
    private customerService: CustomerService,
    private route: ActivatedRoute,
    private router: Router,
    private participantService: ParticipantService,
    private modal: NgbModal) {}

  async ngOnInit() {
    this.route.params.subscribe(params => this.id = params['id']);

    this.customerService.getCustomer(this.id).subscribe(
      res => this.customer = res,
      err => this.router.navigate(['/admin'])
    );

    this.participantService.getParticipantsForCustomer(this.id).subscribe(
      res => this.participants = res,
      err => this.error = err.error
    );
  }

  openModal() {
    this.modal.open(NewParticipantModalComponent, {
      backdrop: 'static'
    }).result.then(
      result => this.addParticipant(result),
      reason => console.log(reason));
  }

  close() {
    this.error = null;
  }

  createParticipant(form: FormGroup) {
    const startDate: NgbDate = form.get('startDate').value;
    const endDate: NgbDate = form.get('endDate').value;

    return {
      email: form.get('email').value,
      name: form.get('name').value,
      startDate: new Date(startDate.year, startDate.month - 1, startDate.day).toLocaleDateString(),
      endDate: new Date(endDate.year, endDate.month - 1, endDate.day).toLocaleDateString(),
    } as IParticipant;
  }

  async addParticipant(form: FormGroup) {
    const participant = this.createParticipant(form);

    try {
      this.participants.push(await this.participantService.addParticipant(participant, this.id).toPromise());
    } catch (error) {
      this.error = error.error;
    }
  }

  maxAmountReached() {
    return this.participants ? this.participants.length >= this.customer.maxAllowedParticipants : true;
  }

  editParticipant(id: number) {
    const ref = this.modal.open(EditParticipantModalComponent);
    ref.componentInstance.participant = this.participants.find(p => p.id === id);
    ref.result.then(form => this.updateParticipant(form, id), reason => reason);
  }

  async updateParticipant(form: FormGroup, id: number) {
    let participant = this.createParticipant(form);
    participant.id = id;

    try {
      participant = await this.participantService.updateParticipant(participant, this.id).toPromise();
      const index = this.participants.findIndex(p => p.id === participant.id);
      this.participants[index] = participant;
    } catch (error) {
      this.error = error.error;
    }
  }
}
