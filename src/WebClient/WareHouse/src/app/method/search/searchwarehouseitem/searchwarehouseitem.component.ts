import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { VendorService } from 'src/app/service/VendorService.service';
import { WareHouseItemDTO } from 'src/app/model/WareHouseItemDTO';

@Component({
  selector: 'app-searchwarehouseitem',
  templateUrl: './searchwarehouseitem.component.html',
  styleUrls: ['./searchwarehouseitem.component.scss']
})
export class SearchwarehouseitemComponent implements OnInit {

  private readonly notifier!: NotifierService;
  form!: FormGroup;
  dt!: WareHouseItemDTO;
  constructor(
    public dialogRef: MatDialogRef<SearchwarehouseitemComponent>,
    private formBuilder: FormBuilder,
    notifierService: NotifierService,
    @Inject(MAT_DIALOG_DATA) public data: WareHouseItemDTO,

  ) { this.notifier = notifierService; }
  ngOnInit() {
    this.dt=this.data;
    this.form = this.formBuilder.group({
      key: '',
      idtype: null
    });
  }
  onNoClick(): void {
    this.dialogRef.close(false);
  }
  onSubmit() {
    console.log(this.form.value);

    this.dialogRef.close(this.form.value);
  }
}

