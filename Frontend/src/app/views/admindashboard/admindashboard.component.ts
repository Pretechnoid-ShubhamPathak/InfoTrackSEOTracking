import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { map } from 'rxjs/operators';
import { GlobalService } from 'src/app/framework/services/global.service';
import { UserService } from 'src/app/framework/services/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { User } from 'src/app/framework/models/User';
import { ChartConfiguration, ChartOptions } from 'chart.js';

@Component({
  selector: 'app-admindashboard',
  templateUrl: './admindashboard.component.html',
  styleUrls: ['./admindashboard.component.css']
})
export class AdmindashboardComponent implements OnInit {

  user_name : string;
  user_email : string;
  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches)
    );

  constructor(private global: GlobalService,private UserService: UserService, private router: Router, private route: ActivatedRoute,
              private breakpointObserver: BreakpointObserver) { }

  ngOnInit() {
    if (! this.global.getLogged()) {
      this.router.navigate(['/login']);
    }else{
      this.UserService.getUser(this.global.getUid()).subscribe({
        next : data => {
          this.user_name = data.username;
          this.user_email = data.emailId;
          console.log(data);
        }
      });
    }
  }

  logout() {
    window.sessionStorage.clear();
    this.router.navigate(['/login']);
    this.global.setLogged(false);
  }
}
