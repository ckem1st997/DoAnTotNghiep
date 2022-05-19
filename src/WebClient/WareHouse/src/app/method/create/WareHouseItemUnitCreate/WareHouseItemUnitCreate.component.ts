import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { Guid } from 'src/app/extension/Guid';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';
import { UnitDTO } from 'src/app/model/UnitDTO';
import { WareHouseItemDTO } from 'src/app/model/WareHouseItemDTO';
import { WareHouseItemUnitDTO } from 'src/app/model/WareHouseItemUnitDTO';
import { UnitService } from 'src/app/service/Unit.service';
import { WareHouseItemUnitService } from 'src/app/service/WareHouseItemUnit.service';
import { WareHouseItemUnitValidator } from 'src/app/validator/WareHouseItemUnitValidator';
import { WareHouseItemValidator } from 'src/app/validator/WareHouseItemValidator';

@Component({
  selector: 'app-WareHouseItemUnitCreate',
  templateUrl: './WareHouseItemUnitCreate.component.html',
  styleUrls: ['./WareHouseItemUnitCreate.component.scss']
})
export class WareHouseItemUnitCreateComponent implements OnInit {
  title = "Thêm đơn vị tính";
  isDataLoaded: boolean = true;
  private readonly notifier!: NotifierService;
  success = false;
  form!: FormGroup;
  dt!: WareHouseItemDTO;
  options!: FormGroup;
  listDropDown: ResultMessageResponse<UnitDTO> = {
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
    public dialogRef: MatDialogRef<WareHouseItemUnitCreateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: WareHouseItemDTO,
    private formBuilder: FormBuilder,
    private service: WareHouseItemUnitService,
    notifierService: NotifierService,
    private unitservice: UnitService
  ) {
    this.notifier = notifierService;
  }
  ngOnInit(): void {
    this.dt = this.data;
    this.form = this.formBuilder.group({
      id: Guid.newGuid(),
      itemId: this.dt.id,
      unitId: null,
      convertRate: 1,
      isPrimary: true,
    });
    this.unitservice.getListDropDown().subscribe(x => this.listDropDown.data = x.data);
  }
  get f() { return this.form.controls; }
  onNoClick(): void {
    this.dialogRef.close(false);
  }

  onSubmit() {
    var test = new WareHouseItemUnitValidator();
    var msg = test.validate(this.form.value);
    var check = JSON.stringify(msg) == '{}';
    if (check == true)
      this.dialogRef.close(this.form.value)
    else {
      var message = '';
      for (const [key, value] of Object.entries(msg)) {
        message = message + " " + value;
      }
      this.notifier.notify('error', message);
    }
  }
}

