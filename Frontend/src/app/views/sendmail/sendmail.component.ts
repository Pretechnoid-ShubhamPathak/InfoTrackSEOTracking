import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MessageService } from 'primeng/api';
import { GlobalService } from 'src/app/framework/services/global.service';
import { AdminService } from 'src/app/framework/services/admin.service';
import { UserService } from 'src/app/framework/services/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { ConfirmDialogComponent } from '../confirmdialog/confirmdialog.component';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { User } from 'src/app/framework/models/User';
import { Console } from 'console';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { Observable, map, startWith } from 'rxjs';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MailData } from 'src/app/framework/models/MailData';
import { MailService } from 'src/app/framework/services/mail.service';


@Component({
  selector: 'app-sendmail-setting',
  templateUrl: './sendmail.component.html',
  styleUrls: ['./sendmail.component.css'],
  providers: [MessageService]
})
export class SendMailComponent implements OnInit {

  isPageLoaded = true;
  errorMessage = '';

  stdcount:number;
  lstUser: User[] = [];
  selectedUsers: User[] = [];

  toEmail: string = '';
  ccEmail: string = '';
  bccEmail: string = '';
  emailSubject: string = '';
  emailMessage: string = '';

  senderName = '';

  mailData:MailData;

  send_Email: FormGroup;
  selectUser = new FormControl('');
  isUserDisable:boolean = false;

  @ViewChild('userInput') userInput: ElementRef<HTMLInputElement>;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  filteredUsers: Observable<User[]>;



  constructor(private _formBuilder: FormBuilder,private _mailService:MailService, private formBuilder: FormBuilder,private global: GlobalService,private _adminService: AdminService,private userService: UserService, private router: Router,private msg: MessageService, public _dialog: MatDialog) {

    this.send_Email = this._formBuilder.group({
      fctoEmail: ['',[Validators.required]],
      fcccEmail: [''],
      fcbccEmail: [''],
      fcsubject: ['',[Validators.required],Validators.maxLength(50)],
      fcmessage: ['',[Validators.required]]
    });
   }

  ngOnInit() {

    this.userService.getAllUser().subscribe(users => {
      this.lstUser = users.filter(u => u.userRole != null);
      console.log(this.lstUser);
      var user = this.lstUser.find(u => u.id == this.global.getUid());
      this.senderName = user?.firstName + ' ' +user?.lastName;
      this.filteredUsers = this.selectUser.valueChanges.pipe(
        startWith(null),
        map((user: string | null) => (user ? this._topicFilter(user) : this.lstUser.slice())),
      );
    });

  }


  private _topicFilter(user: string) {
    const filterValue = user.toLowerCase();

    return this.lstUser.filter(user => user.emailId.toLowerCase().includes(filterValue) || user.firstName.toLowerCase().includes(filterValue) || user.username.toLowerCase().includes(filterValue));
  }

  remove(userid: number): void {
    console.log(userid);

    var user = this.selectedUsers.find( t => t.id == userid);
    // Add User
    if (user) {
      this.lstUser.push(user);
    }
    this.selectedUsers = this.selectedUsers.filter(u => u.id !== userid);
    console.log(this.selectedUsers);
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    console.log(event.option.value);
    var user = this.lstUser.find( u => u.id == event.option.value);
    // Add User
    if (user) {
      this.selectedUsers.push(user);
    }
    this.lstUser = this.lstUser.filter( u => u.id != event.option.value);

    this.userInput.nativeElement.value= '';
    this.selectUser.setValue(null);
  }

  sendEmail() {
    const subjectValue = this.send_Email.get('fcsubject')?.value;
    const messageValue = this.send_Email.get('fcmessage')?.value;

    if (messageValue === '' && subjectValue === '') {
      this.msg.add({severity:'info', summary: 'Required', detail: 'Both Subject and Body are required...'});
      console.log('Both Subject and Body are required.');
      return;
    }
    if (subjectValue === '') {
      this.msg.add({severity:'info', summary: 'Required', detail: 'Subject is required....'});
      console.log('Subject is required.');
      return;
    }
    if (messageValue === '') {
      this.msg.add({severity:'info', summary: 'Required', detail: 'Body is required....'});
      console.log('Body is required.');
      return;
    }
    this.mailData = new MailData();
    this.mailData.EmailSubject = subjectValue;

    this.selectedUsers.forEach( user => {
      this.mailData.EmailBody = "Hi <b>"+user.firstName + ' ' + user.lastName+"</b>,<br><br> " + messageValue +"<br><br><br> Thank you,<br><b>" + this.senderName+"</b>";
      this.mailData.EmailToName = user.firstName + ' ' + user.lastName;
      this.mailData.ToEmailId = user.emailId;
      this._mailService.sendMail(this.mailData).subscribe();
      this.msg.add({severity:'success', summary: 'Mail Sent Successfully to '+user.userRole, detail: ' Mail is send to ' +user.firstName + ' ' + user.lastName});
    });
    console.log(this.selectedUsers);
  }

}
