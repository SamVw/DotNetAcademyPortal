import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomersComponent } from './pages/customers/customers.component';
import { AdminRouterModule } from './admin-routing.module';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { SideNavComponent } from './components/side-nav/side-nav.component';
import { CustomersTableComponent } from './components/tables/customers-table/customers-table.component';
import {NgbModule, NgbDatepickerModule} from '@ng-bootstrap/ng-bootstrap';
import { NewCustomerModalComponent } from './components/modals/new-customer-modal/new-customer-modal.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DetailCustomerModalComponent } from './components/modals/detail-customer-modal/detail-customer-modal.component';
import { CustomerDetailComponent } from './pages/customer-detail/customer-detail.component';
import { NewParticipantModalComponent } from './components/modals/new-participant-modal/new-participant-modal.component';
import { EditParticipantModalComponent } from './components/modals/edit-participant-modal/edit-participant-modal.component';
import { SharedModule } from 'src/app/shared/shared.module';



@NgModule({
  declarations: [
    CustomersComponent,
    DashboardComponent,
    SideNavComponent,
    CustomersTableComponent,
    NewCustomerModalComponent,
    DetailCustomerModalComponent,
    CustomerDetailComponent,
    NewParticipantModalComponent,
    EditParticipantModalComponent],
  imports: [
    CommonModule,
    AdminRouterModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    NgbDatepickerModule,
    SharedModule
  ],
  entryComponents: [
    NewCustomerModalComponent,
    DetailCustomerModalComponent,
    NewParticipantModalComponent,
    EditParticipantModalComponent
  ]
})
export class AdminModule { }
