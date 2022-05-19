import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { BeginningWareHouseDTO } from 'src/app/model/BeginningWareHouseDTO';
import { BeginningWareHouseService } from 'src/app/service/BeginningWareHouse.service';

@Component({
  selector: 'app-WareHouseBenginingCreateDelete',
  templateUrl: './WareHouseBenginingCreateDelete.component.html',
  styleUrls: ['./WareHouseBenginingCreateDelete.component.scss']
})
export class WareHouseBenginingCreateDeleteComponent implements OnInit {
  title = "Xoá tồn kho !"
  private readonly notifier!: NotifierService;
  success = false;
  dt: BeginningWareHouseDTO[]=[];
  constructor(
    public dialogRef: MatDialogRef<WareHouseBenginingCreateDeleteComponent>,
    @Inject(MAT_DIALOG_DATA) public data: BeginningWareHouseDTO[],
    private service: BeginningWareHouseService,
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
      //  else
        //  this.notifier.notify('error', x?.errors["msg"][0]);
      } ,   
      //   error => {
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

