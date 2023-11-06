import { UserList } from './../models/UserList';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/User';
import { GlobalService } from './global.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  url = environment.baseURL;
  constructor(private http: HttpClient) { }

  getAllUser() {
    return this.http.get<User[]>(this.url + '/Users');
  }

  getUser(uid: number) {
    return this.http.get<User>(this.url + '/Users/' + uid);
  }

  getUserbyUsername(username: string) {
    return this.http.get<User>(this.url + '/Users/getbyusernameemail/' + username);
  }



}
