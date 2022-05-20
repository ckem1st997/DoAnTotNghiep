import { HostListener, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Guid } from 'src/app/extension/Guid';
import { InwardDTO } from 'src/app/model/InwardDTO';
import { InwardService } from 'src/app/service/Inward.service';
import { Component, ViewChild } from '@angular/core';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { InwarDetailsCreateComponent } from '../InwarDetailsCreate/InwarDetailsCreate.component';
import { WareHouseBookService } from 'src/app/service/WareHouseBook.service';
import { InwardDetailDTO } from 'src/app/model/InwardDetailDTO';
import { WareHouseItemDTO } from 'src/app/model/WareHouseItemDTO';
import { UnitDTO } from 'src/app/model/UnitDTO';
import { BaseSelectDTO } from 'src/app/model/BaseSelectDTO';
import { InwarDetailsEditComponent } from '../../edit/InwarDetailsEdit/InwarDetailsEdit.component';
import { InwardValidator } from 'src/app/validator/InwardValidator';
import { SignalRService } from 'src/app/service/SignalR.service';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';
import { KafKaService } from 'src/app/service/KafKa.service';
@Component({
  selector: 'app-InwardCreate',
  templateUrl: './InwardCreate.component.html',
  styleUrls: ['./InwardCreate.component.scss']
})
export class InwardCreateComponent implements OnInit, OnDestroy {
  form!: FormGroup;
  listDetails = Array<InwardDetailDTO>();
  listItem = Array<WareHouseItemDTO>();
  listUnit = Array<UnitDTO>();
  getDepartmentDTO = Array<BaseSelectDTO>();
  getEmployeeDTO = Array<BaseSelectDTO>();
  getStationDTO = Array<BaseSelectDTO>();
  getProjectDTO = Array<BaseSelectDTO>();
  getCustomerDTO = Array<BaseSelectDTO>();
  getAccountDTO = Array<BaseSelectDTO>()
  dt: InwardDTO = {
    id: "",
    voucherCode: null,
    voucherDate: null,
    wareHouseId: null,
    deliver: null,
    receiver: null,
    vendorId: null,
    reason: null,
    reasonDescription: null,
    description: null,
    reference: null,
    createdDate: null,
    createdBy: null,
    modifiedDate: null,
    modifiedBy: null,
    deliverPhone: null,
    deliverAddress: null,
    deliverDepartment: null,
    receiverPhone: null,
    receiverAddress: null,
    receiverDepartment: null,
    wareHouseDTO: [],
    vendorDTO: [],
    domainEvents: [],
    voucher: null,
    getCreateBy: [],
    inwardDetails: []
  };
  private readonly notifier!: NotifierService;
  displayedColumns: string[] = ['id', 'itemId', 'unitId', 'uiquantity', 'uiprice', 'amount', 'departmentName', 'employeeName', 'stationName', 'projectName', 'customerName', 'method'];
  dataSource = new MatTableDataSource<InwardDetailDTO>();
  @ViewChild(MatTable)
  table!: MatTable<InwardDetailDTO>;

  constructor(private kafka: KafKaService, private signalRService: SignalRService, private routerde: Router, private serviceBook: WareHouseBookService, notifierService: NotifierService, public dialog: MatDialog, private formBuilder: FormBuilder, private route: ActivatedRoute, private service: InwardService) {
    this.notifier = notifierService;
  }
  @HostListener('window:resize', ['$event'])

  onWindowResize(): void {
    const getScreenHeight = window.innerHeight;
    var clientHeight = document.getElementById('formCreate') as HTMLFormElement;
    var clientHeightForm = document.getElementById('formCreateDiv') as HTMLFormElement;
    const table = document.getElementById("formTable") as HTMLDivElement;
    table.style.height = getScreenHeight - 75 - clientHeight.clientHeight + "px";
    clientHeightForm.style.paddingTop = clientHeight.clientHeight + "px";
  }
  ngOnInit() {
    // this.signalRService.hubConnection.on(this.signalRService.CreateWareHouseBookTrachking, (data: ResultMessageResponse<string>) => {
    // });
    this.onWindowResize();
    const whid = this.route.snapshot.paramMap.get('whid');
    this.service.AddIndex(whid).subscribe(x => {
      this.dt = x.data;
      this.form.patchValue(x.data);
      this.form.patchValue({
        voucherDate: new Date().toISOString().slice(0, 16),
        createdDate: new Date().toISOString().slice(0, 16),
        modifiedDate: new Date().toISOString().slice(0, 16),
      })
    });
    this.form = this.formBuilder.group({
      id: null,
      voucherCode: this.dt.voucherCode,
      voucher: this.dt.voucher,
      voucherDate: new Date().toISOString().slice(0, 16),
      wareHouseId: this.route.snapshot.paramMap.get('whid'),
      deliver: null,
      receiver: null,
      vendorId: null,
      reason: null,
      reasonDescription: null,
      description: null,
      reference: null,
      createdDate: new Date().toISOString().slice(0, 16),
      createdBy: null,
      modifiedDate: new Date().toISOString().slice(0, 16),
      modifiedBy: null,
      deliverPhone: null,
      deliverAddress: null,
      deliverDepartment: null,
      receiverPhone: null,
      receiverAddress: null,
      receiverDepartment: null,
      inwardDetails: null
    });

  }
  ngOnDestroy(): void {
    // tắt phương thức vừa gọi để tránh bị gọi lại nhiều lần
    this.signalRService.hubConnection.off(this.signalRService.CreateWareHouseBookTrachking);
  }
  getCreate() {


  }
  getNameItem(id: string) {
    return this.listItem.find(x => x.id === id)?.name;
  }
  getNameUnit(id: string) {
    return this.listUnit.find(x => x.id === id)?.unitName;
  }
  addData() {
    this.serviceBook.AddInwarDetailsIndex().subscribe(x => {
      const model = x.data;
      // lấy data từ api gán vào biến tạm
      this.listItem = x.data.wareHouseItemDTO;
      this.listUnit = x.data.unitDTO;
      this.getCustomerDTO = x.data.getCustomerDTO;
      this.getAccountDTO = x.data.getAccountDTO;
      this.getDepartmentDTO = x.data.getDepartmentDTO;
      this.getEmployeeDTO = x.data.getEmployeeDTO;
      this.getProjectDTO = x.data.getProjectDTO;
      this.getStationDTO = x.data.getStationDTO;
      model.inwardId = this.form.value["id"];
      const dialogRef = this.dialog.open(InwarDetailsCreateComponent, {
        width: '450px',
        data: model
      });

      dialogRef.afterClosed().subscribe(result => {
        var res = result;
        console.log(res);
        if (res) {
          this.listDetails.push(result);
          this.dataSource.data = this.listDetails;
          this.table.renderRows();
        }
      });

    });

  }
  openDialogDelelte(id: string): void {
    const model = this.listDetails.find(x => x.id === id);
    if (model !== undefined) {
      this.listDetails = this.listDetails.filter(x => x !== this.listDetails.find(x => x.id === id));
      this.dataSource.data = this.listDetails;
      this.table.renderRows();
      this.notifier.notify('success', 'Xóa thành công');
    }
    else
      this.notifier.notify('error', 'Xóa thất bại');

  }


  openDialogedit(id: string): void {

    const model = this.listDetails.find(x => x.id === id);
    if (model !== undefined) {
      // gán data từ biến tạm gán vào biến model, để tránh gọi sang api lấy lại data
      if (model.wareHouseItemDTO.length < 1) this.listItem.forEach(element => { model.wareHouseItemDTO.push(element) });
      if (model.unitDTO.length < 1) this.listUnit.forEach(element => { model.unitDTO.push(element) });
      if (model.getCustomerDTO.length < 1) this.getCustomerDTO.forEach(element => { model.getCustomerDTO.push(element) });
      if (model.getDepartmentDTO.length < 1) this.getDepartmentDTO.forEach(element => { model.getDepartmentDTO.push(element) });
      if (model.getEmployeeDTO.length < 1) this.getEmployeeDTO.forEach(element => { model.getEmployeeDTO.push(element) });
      if (model.getProjectDTO.length < 1) this.getProjectDTO.forEach(element => { model.getProjectDTO.push(element) });
      if (model.getStationDTO.length < 1) this.getStationDTO.forEach(element => { model.getStationDTO.push(element) });
      if (model.getAccountDTO.length < 1) this.getAccountDTO.forEach(element => { model.getAccountDTO.push(element) });

      const dialogRef = this.dialog.open(InwarDetailsEditComponent, {
        width: '550px',
        data: model,
      });

      dialogRef.afterClosed().subscribe(result => {
        var res = result;
        if (res) {
          // this.listDetails = this.listDetails.filter(x => x !== this.listDetails.find(x => x.id === res.id));
          // this.listDetails.push(res);
          this.listDetails[this.listDetails.findIndex(x => x.id === res.id)] = res;
          this.dataSource.data = this.listDetails;
          this.table.renderRows();
        }
      });
    }

  }


  removeData() {
    this.dataSource.data.pop();
    this.table.renderRows();
  }
  saveKafka() {
    this.form.value["voucher"] = this.dt.voucher;
    var test = new InwardValidator();
    var msg = test.validate(this.form.value);
    var check = JSON.stringify(msg) == '{}';
    if (check == true) {
      var checkDetails = this.listDetails.length > 0;
      if (checkDetails == true) {
        this.form.value["inwardDetails"] = this.listDetails;
        this.kafka.Add(this.form.value).subscribe(x => {
          if (x.success) {
            this.notifier.notify('success', x.message);
          }
        }
        );
      }
      else {
        this.notifier.notify('error', 'Vui lòng nhập chi tiết phiếu nhập');
      }


    }

    else {
      var message = '';
      for (const [key, value] of Object.entries(msg)) {
        message = message + " " + value;
      }
      this.notifier.notify('error', message);
    }

  }
  onSubmit() {
    // this.signalRService.SendCreateWareHouseBookTrachking('nhập kho');
    this.form.value["voucher"] = this.dt.voucher;
    var test = new InwardValidator();
    var msg = test.validate(this.form.value);
    var check = JSON.stringify(msg) == '{}';
    if (check == true) {
      var checkDetails = this.listDetails.length > 0;
      if (checkDetails == true) {
        this.form.value["inwardDetails"] = this.listDetails;
        this.service.Add(this.form.value).subscribe(x => {
          if (x.success) {
            this.signalRService.SendCreateWareHouseBookTrachking('nhập kho');
            this.notifier.notify('success', 'Thêm thành công');
            console.log(x);
            // if (x.data)
            //   this.signalRService.SendHistoryTrachking();
            this.routerde.navigate(['wh/warehouse-book']);

            //   this.routerde.navigate(['/details-inward', this.form.value["id"]]);
          }
          // commnet vì xử dụng bộ đánh chặn để thông báo thay vì ở đây
          // else
          //   this.notifier.notify('error', x.errors["msg"][0]);
        }
        );
      }
      else {
        this.notifier.notify('error', 'Vui lòng nhập chi tiết phiếu nhập');
      }


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
