import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { VendorDTO } from 'src/app/model/VendorDTO';
import { VendorService } from 'src/app/service/VendorService.service';
import { VendorValidator } from 'src/app/validator/VendorValidator';
@Component({
  selector: 'app-VendorDetails',
  templateUrl: './VendorDetails.component.html',
  styleUrls: ['./VendorDetails.component.scss']
})
export class VendorDetailsComponent implements OnInit {
  title="Thông tin chi tiết nhà cung cấp !";
  private readonly notifier!: NotifierService;
  success = false;
  form!: FormGroup;
  dt!: VendorDTO;
  options!: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<VendorDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: VendorDTO,
    private formBuilder: FormBuilder,
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
}

