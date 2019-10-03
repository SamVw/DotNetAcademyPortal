import { HttpHeaders } from '@angular/common/http';

export class ApiHeaderHelper {
  static createHeader(token: string): HttpHeaders {
    return new HttpHeaders({
      'Authorization': 'Bearer ' + token,
      'Content-Type': 'application/json'
    });
  }
}
