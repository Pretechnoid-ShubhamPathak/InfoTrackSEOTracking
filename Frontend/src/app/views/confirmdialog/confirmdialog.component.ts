import {Inject, Component, HostListener} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import { Dialog } from 'primeng/dialog';

@Component({
  selector: 'app-confirmdialog',
  templateUrl: './confirmdialog.component.html',
  styleUrls: ['./confirmdialog.component.css']
})
export class ConfirmDialogComponent {
  _title : string;
  _message : string;
  _action : string;

  constructor(@Inject(MAT_DIALOG_DATA) public data: {title:string,message:string,action:string},
  private dialogRef: MatDialogRef<ConfirmDialogComponent>){
    this._title = data.title;
    this._message = data.message;
    this._action = data.action.toLowerCase();
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  @HostListener("keydown.esc")
  public onEsc() {
    this.onNoClick();
  }

}
