import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { InwardDetailDTO } from 'src/app/model/InwardDetailDTO';
import { WareHouseBookService } from 'src/app/service/WareHouseBook.service';
import { InwardDetailsValidator } from 'src/app/validator/InwardDetailsValidator';
import { InwarDetailsEditComponent } from '../../edit/InwarDetailsEdit/InwarDetailsEdit.component';

@Component({
  selector: 'app-InwardDetailDetails',
  templateUrl: './InwardDetailDetails.component.html',
  styleUrls: ['./InwardDetailDetails.component.scss']
})
export class InwardDetailDetailsComponent implements OnInit {
  title = "Thông tin vật tư phiếu nhập kho";
  success = false;
  form!: FormGroup;
  dt!: InwardDetailDTO;
  options!: FormGroup;
  serialWareHousesShow: any[] = [];
  constructor(
    public dialogRef: MatDialogRef<InwardDetailDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: InwardDetailDTO,
    private formBuilder: FormBuilder,
    private service: WareHouseBookService
  ) {  }
  
  ngOnInit() {
    this.dt = this.data;
    this.getData();
    this.form = this.formBuilder.group({
      id: this.dt.id,
      inwardId: this.dt.inwardId,
      itemId: this.dt.itemId,
      unitId: this.dt.unitId,
      uiquantity: this.dt.uiquantity,
      uiprice: this.dt.uiprice,
      amount: this.dt.amount,
      quantity: this.dt.quantity,
      price: this.dt.price,
      departmentId: this.dt.departmentId,
      departmentName: this.dt.departmentName,
      employeeId: this.dt.employeeId,
      employeeName: this.dt.employeeName,
      stationId: this.dt.stationId,
      stationName: this.dt.stationName,
      projectId: this.dt.projectId,
      projectName: this.dt.projectName,
      customerId: this.dt.customerId,
      customerName: this.dt.customerName,
      accountMore: this.dt.accountMore,
      accountYes: this.dt.accountYes,
      status: this.dt.status,
      serialWareHouses: this.dt.serialWareHouses
    });
  }

  getData() {
    const id = this.dt.id
    if (id !== null)
      this.service.InwarDetails(id).subscribe(x => {
        this.dt = x.data;
        this.serialWareHousesShow = this.data.serialWareHouses;
        this.form.patchValue({
          id: this.dt.id,
          inwardId: this.dt.inwardId,
          itemId: this.dt.itemId,
          unitId: this.dt.unitId,
          uiquantity: this.dt.uiquantity,
          uiprice: this.dt.uiprice,
          amount: this.dt.amount,
          quantity: this.dt.quantity,
          price: this.dt.price,
          departmentId: this.dt.departmentId,
          departmentName: this.dt.departmentName,
          employeeId: this.dt.employeeId,
          employeeName: this.dt.employeeName,
          stationId: this.dt.stationId,
          stationName: this.dt.stationName,
          projectId: this.dt.projectId,
          projectName: this.dt.projectName,
          customerId: this.dt.customerId,
          customerName: this.dt.customerName,
          accountMore: this.dt.accountMore,
          accountYes: this.dt.accountYes,
          status: this.dt.status,
          serialWareHouses: this.dt.serialWareHouses
        });
      });

  }
  get f() { return this.form.controls; }
  onNoClick(): void {
    this.dialogRef.close(false);
  }
}


