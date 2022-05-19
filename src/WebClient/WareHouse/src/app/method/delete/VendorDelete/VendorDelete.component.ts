import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { Guid } from 'src/app/extension/Guid';
import { VendorDTO } from 'src/app/model/VendorDTO';
import { VendorService } from 'src/app/service/VendorService.service';

@Component({
  selector: 'app-VendorDelete',
  templateUrl: './VendorDelete.component.html',
  styleUrls: ['./VendorDelete.component.scss']
})
export class VendorDeleteComponent implements OnInit {
  title = "Xoá nhà cung cấp !"
  private readonly notifier!: NotifierService;
  success = false;
  dt: VendorDTO[]=[];
  options!: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<VendorDeleteComponent>,
    @Inject(MAT_DIALOG_DATA) public data: VendorDTO[],
    private formBuilder: FormBuilder,
    private service: VendorService,
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
      this.service.DeleteVendor(ids).subscribe(x => {
        if (x.success)
          this.dialogRef.close(x.success)
     //   else
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

