import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { Guid } from 'src/app/extension/Guid';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';
import { UnitDTO } from 'src/app/model/UnitDTO';
import { UnitService } from 'src/app/service/Unit.service';
import { UnitValidator } from 'src/app/validator/UnitValidator';
import { UnitCreateComponent } from '../../create/UnitCreate/UnitCreate.component';



@Component({
  selector: 'app-UnitEdit',
  templateUrl: './UnitEdit.component.html',
  styleUrls: ['./UnitEdit.component.scss']
})
export class UnitEditComponent implements OnInit {

  title = "Chỉnh sửa đơn vị tính";
  private readonly notifier!: NotifierService;
  success = false;
  form!: FormGroup;
  dt!: UnitDTO;
  options!: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<UnitCreateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UnitDTO,
    private formBuilder: FormBuilder,
    private service: UnitService,
    notifierService: NotifierService
  ) { this.notifier = notifierService; }
  ngOnInit() {
    this.dt = this.data;
    this.form = this.formBuilder.group({
      id:this.data.id,
      unitName: this.data.unitName,
      inactive: this.data.inactive,
    });
  }
  get f() { return this.form.controls; }
  onNoClick(): void {
    this.dialogRef.close(false);
  }

  onSubmit() {
    var test = new UnitValidator();
    var msg = test.validate(this.form.value);
    var check = JSON.stringify(msg) == '{}';
    if (check == true)
      this.service.Edit(this.form.value).subscribe(x => {
        if (x.success)
          this.dialogRef.close(x.success)
        // else
        //   this.notifier.notify('error', x.errors["msg"][0]);
      } ,   
      //   error => {
      //   if (error.error.errors.length === undefined)
      //     this.notifier.notify('error', error.error.message);
      //   else
      //     this.notifier.notify('error', error.error);
      // }
      );
    else {
      var message = '';
      for (const [key, value] of Object.entries(msg)) {
        message = message + " " + value;
      }
      this.notifier.notify('error', message);
    }

  }
}
