import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';
import { UnitDTO } from 'src/app/model/UnitDTO';
import { UnitService } from 'src/app/service/Unit.service';
import { UnitValidator } from 'src/app/validator/UnitValidator';
import { UnitCreateComponent } from '../../create/UnitCreate/UnitCreate.component';

@Component({
  selector: 'app-UnitDetails',
  templateUrl: './UnitDetails.component.html',
  styleUrls: ['./UnitDetails.component.scss']
})
export class UnitDetailsComponent implements OnInit {
  title = "Thông tin chi tiết đơn vị tính";
  success = false;
  form!: FormGroup;
  dt!: UnitDTO;
  options!: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<UnitCreateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UnitDTO,
    private formBuilder: FormBuilder,
    private service: UnitService
  ) {  }
  ngOnInit() {
    this.dt = this.data;
    this.form = this.formBuilder.group({
      id:this.data.id,
      unitName: this.data.unitName,
      inactive: this.data.inactive,
    });
  }
  get f() { return this.form.controls; }
  onNoClick(): void {
    this.dialogRef.close(false);
  }
}
