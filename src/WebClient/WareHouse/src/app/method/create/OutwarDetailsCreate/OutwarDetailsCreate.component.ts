import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { Guid } from 'src/app/extension/Guid';
import { OutwardDetailDTO } from 'src/app/model/OutwardDetailDTO';
import { WareHouseBookService } from 'src/app/service/WareHouseBook.service';
import { InwarDetailsCreateComponent } from '../InwarDetailsCreate/InwarDetailsCreate.component';
import { OutwardDetailsValidator } from './../../../validator/OutwardDetailsValidator';
import { InwardService } from './../../../service/Inward.service';
import { MatDialog } from '@angular/material/dialog';
import { SearchwarehouseitemComponent } from './../../search/searchwarehouseitem/searchwarehouseitem.component';
import { WareHouseItemDTO } from 'src/app/model/WareHouseItemDTO';
import { WareHouseItemService } from 'src/app/service/WareHouseItem.service';

@Component({
  selector: 'app-OutwarDetailsCreate',
  templateUrl: './OutwarDetailsCreate.component.html',
  styleUrls: ['./OutwarDetailsCreate.component.scss']
})
export class OutwarDetailsCreateComponent implements OnInit {
  title = "Thêm mới vật tư phiếu xuất kho";
  // itemsAsObjects: AutoCompleteModel[] = [];
  private readonly notifier!: NotifierService;
  modelCreate: WareHouseItemDTO[] = [];
  success = false;
  form!: FormGroup;
  dt!: OutwardDetailDTO;
  options!: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<OutwarDetailsCreateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: OutwardDetailDTO,
    private formBuilder: FormBuilder,
    notifierService: NotifierService,
    private service: WareHouseBookService,
    private serviceIn: InwardService,
    public dialog: MatDialog,
    private serviceitem: WareHouseItemService, 
  ) { this.notifier = notifierService; }
  ngOnInit() {
    this.dt = this.data;

    this.form = this.formBuilder.group({
      id: Guid.newGuid(),
      outwardId: this.dt.outwardId,
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
  changAmount() {
    var getUiQuantity = this.form.value['uiquantity'];
    var getUiPrice = this.form.value['uiprice'];
    let unitId = this.form.value['unitId'];
    this.form.patchValue({ amount: getUiPrice * getUiQuantity });
    var idSelect = this.form.value['itemId'];
    if (this.data.outward != null && this.data.outward.wareHouseId && idSelect)
      this.service.CheckQuantityIdItem(idSelect, this.data.outward?.wareHouseId, unitId).subscribe(
        (data) => {
          if (data) {
            if (data.data < getUiQuantity)
              this.notifier.notify("error", "Số lượng xuất ra đang quá số lượng tồn kho !");
          }
        }
      );
    else {
      this.notifier.notify("error", "Vui lòng chọn kho hoặc vật tư !");
    }

  }


  seachwhitem() {
    this.serviceitem.AddIndex().subscribe(x => {
      this.modelCreate = x.data;
      const dialogRef = this.dialog.open(SearchwarehouseitemComponent, {
        width: '550px',
        height: 'auto',
        data: this.modelCreate
      });

      dialogRef.afterClosed().subscribe(result => {
        var res = result;
        if (res) {

        }
      });

    });
   
  }


  changItem(e: any) {
    var idSelect = e.target.value.split(" ")[1];
    if (this.data.outward != null && this.data.outward.wareHouseId && idSelect)
      this.serviceIn.CheckItemExist(idSelect, this.data.outward?.wareHouseId).subscribe(
        (data) => {
          if (data.success) {
            this.service.GetUnitByIdItem(idSelect).subscribe(res => {
              this.dt.unitDTO = res.data;
              this.form.patchValue({ unitId: this.dt.wareHouseItemDTO?.find(x => x.id === idSelect)?.unitId ?? null });
            })
          }
          else {
            this.notifier.notify("error", "Vật tư chưa có trong kho !");
          }
        }
      );
    else {
      this.notifier.notify("error", "Vui lòng chọn kho hoặc vật tư !");
    }


  }
  onSubmit() {

    var test = new OutwardDetailsValidator();
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
      var serialWareHouses = this.form.value["serialWareHouses"];
      if (serialWareHouses !== undefined && serialWareHouses !== null)
        serialWareHouses.forEach((element: { id: string; itemId: string; serial: string; outwardDetailId: string; isOver: boolean } | null) => {
          if (element !== null) {
            element.id = Guid.newGuid();
            element.itemId = this.form.value["itemId"];
            element.outwardDetailId = this.form.value["id"];
            element.isOver = true;
          }
        });
      this.dialogRef.close(this.form.value);
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

