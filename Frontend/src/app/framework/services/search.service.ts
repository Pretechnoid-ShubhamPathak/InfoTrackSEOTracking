
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { SearchHistory,SearchHistoryRequest } from '../models/SearchHistory';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  url = environment.baseURL;
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<SearchHistory[]>(this.url + '/Search/getAllTracking');
  }

  Search(requests:SearchHistoryRequest[]) {
    return this.http.post<SearchHistory[]>(this.url + '/Search/SEOTracking', requests);
  }

}
