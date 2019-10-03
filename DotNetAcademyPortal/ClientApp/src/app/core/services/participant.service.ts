import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { AuthenticationService } from '../authentication/authentication.service';
import { Observable } from 'rxjs';
import { IParticipant } from 'src/app/shared/models/IParticipant';
import { AppSettings } from 'src/app/configs/app-settings.config';
import { ApiHeaderHelper } from 'src/app/shared/Helpers/ApiHeaderHelper';

@Injectable({
  providedIn: 'root'
})
export class ParticipantService {

  constructor(private http: HttpClient, private authService: AuthenticationService) {
   }

   getParticipantsForCustomer(customerId: string): Observable<IParticipant[]> {
     return this.http.get<IParticipant[]>(AppSettings.apiUrl + '/participants/customer/' + customerId, {
       headers: ApiHeaderHelper.createHeader(this.authService.currentUserValue.token)
     });
   }

   addParticipant(participant: IParticipant, id: string): Observable<IParticipant> {
    return this.http.post<IParticipant>(AppSettings.apiUrl + '/participants/customer/' + id, participant, {
      headers: ApiHeaderHelper.createHeader(this.authService.currentUserValue.token)
    });
   }

   updateParticipant(participant: IParticipant, id: string): Observable<IParticipant> {
    console.log(participant);
    return this.http.put<IParticipant>(AppSettings.apiUrl + '/participants/customer/' + id, participant, {
      headers: ApiHeaderHelper.createHeader(this.authService.currentUserValue.token)
    });
   }

   getParticipantsForAuthenticatedCustomer(): Observable<IParticipant[]> {
    return this.http.get<IParticipant[]>(
      AppSettings.apiUrl + '/participants/customer/authenticated/' + this.authService.currentUserValue.id, {
      headers: ApiHeaderHelper.createHeader(this.authService.currentUserValue.token)
    });
   }

   updateParticipantName(participant: IParticipant, id: string): Observable<any> {
    return this.http.put(
      AppSettings.apiUrl + '/participants/customer/' + id + '/name', participant, {
      headers: ApiHeaderHelper.createHeader(this.authService.currentUserValue.token)
    });
   }
}
