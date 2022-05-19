import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';

@Component({
  selector: 'app-formSearchBeginning',
  templateUrl: './formSearchBeginning.component.html',
  styleUrls: ['./formSearchBeginning.component.scss']
})
export class FormSearchBeginningComponent implements OnInit {
  private readonly notifier!: NotifierService;
  form!: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<FormSearchBeginningComponent>,
    private formBuilder: FormBuilder,
    notifierService: NotifierService
  ) { this.notifier = notifierService; }
  ngOnInit() {
    this.form = this.formBuilder.group({
      key: '',
      inactive: null,
      start: new FormControl(),
      end: new FormControl(),
    });
  }
  onNoClick(): void {
    this.dialogRef.close(false);
  }
  onSubmit() {
    this.dialogRef.close(this.form.value)
  }
}

