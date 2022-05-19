import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { Guid } from 'src/app/extension/Guid';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';
import { WareHouseItemCategoryDTO } from 'src/app/model/WareHouseItemCategoryDTO';
import { WareHouseItemCategoryService } from 'src/app/service/WareHouseItemCategory.service';
import { WareHouseValidator } from 'src/app/validator/WareHouseValidator';
import { WareHouseCreateComponent } from '../WareHouseCreate/WareHouseCreate.component';

@Component({
  selector: 'app-WareHouseItemCategoryCreate',
  templateUrl: './WareHouseItemCategoryCreate.component.html',
  styleUrls: ['./WareHouseItemCategoryCreate.component.scss']
})

export class WareHouseItemCategoryCreateComponent implements OnInit {

  title = "Thêm loại vật tư";
  private readonly notifier!: NotifierService;
  success = false;
  form!: FormGroup;
  dt!: WareHouseItemCategoryDTO;
  options!: FormGroup;
  listDropDown: ResultMessageResponse<WareHouseItemCategoryDTO> = {
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
    public dialogRef: MatDialogRef<WareHouseCreateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: WareHouseItemCategoryDTO,
    private formBuilder: FormBuilder,
    private service: WareHouseItemCategoryService,
    notifierService: NotifierService
  ) { this.notifier = notifierService; }
  ngOnInit() {
    this.dt = this.data;
    this.form = this.formBuilder.group({
      id: Guid.newGuid(),
      code: '',
      name: '',
      description: null,
      parentId: null,
      path: null,
      inactive: true,
    });
    this.form.patchValue(this.data);

    this.getDropDown();
  }
  get f() { return this.form.controls; }
  onNoClick(): void {
    this.dialogRef.close(false);
  }
  getDropDown() {
    this.service.getListDropDown().subscribe(x => this.listDropDown = x);
  }

  onSubmit() {
    var test = new WareHouseValidator();
    var msg = test.validate(this.form.value);
    var check = JSON.stringify(msg) == '{}';
    if (check == true)
      this.service.Add(this.form.value).subscribe(x => {
        if (x.success)
          this.dialogRef.close(x.success)
        // else {
        //   if (x.errors["msg"] != undefined)
        //   this.notifier.notify('error', x.errors["msg"][0]);
        // else
        //   this.notifier.notify('error', x.message);
        // }

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

