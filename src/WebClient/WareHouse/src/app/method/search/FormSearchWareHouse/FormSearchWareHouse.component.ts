import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';


@Component({
  selector: 'app-FormSearchWareHouse',
  templateUrl: './FormSearchWareHouse.component.html',
  styleUrls: ['./FormSearchWareHouse.component.scss']
})
export class FormSearchWareHouseComponent implements OnInit {
  private readonly notifier!: NotifierService;
  form!: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<FormSearchWareHouseComponent>,
    private formBuilder: FormBuilder,
    notifierService: NotifierService
  ) { this.notifier = notifierService; }
  ngOnInit() {
    this.form = this.formBuilder.group({
      key: '',
      inactive: null,
    });
  }
  onNoClick(): void {
    this.dialogRef.close(false);
  }
  onSubmit() {
    console.log(this.form.value);
    this.dialogRef.close(this.form.value)
  }
}

