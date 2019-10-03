import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerRouterModule } from './customer-routing.module';
import { CustomerOverviewComponent } from './pages/customer-overview/customer-overview.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from 'src/app/shared/shared.module';
import { EditCustomerModalComponent } from './components/edit-customer-modal/edit-customer-modal.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [CustomerOverviewComponent, EditCustomerModalComponent],
  imports: [
    CommonModule,
    CustomerRouterModule,
    NgbModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule
  ],
  entryComponents: [
    EditCustomerModalComponent
  ]
})
export class CustomerModule { }
