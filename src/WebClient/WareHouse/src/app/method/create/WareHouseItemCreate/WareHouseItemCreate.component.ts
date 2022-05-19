import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { NotifierService } from 'angular-notifier';
import { Observable } from 'rxjs';
import { Guid } from 'src/app/extension/Guid';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';
import { VendorDTO } from 'src/app/model/VendorDTO';
import { WareHouseDTO } from 'src/app/model/WareHouseDTO';
import { WareHouseItemDTO } from 'src/app/model/WareHouseItemDTO';
import { VendorService } from 'src/app/service/VendorService.service';
import { WarehouseService } from 'src/app/service/warehouse.service';
import { WareHouseItemService } from 'src/app/service/WareHouseItem.service';
import { VendorValidator } from 'src/app/validator/VendorValidator';
import { WareHouseItemValidator } from 'src/app/validator/WareHouseItemValidator';

@Component({
  selector: 'app-WareHouseItemCreate',
  templateUrl: './WareHouseItemCreate.component.html',
  styleUrls: ['./WareHouseItemCreate.component.scss']
})
export class WareHouseItemCreateComponent implements OnInit {
  title = "Thêm vật tư";
  isDataLoaded: boolean = true;
  private readonly notifier!: NotifierService;
  success = false;
  form!: FormGroup;
  dt!: WareHouseItemDTO;
  options!: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<WareHouseItemCreateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: WareHouseItemDTO,
    private formBuilder: FormBuilder,
    private service: WareHouseItemService,
    notifierService: NotifierService
  ) {
    this.notifier = notifierService;
  }
  ngOnInit(): void {
    this.dt=this.data;
    this.form = this.formBuilder.group({
      id: Guid.newGuid(),
      code: '',
      name: '',
      categoryId: null,
      vendorId: null,
      country: null,
      unitId: null,
      description: null,
      parentId: null,
      path: null,
      inactive: true,
    });
    this.form.patchValue(this.data);

  }
  get f() { return this.form.controls; }
  onNoClick(): void {
    this.dialogRef.close(false);
  }

  onSubmit() {
    var test = new WareHouseItemValidator();
    var msg = test.validate(this.form.value);
    var check = JSON.stringify(msg) == '{}';
    if (check == true)
      this.service.Add(this.form.value).subscribe(x => {
        if (x.success)
          this.dialogRef.close(x.success)
        // else {
        //   if (x.errors["msg"] != undefined)
        //     this.notifier.notify('error', x.errors["msg"][0]);
        //   else
        //     this.notifier.notify('error', x.message);
        // }
      } ,    
      //  error => {
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

