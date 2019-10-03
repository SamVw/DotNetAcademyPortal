import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { AuthenticationService } from 'src/app/core/authentication/authentication.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  error: string;
  returnUrl: string;
  location: string;

  constructor(private route: ActivatedRoute, private authService: AuthenticationService, private router: Router) {
    if (this.authService.currentUserValue) {
      this.location = this.authService.currentUserValue.isAdmin ? 'admin' : 'customer';
      this.router.navigate(['/' + this.location]);
    }
  }

  ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || null;
  }

  onSubmit(form: FormGroup) {
    console.log(form.get('userName').value);
    console.log(form.get('password').value);
    const userName = form.get('userName').value;
    const password = form.get('password').value;
    this.error = null;
    this.authService.login(userName, password)
      .subscribe(res => {
        this.location = this.authService.currentUserValue.isAdmin ? 'admin' : 'customer';
        this.router.navigate([this.returnUrl ? this.returnUrl : this.location]);
      },
        err => this.error = err.error);
  }
}
