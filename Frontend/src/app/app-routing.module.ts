import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './views/login/login.component';
import { ForgetPasswordComponent } from './views/forgetpassword/forgetpassword.component';
import { SendMailComponent } from './views/sendmail/sendmail.component';
import { ResetPasswordComponent } from './views/resetpassword/resetpassword.component';
import { AdmindashboardComponent } from './views/admindashboard/admindashboard.component';
import { SEOTrackingComponent } from './views/seotracking/seotracking.component';
import { TrackingHistoryComponent } from './views/trackinghistory/trackinghistory.component';


const routes: Routes = [
  { path: '', redirectTo: '/seotracking', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'forgotpassword', component: ForgetPasswordComponent},
  { path: 'resetpassword', component: ResetPasswordComponent },
  { path: 'sendMail', component:SendMailComponent},
  { path: 'seotracking', component:SEOTrackingComponent},
  { path: 'admin', component:AdmindashboardComponent,
    children:[
      { path: 'seotracking', component:SEOTrackingComponent},
      { path: 'trackinghistory', component:TrackingHistoryComponent},
      { path: 'sendMail', component:SendMailComponent}
    ]
  }
];

export const appRouting = RouterModule.forRoot(routes);

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
