import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';
import { WareHouseDTO } from 'src/app/model/WareHouseDTO';
import { WarehouseService } from 'src/app/service/warehouse.service';
import { WareHouseValidator } from 'src/app/validator/WareHouseValidator';

@Component({
  selector: 'app-WareHouseEdit',
  templateUrl: './WareHouseEdit.component.html',
  styleUrls: ['./WareHouseEdit.component.scss']
})
export class WareHouseEditComponent implements OnInit {
  title = "Chỉnh sửa kho vận";
  private readonly notifier!: NotifierService;
  success = false;
  form!: FormGroup;
  dt!: WareHouseDTO;
  options!: FormGroup;
  listDropDown: ResultMessageResponse<WareHouseDTO> = {
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
    public dialogRef: MatDialogRef<WareHouseEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: WareHouseDTO,
    private formBuilder: FormBuilder,
    private service: WarehouseService,
    notifierService: NotifierService
  ) { this.notifier = notifierService; }
  ngOnInit() {
    this.dt = this.data;
    this.form = this.formBuilder.group({
      id: this.data.id,
      code:  this.data.code,
      name:  this.data.name,
      address:  this.data.address,
      description:  this.data.description,
      parentId:  this.data.parentId,
      path:  this.data.path,
      inactive:  this.data.inactive,
    });
    this.getDropDown();
  }
  get f() { return this.form.controls; }
  onNoClick(): void {
    this.dialogRef.close(false);
  }
  getDropDown() {
    this.listDropDown.data = this.data.wareHouseDTOs;
  }

  onSubmit() {
    var test = new WareHouseValidator();
    var msg = test.validate(this.form.value);
    var check = JSON.stringify(msg) == '{}';
    if (check == true)
      this.service.Edit(this.form.value).subscribe(x => {
        if (x.success)
          this.dialogRef.close(x.success)
     //   else
       //   this.notifier.notify('error', x.errors["msg"][0]);
      } ,   
      //   error => {
      //   if (error.error.errors.length === undefined)
      //     this.notifier.notify('error', error.error.message);
      //   else
      //     this.notifier.notify('error', error.error);
      // }
      );
    else {
      var message = '';
      for (const [key, value] of Object.entries(msg)) {
        message = message + " " + value;
      }
      this.notifier.notify('error', message);
    }

  }
}

