import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { WareHouseItemUnitDTO } from 'src/app/model/WareHouseItemUnitDTO';
import { WareHouseItemUnitService } from 'src/app/service/WareHouseItemUnit.service';

@Component({
  selector: 'app-WareHouseItemUnitDelelte',
  templateUrl: './WareHouseItemUnitDelelte.component.html',
  styleUrls: ['./WareHouseItemUnitDelelte.component.scss']
})
export class WareHouseItemUnitDelelteComponent implements OnInit {
  title = "Xoá đơn vị tính !"
  private readonly notifier!: NotifierService;
  success = false;
  dt!: WareHouseItemUnitDTO;
  constructor(
    public dialogRef: MatDialogRef<WareHouseItemUnitDelelteComponent>,
    @Inject(MAT_DIALOG_DATA) public data: WareHouseItemUnitDTO,
    private service: WareHouseItemUnitService,
    notifierService: NotifierService
  ) { this.notifier = notifierService; }
  ngOnInit() {
    this.dt = this.data;
  }
  onNoClick(): void {
    this.dialogRef.close(false);
  }
  onSubmit() {
    console.log(this.dt)
    var ids = Array<string>();
    ids.push(this.dt.id);
    if (this.dt !== undefined && ids.length > 0) {
      this.service.Delete(ids).subscribe(x => {
        if (x.success)
          this.dialogRef.close(x.success);
     //   else
       //   this.notifier.notify('error', x?.errors["msg"][0]);
      } ,  
      //    error => {
      //   if (error.error.errors.length === undefined)
      //     this.notifier.notify('error', error.error.message);
      //   else
      //     this.notifier.notify('error', error.error);
      // }
      );
    }
    else this.notifier.notify('error', 'Có lỗi xảy ra,bạn chưa chọn mục để xoá, xin vui lòng thử lại !');

  }
}


