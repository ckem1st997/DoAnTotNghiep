import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { WareHouseBookSearchModel } from 'src/app/model/WareHouseBookSearchModel';

@Component({
  selector: 'app-formSearchWareHouseBook',
  templateUrl: './formSearchWareHouseBook.component.html',
  styleUrls: ['./formSearchWareHouseBook.component.scss']
})
export class FormSearchWareHouseBookComponent implements OnInit {
  private readonly notifier!: NotifierService;
  selectedValue: string = "";
  form!: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<FormSearchWareHouseBookComponent>,
    @Inject(MAT_DIALOG_DATA) public data: WareHouseBookSearchModel,
    private formBuilder: FormBuilder,
    notifierService: NotifierService
  ) { this.notifier = notifierService; }
  ngOnInit() {
    // var dateStart=this.data?.toDate != undefined?new Date(this.data?.toDate).setDate(new Date(this.data?.toDate).getDate()+1):null;
    this.form = this.formBuilder.group({
      key: this.data?.keySearch != undefined ? this.data.keySearch : '',
      inactive: null,
      start:  this.data?.fromDate != undefined ? this.addDays(new Date(this.data?.fromDate), 1).toISOString().slice(0, -1) : new FormControl(),
      end: this.data?.toDate != undefined ? this.addDays(new Date(this.data?.toDate), 1).toISOString().slice(0, -1) : new FormControl(),
      type: this.data?.typeWareHouseBook != undefined ? this.data.typeWareHouseBook : ''
    });
  }

  addDays(date: Date, days: number): Date {
    date.setDate(date.getDate() + days);
    return date;
  }
  onNoClick(): void {
    this.dialogRef.close(false);
  }
  onSubmit() {
    this.form.value.start = this.form.value.start != null ? new Date(this.form.value.start).toLocaleDateString('en-US') : null;
    this.form.value.end = this.form.value.end != null ? new Date(this.form.value.end).toLocaleDateString('en-US') : null;
    this.dialogRef.close(this.form.value)
  }
}

