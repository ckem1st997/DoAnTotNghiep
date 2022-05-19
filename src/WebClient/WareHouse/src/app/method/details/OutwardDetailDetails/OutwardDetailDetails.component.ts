import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { OutwardDetailDTO } from 'src/app/model/OutwardDetailDTO';
import { WareHouseBookService } from 'src/app/service/WareHouseBook.service';

@Component({
  selector: 'app-OutwardDetailDetails',
  templateUrl: './OutwardDetailDetails.component.html',
  styleUrls: ['./OutwardDetailDetails.component.scss']
})

export class OutwardDetailDetailsComponent implements OnInit {
  title = "Thông tin vật tư phiếu xuất kho";
  success = false;
  form!: FormGroup;
  dt!: OutwardDetailDTO;
  options!: FormGroup;
  serialWareHousesShow: any[] = [];
  constructor(
    public dialogRef: MatDialogRef<OutwardDetailDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: OutwardDetailDTO,
    private formBuilder: FormBuilder,
    private service: WareHouseBookService
  ) {  }
  ngOnInit() {
    this.dt = this.data;
    this.getData();
    this.form = this.formBuilder.group({
      id: this.dt.id,
      inwardId: this.dt.outwardId,
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
      this.service.OutwarDetails(id).subscribe(x => {
        this.dt = x.data;
        this.serialWareHousesShow = this.data.serialWareHouses;
        this.form.patchValue({
          id: this.dt.id,
          inwardId: this.dt.outwardId,
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


