import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { ReportTotalSearchModel } from 'src/app/model/ReportTotalSearchModel';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';
import { WareHouseDTO } from 'src/app/model/WareHouseDTO';
import { WareHouseItemDTO } from 'src/app/model/WareHouseItemDTO';
import { WarehouseService } from 'src/app/service/warehouse.service';
import { WareHouseItemService } from 'src/app/service/WareHouseItem.service';
import { FormSearchReportTotalComponent } from '../FormSearchReportTotal/FormSearchReportTotal.component';

@Component({
  selector: 'app-FormSearchReportDetails',
  templateUrl: './FormSearchReportDetails.component.html',
  styleUrls: ['./FormSearchReportDetails.component.scss']
})
export class FormSearchReportDetailsComponent implements OnInit {
  private readonly notifier!: NotifierService;
  form!: FormGroup;
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

  listDropDownItem: ResultMessageResponse<WareHouseItemDTO> = {
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
    @Inject(MAT_DIALOG_DATA) public data: ReportTotalSearchModel,
    public dialogRef: MatDialogRef<FormSearchReportTotalComponent>,
    private serviceWH: WarehouseService,
    private serviceItem: WareHouseItemService,
    private formBuilder: FormBuilder,
    notifierService: NotifierService
  ) { this.notifier = notifierService; }
  ngOnInit() {
    const today = new Date();
    const month = today.getMonth();
    const year = today.getFullYear();
    const day = today.getDay();
    this.form = this.formBuilder.group({
      wareHouseId: this.data.wareHouseId,
      itemId: this.data.itemId,
      start: new FormControl(new Date( year , month-1 , day)),
      end: new FormControl(new Date(year, month, day)),
      keySearch:this.data.keySearch
    });
    this.getDropDown();
  }

  getDropDown() {
    this.serviceWH.getListDropDown().subscribe(x => this.listDropDown = x);
    this.serviceItem.getListDropDown().subscribe(x => this.listDropDownItem = x);
  }
  onNoClick(): void {
    this.dialogRef.close(false);
  }
  onSubmit() {
    var formValue = this.form.value;
    if (formValue !== undefined && formValue["wareHouseId"] && formValue["start"] && formValue["end"] && formValue["itemId"]) {
      this.dialogRef.close(this.form.value);
    }
    else if (!formValue["wareHouseId"])
      this.notifier.notify('error', 'Bạn chưa chọn kho !');
    else if (!formValue["itemId"])
      this.notifier.notify('error', 'Bạn chưa chọn vật tư !');
    else if (!formValue["start"] || !formValue["end"])
      this.notifier.notify('error', 'Bạn chưa chọn ngày !');
  }
}



