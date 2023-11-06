import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';

import { SearchService } from 'src/app/framework/services/search.service';
import { SearchEngine } from 'src/app/framework/models/SearchEngine';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder, FormControl, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { SearchHistory, SearchHistoryRequest } from 'src/app/framework/models/SearchHistory';
/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-trackinghistory-setting',
  templateUrl: './trackinghistory.component.html',
  styleUrls: ['./trackinghistory.component.css'],
  providers: [MessageService]
})
export class TrackingHistoryComponent implements OnInit {

  isPageLoaded = true;
  errorMessage = '';

  displayedColumns: string[] = ['id', 'keywords', 'url', 'searchEngine','ranking','searchedAt'];
  dataSource: SearchHistory[];

  lstSearchHistoryRequest : SearchHistoryRequest[] = [];

  constructor(private _search:SearchService, private router: Router,private msg: MessageService, public _dialog: MatDialog) {
   }

  ngOnInit() {
    console.log("testing");

    this._search.getAll().subscribe( respone =>{
      this.dataSource = respone;
    });
  }



}
