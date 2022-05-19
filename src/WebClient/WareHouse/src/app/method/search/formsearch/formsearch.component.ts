import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { VendorService } from 'src/app/service/VendorService.service';

@Component({
  selector: 'app-formsearch',
  templateUrl: './formsearch.component.html',
  styleUrls: ['./formsearch.component.scss']
})
export class FormsearchComponent implements OnInit {
  private readonly notifier!: NotifierService;
  form!: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<FormsearchComponent>,
    private formBuilder: FormBuilder,
    private service: VendorService,
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

