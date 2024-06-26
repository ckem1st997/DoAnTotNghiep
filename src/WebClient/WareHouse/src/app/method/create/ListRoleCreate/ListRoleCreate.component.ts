import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NotifierService } from 'angular-notifier';
import { map, Observable, startWith } from 'rxjs';
import { Guid } from 'src/app/extension/Guid';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';
import { UnitDTO } from 'src/app/model/UnitDTO';
import { ListRoleService } from 'src/app/service/ListRole.service';
import { UnitService } from 'src/app/service/Unit.service';
import { UnitValidator } from 'src/app/validator/UnitValidator';
import { ListApp, ListRole } from './../../../model/ListApp';
import { ListAppService } from './../../../service/ListApp.service';

@Component({
  selector: 'app-ListRoleCreate',
  templateUrl: './ListRoleCreate.component.html',
  styleUrls: ['./ListRoleCreate.component.scss']
})
export class ListRoleCreateComponent implements OnInit {

  title = "Thêm mới";
  private readonly notifier!: NotifierService;
  success = false;
  form!: FormGroup;
  dt!: ListRole;
  options!: FormGroup;
  listDropDown: ResultMessageResponse<ListRole> = {
    success: false,
    code: '',
    httpStatusCode: 0,
    title: '',
    message: '',
    data: [],
    totalCount: 0,
    isRedirect: false,
    redirectUrl: '',
    errors: {}
  }
  optionsc: string[] = [];
  filteredOptions!: Observable<string[]>;
  constructor(
    public dialogRef: MatDialogRef<ListRoleCreateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string,
    private formBuilder: FormBuilder,
    private service: ListRoleService,
    notifierService: NotifierService
  ) { this.notifier = notifierService; }
  ngOnInit() {
    this.form = this.formBuilder.group({
      id: Guid.newGuid(),
      name: '',
      inActive: true,
      description: '',
      parentId: '',
      softShow: 1,
      isAPI: true,
      key: '',
      appId: ''
    });
    this.service.getListTree(this.data).subscribe(x => this.listDropDown.data = x.data);
    this.service.getListKey().subscribe(x => this.optionsc = x.data);
    this.filteredOptions = this.form.controls['key'].valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value || '')),
    );
  }
  private _filter(value: any): string[] {
    const filterValue = value.toLowerCase();

    return this.optionsc.filter(option => option.toLowerCase().includes(filterValue));
  }
  get f() { return this.form.controls; }
  onNoClick(): void {
    this.dialogRef.close(false);
  }

  onSubmit() {
    this.form.value['appId'] = this.data;
    this.service.Create(this.form.value).subscribe(x => {
      if (x.success)
        this.dialogRef.close(x.success)
    });

  }
}


