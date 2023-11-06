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
  selector: 'app-seotracking-setting',
  templateUrl: './seotracking.component.html',
  styleUrls: ['./seotracking.component.css'],
  providers: [MessageService]
})
export class SEOTrackingComponent implements OnInit {

  isPageLoaded = true;
  errorMessage = '';

  keywordFormControl = new FormControl('', [Validators.required]);
  urlFormControl = new FormControl('', [Validators.required]);
  searchengines = new FormControl(['']);

  matcher = new MyErrorStateMatcher();
  lstsearchengine: string[] = [SearchEngine.Google,SearchEngine.Bing,SearchEngine.Yahoo]

  selectedSearchEngine = this.lstsearchengine[0];

  displayedColumns: string[] = ['id', 'keywords', 'url', 'searchEngine','ranking'];
  dataSource: SearchHistory[];

  lstSearchHistoryRequest : SearchHistoryRequest[] = [];

  constructor(private _search:SearchService, private router: Router,private msg: MessageService, public _dialog: MatDialog) {
   }

  ngOnInit() {
  }

  onSubmit() {
    console.log("testing");
    console.log(this.keywordFormControl.value);
    console.log(this.searchengines.value);
    console.log(this.urlFormControl.value);

    var keywords  = this.keywordFormControl.value!;
    var url = this.urlFormControl.value!;

    this.searchengines.value?.forEach(se => {
      var searchHistoryRequest = new SearchHistoryRequest();
      searchHistoryRequest.searchEngine = se;
      searchHistoryRequest.keywords = keywords;
      searchHistoryRequest.url = url;
      this.lstSearchHistoryRequest.push(searchHistoryRequest);
    });

    this._search.Search(this.lstSearchHistoryRequest).subscribe( respone =>{
      this.dataSource = respone;
    });
  }



}
