import { Component, HostListener, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource, MatTable } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { NotifierService } from 'angular-notifier';
import { Guid } from 'src/app/extension/Guid';
import { BaseSelectDTO } from 'src/app/model/BaseSelectDTO';
import { InwardDetailDTO } from 'src/app/model/InwardDetailDTO';
import { InwardDTO } from 'src/app/model/InwardDTO';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';
import { UnitDTO } from 'src/app/model/UnitDTO';
import { WareHouseItemDTO } from 'src/app/model/WareHouseItemDTO';
import { InwardService } from 'src/app/service/Inward.service';
import { SignalRService } from 'src/app/service/SignalR.service';
import { WareHouseBookService } from 'src/app/service/WareHouseBook.service';
import { InwardValidator } from 'src/app/validator/InwardValidator';
import { InwarDetailsCreateComponent } from '../../create/InwarDetailsCreate/InwarDetailsCreate.component';
import { InwarDetailsEditComponent } from '../InwarDetailsEdit/InwarDetailsEdit.component';
import { InwarDetailsEditByServiceComponent } from '../InwarDetailsEditByService/InwarDetailsEditByService.component';

@Component({
  selector: 'app-InwardEdit',
  templateUrl: './InwardEdit.component.html',
  styleUrls: ['./InwardEdit.component.scss']
})
export class InwardEditComponent implements OnInit, OnDestroy {
  title: string = "";
  form!: FormGroup;
  listDetails = Array<InwardDetailDTO>();
  listItem = Array<WareHouseItemDTO>();
  listUnit = Array<UnitDTO>();
  getDepartmentDTO = Array<BaseSelectDTO>();
  getEmployeeDTO = Array<BaseSelectDTO>();
  getStationDTO = Array<BaseSelectDTO>();
  getProjectDTO = Array<BaseSelectDTO>();
  getCustomerDTO = Array<BaseSelectDTO>();
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

  constructor(private route1: Router, public signalRService: SignalRService, private serviceBook: WareHouseBookService, notifierService: NotifierService, public dialog: MatDialog, private formBuilder: FormBuilder, private route: ActivatedRoute, private service: InwardService) {
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
    this.signalRService.hubConnection.on(this.signalRService.WareHouseBookTrachkingToCLient, (data: ResultMessageResponse<string>) => {
      if (data.success) {
        if (this.form.value["id"] === data.data) {
          this.notifier.notify('success', data.message);
          this.getData();
        }
      }
    });
    this.signalRService.hubConnection.on(this.signalRService.DeleteWareHouseBookTrachking, (data: ResultMessageResponse<string>) => {
      if (data.success && data.data.includes(this.form.value["id"])) {
        this.notifier.notify('success', "Phiếu đã bị xoá !");
        this.route1.navigate(['/wh/warehouse-book']);
      }
    });
    this.onWindowResize();
    this.getData();
    this.form = this.formBuilder.group({
      id: Guid.newGuid(),
      voucherCode: null,
      voucher: this.dt.voucher,
      voucherDate: new Date().toISOString().slice(0, 16),
      wareHouseId: null,
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
    this.signalRService.hubConnection.off(this.signalRService.WareHouseBookTrachkingToCLient);
    this.signalRService.hubConnection.off(this.signalRService.DeleteWareHouseBookTrachking);

  }
  getData() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id !== null)
      this.service.EditIndex(id).subscribe(x => {
        this.dt = x.data;
        this.listDetails = this.dt.inwardDetails;
        this.dataSource.data = this.dt.inwardDetails;
        this.table.renderRows();
        this.form.patchValue(x.data);
      });

    this.serviceBook.AddInwarDetailsIndex().subscribe(x => {
      this.listItem = x.data.wareHouseItemDTO;
      this.listUnit = x.data.unitDTO;
    });

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
        if (res) {
          this.serviceBook.AddInwarDetails(res).subscribe(x => {
            if (x.success) {
              this.listDetails.push(result);
              this.dataSource.data = this.listDetails;
              this.table.renderRows();
              this.notifier.notify('success', 'Thêm thành công');
              this.signalRService.SendWareHouseBookTrachking(this.form.value["id"]);
              if (x.data)
                this.signalRService.SendHistoryTrachking();
            }
            else
              this.notifier.notify('ward', 'Thêm thất bại');

          },

          );
        }
      });

    });

  }
  openDialogDelelte(id: string): void {
    const model = this.listDetails.find(x => x.id === id);
    if (model !== undefined) {
      var ids = new Array<string>();
      ids.push(id);
      this.serviceBook.DeleteInwarDetails(ids).subscribe(x => {
        if (x.success) {
          this.listDetails = this.listDetails.filter(x => x !== this.listDetails.find(x => x.id === id));
          this.dataSource.data = this.listDetails;
          this.table.renderRows();
          this.notifier.notify('success', 'Xóa thành công');
          this.signalRService.SendWareHouseBookTrachking(this.form.value["id"]);
          if (x.data)
            this.signalRService.SendHistoryTrachking();
        }
        else
          this.notifier.notify('error', 'Xóa thất bại');


      },
      );
    }
    else
      this.notifier.notify('error', 'Xóa thất bại');

  }


  openDialogedit(id: string): void {
    this.serviceBook.EditInwarDetailsIndex(id).subscribe(x => {
      const dialogRef = this.dialog.open(InwarDetailsEditByServiceComponent, {
        width: '550px',
        data: x.data,
      });

      dialogRef.afterClosed().subscribe(result => {
        var res = result;
        if (res) {
          this.serviceBook.EditInwarDetailsIndexByModel(result).subscribe(x => {
            if (x.success) {
              this.listDetails = this.listDetails.filter(x => x !== this.listDetails.find(x => x.id === res.id));
              this.listDetails.push(res);
              this.dataSource.data = this.listDetails;
              this.table.renderRows();
              this.notifier.notify('success', 'Sửa thành công');
              this.signalRService.SendWareHouseBookTrachking(this.form.value["id"]);
              if (x.data)
                this.signalRService.SendHistoryTrachking();
            }

          },
          );

        }
      });
    });
  }


  removeData() {
    this.dataSource.data.pop();
    this.table.renderRows();
  }

  onSubmit() {
    this.form.value["voucher"] = this.dt.voucher;
    var test = new InwardValidator();
    var msg = test.validate(this.form.value);
    var check = JSON.stringify(msg) == '{}';
    if (check == true) {
      var checkDetails = this.listDetails.length > 0;
      if (checkDetails == true) {
        this.form.value["inwardDetails"] = this.listDetails;
        this.service.Edit(this.form.value).subscribe(x => {
          if (x.success) {
            this.notifier.notify('success', 'Chỉnh sửa thành công');
            this.signalRService.SendWareHouseBookTrachking(this.form.value["id"]);
            if (x.data)
              this.signalRService.SendHistoryTrachking();
          }
        },
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
