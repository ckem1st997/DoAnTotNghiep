import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';
import { WareHouseItemCategoryDTO } from 'src/app/model/WareHouseItemCategoryDTO';
import { WareHouseItemCategoryService } from 'src/app/service/WareHouseItemCategory.service';
import { WareHouseValidator } from 'src/app/validator/WareHouseValidator';
import { WareHouseCreateComponent } from '../../create/WareHouseCreate/WareHouseCreate.component';

@Component({
  selector: 'app-WareHouseItemCategoryEditDetails',
  templateUrl: './WareHouseItemCategoryEditDetails.component.html',
  styleUrls: ['./WareHouseItemCategoryEditDetails.component.scss']
})
export class WareHouseItemCategoryEditDetailsComponent implements OnInit {
  title = "Thông tin loại vật tư";
  private readonly notifier!: NotifierService;
  success = false;
  form!: FormGroup;
  dt!: WareHouseItemCategoryDTO;
  options!: FormGroup;
  listDropDown:WareHouseItemCategoryDTO[]=[];
  constructor(
    public dialogRef: MatDialogRef<WareHouseCreateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: WareHouseItemCategoryDTO,
    private formBuilder: FormBuilder,
    private service: WareHouseItemCategoryService,
    notifierService: NotifierService
  ) { this.notifier = notifierService; }
  ngOnInit() {
    this.dt = this.data;
    this.form = this.formBuilder.group({
      id: this.dt.id,
      code: this.dt.code,
      name: this.dt.name,
      description: this.dt.description,
      parentId: this.dt.parentId,
      path: this.dt.path,
      inactive: this.dt.inactive,
    });
    this.getDropDown();
  }
  get f() { return this.form.controls; }
  onNoClick(): void {
    this.dialogRef.close(false);
  }
  getDropDown() {
    this.listDropDown=this.data.inverseParent;
  }

  onSubmit() {
    var test = new WareHouseValidator();
    var msg = test.validate(this.form.value);
    var check = JSON.stringify(msg) == '{}';
    if (check == true)
      this.service.Edit(this.form.value).subscribe(x => {
        if (x.success)
          this.dialogRef.close(x.success)
        else
          this.notifier.notify('error', x.errors["msg"][0]);
      }
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


