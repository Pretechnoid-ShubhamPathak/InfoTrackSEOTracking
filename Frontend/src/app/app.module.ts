import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgxScannerQrcodeModule } from 'ngx-scanner-qrcode';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LayoutModule } from '@angular/cdk/layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DatePipe } from '@angular/common';
import { MaterialModuleImportModule } from './core/modules/material-module-import.module';
import { PrimengModuleImportModule } from './core/modules/primeng-module-import.module';

import { LoginComponent } from './views/login/login.component';
import { GlobalService } from './framework/services/global.service';
import { AuthInterceptor, httpInterceptorProviders } from './framework/auth/auth-interceptor';
import { NgChartsModule } from 'ng2-charts';

import { AdmindashboardComponent } from './views/admindashboard/admindashboard.component';
import { ConfirmDialogComponent } from './views/confirmdialog/confirmdialog.component';
import { SendMailComponent } from './views/sendmail/sendmail.component'
import { ResetPasswordComponent } from './views/resetpassword/resetpassword.component';
import { ForgetPasswordComponent } from './views/forgetpassword/forgetpassword.component';

import { SEOTrackingComponent } from './views/seotracking/seotracking.component';
import { TrackingHistoryComponent } from './views/trackinghistory/trackinghistory.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AdmindashboardComponent,
    ResetPasswordComponent,
    ForgetPasswordComponent,
    ConfirmDialogComponent,
    SendMailComponent,
    SEOTrackingComponent,
    TrackingHistoryComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    NgxScannerQrcodeModule,
    BrowserAnimationsModule,
    MaterialModuleImportModule,
    PrimengModuleImportModule,
    FormsModule,
    LayoutModule,
    ReactiveFormsModule,
    NgChartsModule,
  ],
  exports: [FormsModule,NgChartsModule
  ],
  providers: [DatePipe,GlobalService, httpInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
