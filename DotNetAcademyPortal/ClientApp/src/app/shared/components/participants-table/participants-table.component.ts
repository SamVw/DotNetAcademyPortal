import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IParticipant } from 'src/app/shared/models/IParticipant';

@Component({
  selector: 'app-participants-table',
  templateUrl: './participants-table.component.html',
  styleUrls: ['./participants-table.component.css']
})
export class ParticipantsTableComponent implements OnInit {

  @Input()
  participants: IParticipant[];

  @Output()
  edit = new EventEmitter<number>();

  constructor() { }

  ngOnInit() {
  }
}
