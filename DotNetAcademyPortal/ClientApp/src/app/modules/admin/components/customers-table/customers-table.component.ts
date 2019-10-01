import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ICustomer } from 'src/app/shared/models/ICustomer';

@Component({
  selector: 'app-customers-table',
  templateUrl: './customers-table.component.html',
  styleUrls: ['./customers-table.component.css']
})
export class CustomersTableComponent implements OnInit {

  @Input()
  customers: ICustomer[];

  @Output()
  show = new EventEmitter<string>();

  constructor() { }

  ngOnInit() {
  }

  showDetails(id: string) {
    this.show.emit(id);
  }

}
