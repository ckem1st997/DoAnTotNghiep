import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { Guid } from 'src/app/extension/Guid';
import { VendorDTO } from 'src/app/model/VendorDTO';
import { VendorService } from 'src/app/service/VendorService.service';
import { VendorValidator } from 'src/app/validator/VendorValidator';

@Component({
  selector: 'app-VendorCreate',
  templateUrl: './VendorCreate.component.html',
  styleUrls: ['./VendorCreate.component.scss']
})
export class VendorCreateComponent implements OnInit {
  title = "Tạo mới nhà cung cấp";
  private readonly notifier!: NotifierService;
  success = false;
  form!: FormGroup;
  dt!: VendorDTO;
  options!: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<VendorCreateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: VendorDTO,
    private formBuilder: FormBuilder,
    private service: VendorService,
    notifierService: NotifierService
  ) { this.notifier = notifierService; }
  ngOnInit() {
    this.dt = this.data;
    this.form = this.formBuilder.group({
      id: Guid.newGuid(),
      code:'',
      name: '',
      address: '',
      phone: '',
      email: null,
      contactPerson: '',
      inactive: true,
    });
    this.form.patchValue(this.data);

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
      this.service.AddVendor(this.form.value).subscribe(x => {
        if (x.success)
          this.dialogRef.close(x.success)
        else {
          if (x.errors["msg"] != undefined)
            this.notifier.notify('error', x.errors["msg"][0]);
          // else
          //   this.notifier.notify('error', x.message);
        }
      } , 
      //     error => {
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

