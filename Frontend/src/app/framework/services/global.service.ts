import { Injectable } from '@angular/core';
@Injectable({
  providedIn: 'root'
})
export class GlobalService {

  constructor() { }

  private token!: string;
  private uid!: number;
  private urole!: string;
  private username!: string;
  private userEmail!: string;
  private logged = false;
  getToken(): string {

    this.token = sessionStorage.getItem('token')!;
    return this.token;
  }
  setToken(tokenValue: string) {
    this.token = tokenValue;
    sessionStorage.setItem('token', this.token);
  }
  getUid(): number {
    this.uid = +sessionStorage.getItem('uid')!;
    return this.uid;
  }
  setUid(uid: number) {
    this.uid = uid;
    sessionStorage.setItem('uid', '' + this.uid);
  }
  getURole(): string {
    this.uid = +sessionStorage.getItem('urole')!;
    return this.urole;
  }
  setURole(urole: string) {
    this.urole = urole;
    sessionStorage.setItem('urole', '' + this.urole);
  }
  getUsername(): string {
    this.username = sessionStorage.getItem('username')!;
    return this.username;
  }
  setUsername(_username: string) {
    this.username = _username;
    sessionStorage.setItem('username', this.username);
  }
  getUserEmail(): string {
    this.userEmail = sessionStorage.getItem('userEmail')!;
    return this.userEmail;
  }
  setUserEmail(userEmail: string) {
    this.userEmail = userEmail;
    sessionStorage.setItem('userEmail', this.userEmail);
  }
  setLogged(v: boolean) {
    if ( !v ) {
      sessionStorage.clear();
      sessionStorage.setItem('logged', 'false');    // again created "logged" sessionstorage item, bcoz it is needed to be checked
    } else {
      sessionStorage.setItem('logged', 'true');
    }
    this.logged = v;
  }
  getLogged(): boolean {
    if ( sessionStorage.getItem('logged') === 'true') { this.logged = true; }
    if ( sessionStorage.getItem('logged') === 'false') { this.logged = false; }
    return this.logged;
  }

}
