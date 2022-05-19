import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { UnitDTO } from 'src/app/model/UnitDTO';
import { UserMaster } from 'src/app/model/UserMaster';
import { AuthozireService } from 'src/app/service/Authozire.service';
import { UnitService } from 'src/app/service/Unit.service';
import { UnitValidator } from 'src/app/validator/UnitValidator';
import { UnitCreateComponent } from '../../create/UnitCreate/UnitCreate.component';
import { SelectWareHouseComponent } from '../selectWareHouse/selectWareHouse.component';

@Component({
  selector: 'app-RoleEdit',
  templateUrl: './RoleEdit.component.html',
  styleUrls: ['./RoleEdit.component.scss']
})
export class RoleEditComponent implements OnInit {


  title = "Phân quyền";
  private readonly notifier!: NotifierService;
  success = false;
  form!: FormGroup;
  dt!: UserMaster;
  options!: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<RoleEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UserMaster,
    private formBuilder: FormBuilder,
    private service: AuthozireService,
    notifierService: NotifierService,
    public dialog: MatDialog
  ) { this.notifier = notifierService; }
  ngOnInit() {
    this.dt = this.data;
    this.form = this.formBuilder.group({
      id: this.data.id,
      userName: this.data.userName,
      password: '',
      inActive: this.data.inActive,
      role: this.data.role,
      roleNumber: this.data.roleNumber.toString(),
      read: this.data.read,
      create: this.data.create,
      edit: this.data.edit,
      delete: this.data.delete,
      warehouseId: this.data.warehouseId,
      listWarehouseId: '',
    });
  }
  get f() { return this.form.controls; }
  onNoClick(): void {
    this.dialogRef.close(false);
  }


  SelectWH() {

    const dialogRef = this.dialog.open(SelectWareHouseComponent, {
      width: '550px',
      height: '550px',
      data: this.dt
    });

    dialogRef.afterClosed().subscribe(result => {
      var res = result;
      if (res) {
        //  this.notifier.notify('success', 'Chỉnh sửa thành công !');
        this.form.value.listWarehouseId = res;
        this.form.value.warehouseId = res;
      }
    });


  }
  changItem(e: any) {
    var idSelect = e.target.value.split(" ")[1];
    this.form.value.roleNumber = idSelect;
    this.form.value.role = this.dt.roleSelect.find(x => x.value == idSelect)?.text;
  }
  onSubmit() {
    var y: number = +this.form.value.roleNumber;
    this.form.value.roleNumber = y;
    this.service.Edit(this.form.value).subscribe(x => {
      if (x.success)
        this.dialogRef.close(x.success)
      // else
      //   this.notifier.notify('error', x.errors["msg"][0]);
    },
      //  error => {
      //   if (error.error.errors.length === undefined)
      //     this.notifier.notify('error', error.error.message);
      //   else
      //     this.notifier.notify('error', error.error);
      // }
    );
    // else {
    //   var message = '';
    //   for (const [key, value] of Object.entries(msg)) {
    //     message = message + " " + value;
    //   }
    //   this.notifier.notify('error', message);
    // }

  }
}
