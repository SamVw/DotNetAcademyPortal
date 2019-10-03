import { Component, OnInit } from '@angular/core';
import { IUser } from 'src/app/shared/models/IUser';
import { AuthenticationService } from 'src/app/core/authentication/authentication.service';
import { ICustomer } from 'src/app/shared/models/ICustomer';
import { CustomerService } from 'src/app/core/services/customer.service';
import { ParticipantService } from 'src/app/core/services/participant.service';
import { IParticipant } from 'src/app/shared/models/IParticipant';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EditCustomerModalComponent } from '../../components/edit-customer-modal/edit-customer-modal.component';

@Component({
  selector: 'app-customer-overview',
  templateUrl: './customer-overview.component.html',
  styleUrls: ['./customer-overview.component.css']
})
export class CustomerOverviewComponent implements OnInit {

  error: string;
  customer: ICustomer;
  participants: IParticipant[];

  constructor(
    private authService: AuthenticationService,
    private customerService: CustomerService,
    private participantService: ParticipantService,
    private modal: NgbModal) {
  }

  ngOnInit() {
    console.log(this.authService.currentUserValue);
    this.customerService.getAuthenticatedCustomer().subscribe(
      res => this.customer = res,
      err => console.log(err)
    );

    this.participantService.getParticipantsForAuthenticatedCustomer().subscribe(
      res => this.participants = res,
      err => console.log(this.participants)
    );
  }

  close() {
    this.error = null;
  }

  edit(participantId: number) {
    let participant = this.participants.find(p => p.id === participantId);
    const ref = this.modal.open(EditCustomerModalComponent, {
      backdrop: 'static'
    });
    ref.componentInstance.name = participant.name;
    ref.result.then(name => {
      console.log(name);
        participant.name = name;
        this.participantService.updateParticipantName(participant, this.authService.currentUserValue.id).subscribe(
          res => res,
          err => err
        );
      },
      reason => reason);
  }
}
