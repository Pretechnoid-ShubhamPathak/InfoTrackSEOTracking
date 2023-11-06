import { GlobalService } from 'src/app/framework/services/global.service';
import { Component, OnInit, AfterViewInit } from '@angular/core';
import { AuthService } from 'src/app/framework/auth/auth.service';
import { TokenStorageService } from 'src/app/framework/auth/token-storage.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MessageService } from 'primeng/api';
import { AuthOtpInfo } from 'src/app/framework/auth/otp-info';

@Component({
  selector: 'app-forgetpassword',
  templateUrl: './forgetpassword.component.html',
  providers: [MessageService],
  styleUrls: ['./forgetpassword.component.css']
})
export class ForgetPasswordComponent implements OnInit , AfterViewInit{
  otpData = '5';
  form: any = {};
  otpform: any = {};
  isLoggedIn = false;
  isLoginFailed = false;
  isotpFailed:boolean;
  otpclicked:boolean;
  errorMessage = '';
  successMessage = '';
  roles: string;
  private otpInfo!: AuthOtpInfo;
  isformDisable:boolean = false;
  userOtp:number = 0;

  constructor(private msg: MessageService, private authService: AuthService, private tokenStorage: TokenStorageService,
              private router: Router, private route: ActivatedRoute, private global: GlobalService) { }

  ngOnInit() {
    if (this.tokenStorage.getToken()) {
      this.isLoggedIn = true;
      this.roles = this.tokenStorage.getUserRole();
      //this.router.navigate(['./u']);
    }
    //this.otpform.submitotp = 'Verify OTP';
    this.form.submit = 'Send OTP';

  }

  ngAfterViewInit() {
    this.msg.add({severity: 'success', summary: 'Service Message', detail: 'Via MessageService'});
  }

  onSubmit() {
    this.otpInfo = new AuthOtpInfo();
    this.otpInfo.email = this.form.email;
    this.otpInfo.username = this.form.username;
    console.log(this.otpInfo.username);
    console.log(this.otpInfo.email);
    this.isformDisable = true;
    this.authService.sendotp(this.otpInfo.username,this.otpInfo.email).subscribe(
      otp => {
        this.userOtp = otp;
        console.log(this.userOtp);
    });

  }

  verifyOtp(){
    console.log(this.userOtp + ' = '+ this.form.otp)
    if(this.userOtp == this.form.otp){
      this.otpclicked = true;
      this.isotpFailed = false;
      this.successMessage = ' OTP Verified !! ';
      this.tokenStorage.saveUsername(this.otpInfo.username);
      this.router.navigate(['./resetpassword']);
    }else{
      this.isotpFailed = true;
      this.errorMessage = ' wrong OTP entered...Try Again  ';
    }
  }

  reloadPage() {
    window.location.reload();
  }
}
