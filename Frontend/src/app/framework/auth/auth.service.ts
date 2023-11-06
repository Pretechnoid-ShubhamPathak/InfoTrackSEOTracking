import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { JwtResponse } from './jwt-response';
import { AuthLoginInfo } from './login-info';
import { environment } from 'src/environments/environment';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  url = environment.baseURL;
  private loginUrl = this.url + '/Users/authenticate';
  private changpasswordUrl = this.url + '/Users/changepassword/';

  constructor(private http: HttpClient) {
  }

  attemptAuth(credentials: AuthLoginInfo): Observable<JwtResponse> {
    return this.http.post<JwtResponse>(this.loginUrl, credentials, httpOptions);
  }

  changePassword(username:string,password:string): Observable<JwtResponse> {
    return this.http.post<any>(this.changpasswordUrl+username+'/'+password, httpOptions);
  }

  sendotp(username:string,email:string){
    return this.http.get<number>(this.url + '/Mail/sendotp/'+username+'/'+email);
  }
}
