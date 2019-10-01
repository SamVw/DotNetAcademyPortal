import { Component, OnInit, Input } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { NewCustomerModalComponent } from '../../components/new-customer-modal/new-customer-modal.component';
import { FormGroup } from '@angular/forms';
import { ICustomer } from 'src/app/shared/models/ICustomer';
import { CustomerService } from 'src/app/core/services/customer.service';
import { DetailCustomerModalComponent } from '../../components/detail-customer-modal/detail-customer-modal.component';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnInit {

  error: string;
  customers: ICustomer[];

  constructor(private modal: NgbModal, private customerService: CustomerService) { }

  ngOnInit() {
    this.customerService.getCustomers().subscribe(res => this.customers = res);
  }

  openModal() {
    this.modal.open(NewCustomerModalComponent).result.then(
      result => this.addCustomer(result),
      reason => console.log(reason));
  }

  close() {
    this.error = null;
  }

  addCustomer(form: FormGroup) {
    const customer: ICustomer = {
      address: form.get('address').value,
      name: form.get('name').value,
      email: form.get('email').value,
      maxAllowedParticipants: form.get('maxParticipants').value,
    };

    this.customerService.addCustomer(customer).subscribe(
      res => {
        this.customers.push(customer);
      },
      err => this.error = err.error
    );
  }

  showDetails(id: string) {
    const customer = this.customers.find(c => c.id === id);
    const ref = this.modal.open(DetailCustomerModalComponent);
    ref.componentInstance.customer = customer;
    ref.result.then(
      result => this.SaveChanges(result, customer),
      reason => console.log(reason)
    );
  }

  SaveChanges(result: FormGroup, customer: ICustomer) {
    this.error = null;
    this.customerService.updateCustomer({
      address: result.get('address').value,
      email: result.get('email').value,
      maxAllowedParticipants: result.get('maxParticipants').value,
      id: customer.id,
      name: customer.name
    } as ICustomer).subscribe(
      res =>  {
        const index = this.customers.findIndex(c => c.id);
        this.customers[index] = res;
      },
      err => this.error = err.error
    );
  }
}
