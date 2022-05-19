import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { Guid } from 'src/app/extension/Guid';
import { VendorDTO } from 'src/app/model/VendorDTO';
import { WareHouseDTO } from 'src/app/model/WareHouseDTO';
import { VendorService } from 'src/app/service/VendorService.service';
import { WarehouseService } from 'src/app/service/warehouse.service';


@Component({
  selector: 'app-WareHouseDelete',
  templateUrl: './WareHouseDelete.component.html',
  styleUrls: ['./WareHouseDelete.component.scss']
})
export class WareHouseDeleteComponent implements OnInit {
  title = "Xoá kho vận !"
  private readonly notifier!: NotifierService;
  success = false;
  dt: WareHouseDTO[]=[];
  constructor(
    public dialogRef: MatDialogRef<WareHouseDeleteComponent>,
    @Inject(MAT_DIALOG_DATA) public data: WareHouseDTO[],
    private service: WarehouseService,
    notifierService: NotifierService
  ) { this.notifier = notifierService; }
  ngOnInit() {
    for (let index = 0; index <  this.data.length; index++) {
      const element =  this.data[index];
      this.dt.push(element);
    }

  }
  onNoClick(): void {
    this.dialogRef.close(false);
  }
  onSubmit() {
    this.dialogRef.close(true);
   
  }
}

