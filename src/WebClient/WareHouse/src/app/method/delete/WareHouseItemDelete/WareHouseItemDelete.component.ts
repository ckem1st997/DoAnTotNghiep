import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { WareHouseItemDTO } from 'src/app/model/WareHouseItemDTO';
import { WareHouseItemService } from 'src/app/service/WareHouseItem.service';

@Component({
  selector: 'app-WareHouseItemDelete',
  templateUrl: './WareHouseItemDelete.component.html',
  styleUrls: ['./WareHouseItemDelete.component.css']
})
export class WareHouseItemDeleteComponent implements OnInit {
  title = "Xoá vật tư !"
  private readonly notifier!: NotifierService;
  success = false;
  dt: WareHouseItemDTO[]=[];
  constructor(
    public dialogRef: MatDialogRef<WareHouseItemDeleteComponent>,
    @Inject(MAT_DIALOG_DATA) public data: WareHouseItemDTO[],
    private service: WareHouseItemService,
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
    var ids = Array<string>();
    for (let index = 0; index < this.dt.length; index++) {
      const element = this.dt[index];
      ids.push(element.id);
    }
    if (this.dt !== undefined && this.dt.length>0) {
      this.service.Delete(ids).subscribe(x => {
        if (x.success)
          this.dialogRef.close(x.success)
       // else
        //  this.notifier.notify('error', x?.errors["msg"][0]);
      } , 
      //     error => {
      //   if (error.error.errors.length === undefined)
      //     this.notifier.notify('error', error.error.message);
      //   else
      //     this.notifier.notify('error', error.error);
      // }
      );
    }
    else this.notifier.notify('error', 'Có lỗi xảy ra, xin vui lòng thử lại !');
  }
}

