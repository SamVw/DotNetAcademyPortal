import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ParticipantsTableComponent } from './components/participants-table/participants-table.component';



@NgModule({
  declarations: [
    ParticipantsTableComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    ParticipantsTableComponent
  ]
})
export class SharedModule { }
