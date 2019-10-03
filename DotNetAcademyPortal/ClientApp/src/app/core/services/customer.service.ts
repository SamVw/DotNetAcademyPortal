import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ICustomer } from 'src/app/shared/models/ICustomer';
import { AppSettings } from 'src/app/configs/app-settings.config';
import { AuthenticationService } from '../authentication/authentication.service';
import { Observable } from 'rxjs';
import { ApiHeaderHelper } from 'src/app/shared/Helpers/ApiHeaderHelper';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  headers: HttpHeaders;

  constructor(private http: HttpClient, private authService: AuthenticationService) {
   }

  addCustomer(customer: ICustomer): Observable<ICustomer> {
    return this.http.post<ICustomer>(AppSettings.apiUrl + '/customers', customer, {
      headers: ApiHeaderHelper.createHeader(this.authService.currentUserValue.token)
    });
  }

  getCustomers(): Observable<ICustomer[]> {
    console.log(this.headers);
    return this.http.get<ICustomer[]>(AppSettings.apiUrl + '/customers', {
      headers: ApiHeaderHelper.createHeader(this.authService.currentUserValue.token)
    });
  }

  updateCustomer(customer: ICustomer): Observable<any> {
    return this.http.put(AppSettings.apiUrl + '/customers', customer, {
      headers: ApiHeaderHelper.createHeader(this.authService.currentUserValue.token)
    });
  }

  getCustomer(id: string): Observable<ICustomer> {
    return this.http.get<ICustomer>(AppSettings.apiUrl + '/customers/' + id, {
      headers: ApiHeaderHelper.createHeader(this.authService.currentUserValue.token)
    });
  }

  getAuthenticatedCustomer(): Observable<ICustomer> {
    return this.http.get<ICustomer>(AppSettings.apiUrl + '/customers/authenticated/' + this.authService.currentUserValue.id, {
      headers: ApiHeaderHelper.createHeader(this.authService.currentUserValue.token)
    });
  }
}
