import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { UnitDTO } from 'src/app/model/UnitDTO';
import { UnitService } from 'src/app/service/Unit.service';

@Component({
  selector: 'app-UnitDelete',
  templateUrl: './UnitDelete.component.html',
  styleUrls: ['./UnitDelete.component.scss']
})
export class UnitDeleteComponent implements OnInit {

  title = "Xoá đơn vị tính !"
  private readonly notifier!: NotifierService;
  success = false;
  dt: UnitDTO[]=[];
  options!: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<UnitDeleteComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UnitDTO[],
    private formBuilder: FormBuilder,
    private service: UnitService,
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
        //   this.notifier.notify('error', x?.errors["msg"][0]);
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

