import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomersComponent } from './pages/customers/customers.component';
import { AdminRouterModule } from './admin-routing.module';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { SideNavComponent } from './components/side-nav/side-nav.component';
import { CustomersTableComponent } from './components/customers-table/customers-table.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { NewCustomerModalComponent } from './components/new-customer-modal/new-customer-modal.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DetailCustomerModalComponent } from './components/detail-customer-modal/detail-customer-modal.component';



@NgModule({
  declarations: [
    CustomersComponent,
    DashboardComponent,
    SideNavComponent,
    CustomersTableComponent,
    NewCustomerModalComponent,
    DetailCustomerModalComponent],
  imports: [
    CommonModule,
    AdminRouterModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule
  ],
  entryComponents: [
    NewCustomerModalComponent,
    DetailCustomerModalComponent
  ]
})
export class AdminModule { }
