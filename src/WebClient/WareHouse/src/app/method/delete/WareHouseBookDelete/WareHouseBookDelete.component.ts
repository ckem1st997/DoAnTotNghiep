import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { WareHouseBookDTO } from 'src/app/model/WareHouseBookDTO';
import { InwardService } from 'src/app/service/Inward.service';
import { OutwardService } from 'src/app/service/Outward.service';
import { WareHouseBookService } from 'src/app/service/WareHouseBook.service';

@Component({
  selector: 'app-WareHouseBookDelete',
  templateUrl: './WareHouseBookDelete.component.html',
  styleUrls: ['./WareHouseBookDelete.component.scss']
})
export class WareHouseBookDeleteComponent implements OnInit {
  title = "";
  private readonly notifier!: NotifierService;
  success = false;
  options!: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<WareHouseBookDeleteComponent>,
    @Inject(MAT_DIALOG_DATA) public data: WareHouseBookDTO,
    private formBuilder: FormBuilder,
    private serviceIn: InwardService,
    private serviceOut: OutwardService,
    notifierService: NotifierService
  ) { this.notifier = notifierService; }
  ngOnInit() {
    this
    this.title = "Xoá " + this.data.type + " !";
  }
  onNoClick(): void {
    this.dialogRef.close(false);
  }
  onSubmit() {
    var ids = Array<string>();
    ids.push(this.data.id);
    if (this.data !== undefined) {
      if (this.data.type == "Phiếu nhập")
        this.serviceIn.Delete(ids).subscribe(x => {
          if (x.success)
            this.dialogRef.close(x.success)
        //  else
         //   this.notifier.notify('error', x?.errors["msg"][0]);
        },
        //  error => {
        //   if (error.error.errors.length === undefined)
        //     this.notifier.notify('error', error.error.message);
        //   else
        //     this.notifier.notify('error', error.error);
        // }
        );
      else if (this.data.type == "Phiếu xuất")
        this.serviceOut.Delete(ids).subscribe(x => {
          if (x.success)
            this.dialogRef.close(x.success)
       //   else
         //   this.notifier.notify('error', x?.errors["msg"][0]);
        },
        //  error => {
        //   if (error.error.errors.length === undefined)
        //     this.notifier.notify('error', error.error.message);
        //   else
        //     this.notifier.notify('error', error.error);
        // }
        );
      else this.notifier.notify('error', 'Có lỗi xảy ra, xin vui lòng thử lại !');

    }
    else this.notifier.notify('error', 'Có lỗi xảy ra, xin vui lòng thử lại !');

  }
}

