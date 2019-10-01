import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ICustomer } from 'src/app/shared/models/ICustomer';
import { AppSettings } from 'src/app/configs/app-settings.config';
import { AuthenticationService } from '../authentication/authentication.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  headers: HttpHeaders;

  constructor(private http: HttpClient, private authService: AuthenticationService) {
    this.headers = new HttpHeaders({
      'Authorization': 'Bearer ' + this.authService.currentUserValue.token,
      'Content-Type': 'application/json'
    });
   }

  addCustomer(customer: ICustomer): Observable<any> {
    return this.http.post(AppSettings.apiUrl + '/customers', customer, {
      headers: this.headers
    });
  }

  getCustomers(): Observable<ICustomer[]> {
    return this.http.get<ICustomer[]>(AppSettings.apiUrl + '/customers', {
      headers: this.headers
    });
  }

  updateCustomer(customer: ICustomer): Observable<any> {
    return this.http.put(AppSettings.apiUrl + '/customers', customer, {
      headers: this.headers
    });
  }
}
