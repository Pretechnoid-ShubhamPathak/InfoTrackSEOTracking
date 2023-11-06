import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { GlobalService } from './global.service';


@Injectable({
  providedIn: 'root'
})
export class AdminService {
  url = environment.baseURL;
  constructor(private http: HttpClient, private global: GlobalService) { }


}
