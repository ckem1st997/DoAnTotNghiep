import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';
import { WareHouseDTO } from 'src/app/model/WareHouseDTO';
import { WarehouseService } from 'src/app/service/warehouse.service';
import { WareHouseValidator } from 'src/app/validator/WareHouseValidator';

@Component({
  selector: 'app-WareHouseDetails',
  templateUrl: './WareHouseDetails.component.html',
  styleUrls: ['./WareHouseDetails.component.scss']
})
export class WareHouseDetailsComponent implements OnInit {
  title = "Thông tin kho vận";
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
    public dialogRef: MatDialogRef<WareHouseDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: WareHouseDTO,
    private formBuilder: FormBuilder,
    private service: WarehouseService,
  ) { }
  ngOnInit() {
    this.dt = this.data;
    this.form = this.formBuilder.group({
      id: this.data.id,
      code: this.data.code,
      name: this.data.name,
      address: this.data.address,
      description: this.data.description,
      parentId: this.data.parentId,
      path: this.data.path,
      inactive: this.data.inactive,
    });
    this.getDropDown();
  }
  getDropDown() {
    this.listDropDown.data = this.data.wareHouseDTOs;
  }
  get f() { return this.form.controls; }
  onNoClick(): void {
    this.dialogRef.close(false);
  }
}