import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { WareHouseItemDTO } from 'src/app/model/WareHouseItemDTO';
import { WareHouseItemService } from 'src/app/service/WareHouseItem.service';
import { WareHouseItemValidator } from 'src/app/validator/WareHouseItemValidator';

@Component({
  selector: 'app-WareHouseItemDetails',
  templateUrl: './WareHouseItemDetails.component.html',
  styleUrls: ['./WareHouseItemDetails.component.css']
})
export class WareHouseItemDetailsComponent implements OnInit {
  title = "Thông tin chi tiết vật tư";
  isDataLoaded: boolean = true;
  success = false;
  form!: FormGroup;
  dt!: WareHouseItemDTO;
  options!: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<WareHouseItemDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: WareHouseItemDTO,
    private formBuilder: FormBuilder,
    private service: WareHouseItemService
  ) {
  }
  ngOnInit(): void {
    this.dt=this.data;
    console.log(this.data)
    this.form = this.formBuilder.group({
      id: this.dt.id,
      code: this.dt.code,
      name: this.dt.name,
      categoryId: this.dt.categoryId,
      vendorId: this.dt.vendorId,
      country: this.dt.country,
      unitId: this.dt.unitId,
      description: this.dt.description,
      inactive: this.dt.inactive,
    });
  }
  get f() { return this.form.controls; }
  onNoClick(): void {
    this.dialogRef.close(false);
  }
}


