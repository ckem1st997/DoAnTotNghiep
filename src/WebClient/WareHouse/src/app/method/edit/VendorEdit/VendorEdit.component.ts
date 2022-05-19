import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { VendorDTO } from 'src/app/model/VendorDTO';
import { VendorService } from 'src/app/service/VendorService.service';
import { VendorValidator } from 'src/app/validator/VendorValidator';
export interface DialogData {
  animal: string;
  name: string;
}
@Component({
  selector: 'app-VendorEdit',
  templateUrl: './VendorEdit.component.html',
  styleUrls: ['./VendorEdit.component.scss']
})
export class VendorEditComponent implements OnInit {
  title = "Chỉnh sửa nhà cung cấp";
  private readonly notifier!: NotifierService;
  success = false;
  form!: FormGroup;
  dt!: VendorDTO;
  options!: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<VendorEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: VendorDTO,
    private formBuilder: FormBuilder,
    private service: VendorService,
    notifierService: NotifierService
  ) { this.notifier = notifierService; }
  ngOnInit() {
    this.dt = this.data;
    this.form = this.formBuilder.group({
      id: this.dt.id,
      code: this.dt.code,
      name: this.dt.name,
      address: this.dt.address,
      phone: this.dt.phone,
      email: this.dt.email,
      contactPerson: this.dt.contactPerson,
      inactive: this.dt.inactive,
    });
  }
  get f() { return this.form.controls; }
  onNoClick(): void {
    this.dialogRef.close(false);
  }
  onSubmit() {
    var test = new VendorValidator();
    var msg = test.validate(this.form.value);
    var check = JSON.stringify(msg) == '{}';
    if (check == true)
      this.service.EditVendor(this.form.value).subscribe(x => {
        if (x.success)
          this.dialogRef.close(x.success)
     //   else
        //  this.notifier.notify('error', x.errors["msg"][0]);
      } ,   
      //   error => {
      //   if (error.error.errors.length === undefined)
      //     this.notifier.notify('error', error.error.message);
      //   else
      //     this.notifier.notify('error', error.error);
      // }
      );
    else {
      var message='';
      for (const [key, value] of Object.entries(msg)) {
        message=message+" "+value;
      }
      this.notifier.notify('error', message);
    }

  }
}

