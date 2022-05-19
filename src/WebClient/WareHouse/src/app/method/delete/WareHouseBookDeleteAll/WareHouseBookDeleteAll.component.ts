import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { DeleteWareHouseBook } from 'src/app/model/DeleteWareHouseBook';
import { WareHouseBookDTO } from 'src/app/model/WareHouseBookDTO';
import { InwardService } from 'src/app/service/Inward.service';
import { OutwardService } from 'src/app/service/Outward.service';
import { WareHouseBookService } from 'src/app/service/WareHouseBook.service';
import { WareHouseBookDeleteComponent } from '../WareHouseBookDelete/WareHouseBookDelete.component';

@Component({
  selector: 'app-WareHouseBookDeleteAll',
  templateUrl: './WareHouseBookDeleteAll.component.html',
  styleUrls: ['./WareHouseBookDeleteAll.component.scss']
})
export class WareHouseBookDeleteAllComponent implements OnInit {
  title = "Xoá danh sách phiếu !";;
  private readonly notifier!: NotifierService;
  success = false;
  options!: FormGroup;
  idsDelete: DeleteWareHouseBook={
    idsIn: [],
    idsOut: []
  };
  constructor(
    public dialogRef: MatDialogRef<WareHouseBookDeleteAllComponent>,
    @Inject(MAT_DIALOG_DATA) public data: WareHouseBookDTO[],
    private formBuilder: FormBuilder,
    private service: WareHouseBookService,
    notifierService: NotifierService
  ) { this.notifier = notifierService; }
  ngOnInit() {
  }
  onNoClick(): void {
    this.dialogRef.close(false);
  }
  onSubmit() {
    for (let i = 0; i < this.data.length; i++) {
      if (this.data[i].type == "Phiếu nhập")
        this.idsDelete.idsIn.push(this.data[i].id);
      else if (this.data[i].type == "Phiếu xuất")
        this.idsDelete.idsOut.push(this.data[i].id);
    };
    if (this.data !== undefined && this.data.length > 0) {
      if (this.idsDelete.idsIn.length > 0 || this.idsDelete.idsOut.length > 0) {
        this.service.Delete(this.idsDelete).subscribe(x => {
          if (x.success)
            this.dialogRef.close(x.success);
        },
      
        );

      }
      else this.notifier.notify('error', 'Có lỗi xảy ra, xin vui lòng thử lại !');
    }
  }
}
