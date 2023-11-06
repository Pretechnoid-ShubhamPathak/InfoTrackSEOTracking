import { GlobalService } from 'src/app/framework/services/global.service';
import { Component, OnInit, AfterViewInit, Input } from '@angular/core';
import { AuthService } from 'src/app/framework/auth/auth.service';
import { TokenStorageService } from 'src/app/framework/auth/token-storage.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MessageService } from 'primeng/api';
import { AuthOtpInfo } from 'src/app/framework/auth/otp-info';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-resetpassword',
  templateUrl: './resetpassword.component.html',
  providers: [MessageService],
  styleUrls: ['./resetpassword.component.css']
})
export class ResetPasswordComponent implements OnInit , AfterViewInit{
  form: any = {};
  isLoggedIn = false;
  isLoginFailed = false;

  userName:string;

  errorMessage = '';
  successMessage = '';

  isformDisable:boolean = false;
  isusernameDisable:boolean = false;
  userOtp:number = 0;

  constructor(private formBuilder: FormBuilder,private msg: MessageService, private authService: AuthService, private tokenStorage: TokenStorageService,
              private router: Router, private route: ActivatedRoute, private global: GlobalService) {

                this.form = this.formBuilder.group({
                  password: [
                            '',
                            [Validators.required,Validators.minLength(8),
                             Validators.pattern(/^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])[A-Za-z\d!@#\$%\^&\*]{6,}$/),]],
                  cpassword: ['', [Validators.required, Validators.minLength(8)]],
                });

               }

  ngOnInit() {
    this.userName =  this.tokenStorage.getUsername();

    if(!this.userName){
      this.msg.add({severity: 'Error', summary: 'Service Message', detail: 'Please Go To forget password first.'});
      this.isusernameDisable = true;
      this.isformDisable = true;
    }else{
      this.form.submit = 'Change Password';
    }
  }

  ngAfterViewInit() {
    if(this.userName)
      this.msg.add({severity: 'success', summary: 'Service Message', detail: 'OTP Verified Successfully'});
    else
      this.msg.add({severity: 'warn', summary: 'Service Message', detail: 'Please Go To forget password first.'});
  }

  onSubmit() {
    if(this.userName){
      if(this.form.password == this.form.cpassword){
      const password = this.form.password;
      console.log(this.form.password);
      console.log(this.form.cpassword);
      this.authService.changePassword(this.form.username,password).subscribe();
      this.isformDisable = true;
      this.router.navigate(['./login']);
      }else{
        this.msg.add({severity: 'warn', summary: 'Service Message', detail: "Password & Confirm Password doesn't match"});
        this.form.password = null;
        this.form.cpassword = null;
      }
    }
  }

  reloadPage() {
    window.location.reload();
  }
}
