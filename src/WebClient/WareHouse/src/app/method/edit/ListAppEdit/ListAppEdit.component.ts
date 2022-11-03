import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { Guid } from 'src/app/extension/Guid';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';
import { UnitDTO } from 'src/app/model/UnitDTO';
import { UnitService } from 'src/app/service/Unit.service';
import { UnitValidator } from 'src/app/validator/UnitValidator';
import { ListApp } from './../../../model/ListApp';
import { ListAppService } from './../../../service/ListApp.service';


@Component({
  selector: 'app-ListAppEdit',
  templateUrl: './ListAppEdit.component.html',
  styleUrls: ['./ListAppEdit.component.scss']
})
export class ListAppEditComponent implements OnInit {

  title = "Thêm mới";
  private readonly notifier!: NotifierService;
  success = false;
  form!: FormGroup;
  dt!: ListApp;
  options!: FormGroup;
  listDropDown: ResultMessageResponse<ListApp> = {
    success: false,
    code: '',
    httpStatusCode: 0,
    title: '',
    message: '',
    data: [],
    totalCount: 0,
    isRedirect: false,
    redirectUrl: '',
    errors: {}
  }
  constructor(
    public dialogRef: MatDialogRef<ListAppEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ListApp,
    private formBuilder: FormBuilder,
    private service: ListAppService,
    notifierService: NotifierService
  ) { this.notifier = notifierService; }
  ngOnInit() {
    this.dt = this.data;
    this.form = this.formBuilder.group({
      id: Guid.newGuid(),
      name:'',
      inActive: true,
      description: '',
      parentId: false,
      softShow: 1,
      isAPI: true
    });
    this.form.patchValue(this.data);
  }
  get f() { return this.form.controls; }
  onNoClick(): void {
    this.dialogRef.close(false);
  }

  onSubmit() {
    this.service.Edit(this.form.value).subscribe(x => {
      if (x.success)
        this.dialogRef.close(x.success)
    });

  }
}


