import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { Guid } from 'src/app/extension/Guid';
import { AutoCompleteModel } from 'src/app/model/AutoCompleteModel';
import { InwardDetailDTO } from 'src/app/model/InwardDetailDTO';
import { WareHouseDTO } from 'src/app/model/WareHouseDTO';
import { WareHouseItemDTO } from 'src/app/model/WareHouseItemDTO';
import { WareHouseBookService } from 'src/app/service/WareHouseBook.service';
import { InwardDetailsValidator } from 'src/app/validator/InwardDetailsValidator';
import { VendorValidator } from 'src/app/validator/VendorValidator';

@Component({
  selector: 'app-InwarDetailsCreate',
  templateUrl: './InwarDetailsCreate.component.html',
  styleUrls: ['./InwarDetailsCreate.component.scss']
})
export class InwarDetailsCreateComponent implements OnInit {
  title = "Thêm mới vật tư phiếu nhập kho";
  keyword = 'name';
  // itemsAsObjects: AutoCompleteModel[] = [];
  private readonly notifier!: NotifierService;
  success = false;
  form!: FormGroup;
  dt!: InwardDetailDTO;
  options!: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<InwarDetailsCreateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: InwardDetailDTO,
    private formBuilder: FormBuilder,
    notifierService: NotifierService,
    private service: WareHouseBookService
  ) { this.notifier = notifierService; }
  ngOnInit() {
    this.dt = this.data;
    this.form = this.formBuilder.group({
      id: this.dt.id,
      inwardId: this.dt.inwardId,
      itemId: null,
      unitId: null,
      uiquantity: 0,
      uiprice: 0,
      amount: 0,
      quantity: 0,
      price: 0,
      departmentId: null,
      departmentName: null,
      employeeId: null,
      employeeName: null,
      stationId: null,
      stationName: null,
      projectId: null,
      projectName: null,
      customerId: null,
      customerName: null,
      accountMore: null,
      accountYes: null,
      status: null,
      serialWareHouses: []
    });
  }
  get f() { return this.form.controls; }
  onNoClick(): void {
    this.dialogRef.close(false);
  }

  selectEvent(item: WareHouseItemDTO) {
    this.form.patchValue({ itemId: item.id ?? null });
    this.service.GetUnitByIdItem(item.id).subscribe(res => {
      this.dt.unitDTO = res.data;
      this.form.patchValue({ unitId: this.dt.wareHouseItemDTO?.find(x => x.id === item.id)?.unitId ?? null });
    })
  }

  onChangeSearch(val: string) {
    console.log(val);
  }

  onFocused(e: any) {
    console.log(e);
  }

  changAmount() {
    var getUiQuantity = this.form.value['uiquantity'];
    var getUiPrice = this.form.value['uiprice'];
    this.form.patchValue({ amount: getUiPrice * getUiQuantity });
  }
  changItem(e: any) {
    var idSelect = e.target.value.split(" ")[1];
    this.service.GetUnitByIdItem(idSelect).subscribe(res => {
      this.dt.unitDTO = res.data;
      this.form.patchValue({ unitId: this.dt.wareHouseItemDTO?.find(x => x.id === idSelect)?.unitId ?? null });
    })

  }
  onSubmit() {
    console.log(this.form.value);

    var test = new InwardDetailsValidator();
    var msg = test.validate(this.form.value);
    var check = JSON.stringify(msg) == '{}';
    if (check == true) {
      // tìm name của item
      this.form.value["departmentName"] = this.dt.getDepartmentDTO.find(x => x.id === this.form.value["departmentId"])?.name;
      this.form.value["employeeName"] = this.dt.getEmployeeDTO.find(x => x.id === this.form.value["employeeId"])?.name;
      this.form.value["stationName"] = this.dt.getStationDTO.find(x => x.id === this.form.value["stationId"])?.name;
      this.form.value["customerName"] = this.dt.getCustomerDTO.find(x => x.id === this.form.value["customerId"])?.name;
      this.form.value["projectName"] = this.dt.getProjectDTO.find(x => x.id === this.form.value["projectId"])?.name;
      this.form.value["unitDTO"] = [];
      this.form.value["wareHouseItemDTO"] = [];
      this.form.value["getDepartmentDTO"] = [];
      this.form.value["getEmployeeDTO"] = [];
      this.form.value["getStationDTO"] = [];
      this.form.value["getProjectDTO"] = [];
      this.form.value["getCustomerDTO"] = [];
      this.form.value["getAccountDTO"] = [];

      const serialWareHouses = this.form.value["serialWareHouses"];
      //???
      if (serialWareHouses !== undefined && serialWareHouses !== null)
        serialWareHouses.forEach((element: { id: string; itemId: string; serial: string; inwardDetailId: string; isOver: boolean } | null) => {
          if (element !== null) {
            element.id = Guid.newGuid();
            element.itemId = this.form.value["itemId"];
            element.inwardDetailId = this.form.value["id"];
            element.isOver = true;
          }
        });

      this.dialogRef.close(this.form.value);
      // this.notifier.notify('success', 'Thêm thành công !');
    }
    else {
      var message = '';
      for (const [key, value] of Object.entries(msg)) {
        message = message + " " + value;
      }
      this.notifier.notify('error', message);
    }

  }
}

