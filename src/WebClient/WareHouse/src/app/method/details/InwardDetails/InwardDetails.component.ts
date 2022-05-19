import { ChangeDetectorRef, Component, HostListener, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource, MatTable } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { NotifierService } from 'angular-notifier';
import { Guid } from 'src/app/extension/Guid';
import { BaseSelectDTO } from 'src/app/model/BaseSelectDTO';
import { InwardDetailDTO } from 'src/app/model/InwardDetailDTO';
import { InwardDTO } from 'src/app/model/InwardDTO';
import { UnitDTO } from 'src/app/model/UnitDTO';
import { WareHouseItemDTO } from 'src/app/model/WareHouseItemDTO';
import { InwardService } from 'src/app/service/Inward.service';
import { WareHouseBookService } from 'src/app/service/WareHouseBook.service';
import { InwardValidator } from 'src/app/validator/InwardValidator';
import { InwarDetailsCreateComponent } from '../../create/InwarDetailsCreate/InwarDetailsCreate.component';
import { InwarDetailsEditByServiceComponent } from '../../edit/InwarDetailsEditByService/InwarDetailsEditByService.component';
import { InwardDetailDetailsComponent } from '../InwardDetailDetails/InwardDetailDetails.component';
import { environment } from 'src/environments/environment';
import { SignalRService } from 'src/app/service/SignalR.service';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';

@Component({
  selector: 'app-InwardDetails',
  templateUrl: './InwardDetails.component.html',
  styleUrls: ['./InwardDetails.component.scss']
})
export class InwardDetailsComponent implements OnInit,OnDestroy {
  baseAPI: string = environment.baseApi;


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

  constructor(private route1: Router,public signalRService: SignalRService, private changeDetectorRefs: ChangeDetectorRef, private serviceBook: WareHouseBookService, notifierService: NotifierService, public dialog: MatDialog, private formBuilder: FormBuilder, private route: ActivatedRoute, private service: InwardService) {
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
  ngOnDestroy(): void {
    // tắt phương thức vừa gọi để tránh bị gọi lại nhiều lần
    this.signalRService.hubConnection.off(this.signalRService.WareHouseBookTrachkingToCLient);
    this.signalRService.hubConnection.off(this.signalRService.DeleteWareHouseBookTrachking);

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
      id: null,
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

  getData() {
    const id = this.route.snapshot.paramMap.get('id');
    this.dt.id = id == null ? '' : id;
    if (id !== null)
      this.service.Details(id).subscribe(x => {
        if (x.success) {
          this.dt = x.data;
          this.listDetails = x.data.inwardDetails;
          this.removeData();
          this.dataSource.data = x.data.inwardDetails;
          this.table.renderRows();
          this.form.patchValue(x.data);
          this.changeDetectorRefs.detectChanges();
        }

      }
      );

    this.serviceBook.GetDataToWareHouseBookIndex().subscribe(x => {
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

  openDialogDetails(model: InwardDetailDTO): void {
    const dialogRef = this.dialog.open(InwardDetailDetailsComponent, {
      width: '550px',
      data: model
    });
  }

  exportExcel() {
    this.service.ExportExcel(this.dt.id).subscribe(x => {
      console.log(x);
      // var a = document.createElement("a");
      // a.href = window.URL.createObjectURL(x);
      // a.download = "Inward.xlsx";
      // document.body.appendChild(a);
      // a.click();
      // document.body.removeChild(a);
    });
  }
  removeData() {
    this.dataSource.data.pop();
    this.table.renderRows();
  }
}
