import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { GlobalService } from './global.service';
import { MailData } from '../models/MailData';


@Injectable({
  providedIn: 'root'
})
export class MailService {
  url = environment.baseURL;
  constructor(private http: HttpClient, private global: GlobalService) { }

  sendMail(mailData:MailData) {
    return this.http.post<any>(this.url + '/Mail/SendMail', mailData);
  }

  sendOtp(username:string,emailId:string) {
    return this.http.get<number>(this.url + '/Mail/sendotp/'+username+'/'+emailId);
  }

}
