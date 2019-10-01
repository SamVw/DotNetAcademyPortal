import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {

  loginForm: FormGroup = new FormGroup({
    userName: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });

  @Input()
  error: string;

  @Output()
  submit = new EventEmitter<FormGroup>();

  constructor() { }

  ngOnInit() {
  }

  onSubmit() {
    this.submit.emit(this.loginForm);
  }

}
