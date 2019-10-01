import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { AppSettings } from 'src/app/configs/app-settings.config';
import { BehaviorSubject, Observable } from 'rxjs';
import { IUser } from 'src/app/shared/models/IUser';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<IUser>;
  public currentUser: Observable<IUser>;

  constructor(private http: HttpClient) {
      this.currentUserSubject = new BehaviorSubject<IUser>(JSON.parse(localStorage.getItem('currentUser')));
      this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): IUser {
      return this.currentUserSubject.value;
  }

  login(username: string, password: string) {
    console.log(`${AppSettings.apiUrl}/login`);
    return this.http.post<any>(`${AppSettings.apiUrl}/authentication/login`, { username, password })
      .pipe(map(user => {
          if (user && user.token) {
              localStorage.setItem('currentUser', JSON.stringify(user));
              this.currentUserSubject.next(user);
          }

          return user;
      }));
  }

  logout() {
    this.currentUserSubject.next(null);
    localStorage.removeItem('currentUser');
  }
}
