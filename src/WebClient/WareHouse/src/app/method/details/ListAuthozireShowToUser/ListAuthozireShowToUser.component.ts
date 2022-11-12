import { Component, Input, OnInit } from '@angular/core';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { SelectionModel } from '@angular/cdk/collections';
import { FlatTreeControl } from '@angular/cdk/tree';
import { StringMap } from '@angular/compiler/src/compiler_facade_interface';
import { HostListener, Injectable, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatTreeFlattener, MatTreeFlatDataSource } from '@angular/material/tree';
import { NotifierService } from 'angular-notifier';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';
import { RoleSearchModel } from 'src/app/model/RoleSearchModel';
import { ListRoleCreateComponent } from 'src/app/method/create/ListRoleCreate/ListRoleCreate.component';
import { ListRoleEditComponent } from 'src/app/method/edit/ListRoleEdit/ListRoleEdit.component';
import { ListAuthozire, ListRole } from 'src/app/model/ListApp';
import { ListApp } from 'src/app/model/ListApp';
import { TreeView } from 'src/app/model/TreeView';
import { ListAppService } from 'src/app/service/ListApp.service';
import { ListRoleService } from 'src/app/service/ListRole.service';
import { MessageService, TreeNode } from 'primeng/api';
import { ModelDialog } from 'src/app/model/ModelDialog';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ListRoleByUserService } from './../../../service/ListRoleByUser.service';
import { ListAuthozireRoleByUserService } from 'src/app/service/ListAuthozireRoleByUser.service';
import { ListAuthozireByListRoleService } from 'src/app/service/ListAuthozireByListRole.service';
import { ListAuthozireService } from 'src/app/service/ListAuthozire.service';

@Component({
  selector: 'app-ListAuthozireShowToUser',
  templateUrl: './ListAuthozireShowToUser.component.html',
  styleUrls: ['./ListAuthozireShowToUser.component.scss']
})
export class ListAuthozireShowToUserComponent implements OnInit {




  @Input() item: ModelDialog | undefined;
  //
  files5: ListAuthozire[] = [];
  selectedNodes3: ListAuthozire[] = [];

  cols: any[] = [];

  ///
  selectedCar: string = '';

  foods: ListApp[] = [];

  //
  listDelete: ListAuthozire[] = [];
  //select
  //noti
  private readonly notifier!: NotifierService;
  //tree-view

  checkSizeWindows: boolean = true;
  public getScreenWidth: any;
  public getScreenHeight: any;
  //
  panelOpenState = false;
  checkedl = false;
  //
  listRoleByUser: Array<string> = [];
  // dataSourceRole = new MatTableDataSource<ListRole>();
  model: RoleSearchModel = {
    key: '',
    whId: '',
    num: 15,
    pages: 0
  };
  list: ResultMessageResponse<ListAuthozire> = {
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
  totalRows = 100;
  first = 0;

  rows = 10;
  constructor(private authbyuser: ListAuthozireByListRoleService, private listrolebyusser: ListAuthozireRoleByUserService, public ref: DynamicDialogRef, public config: DynamicDialogConfig, private messageService: MessageService, private serviceapp: ListAppService, private service: ListAuthozireService, private _liveAnnouncer: LiveAnnouncer, public dialog: MatDialog, notifierService: NotifierService) {

    this.notifier = notifierService;
    this.isRowSelectable = this.isRowSelectable.bind(this);

  }

  isRowSelectable(event: any): boolean {
    return !this.isOutOfStock(event.data);
  }

  isOutOfStock(data: any): boolean {
    return data.inventoryStatus === 'OUTOFSTOCK';
  }
  ngOnInit() {

    this.GetData();
    this.cols = [
      { field: 'name', header: 'Name' },
      { field: 'description', header: 'Mô tả' },
      { field: 'isAPI', header: 'API' },
      { field: 'inActive', header: 'Hoạt động' }
    ];
  }

  @HostListener('window:resize', ['$event'])

  onWindowResize() {
    this.getScreenWidth = window.innerWidth;
    this.getScreenHeight = window.innerHeight;
    if (this.getScreenWidth <= 800)
      this.checkSizeWindows = false;
    else
      this.checkSizeWindows = true;
  }

  GetData() {
    if (this.config.data.id != undefined) {
      this.listrolebyusser.getList(this.config.data.id).subscribe(x => this.listRoleByUser = x.data);
      this.service.getList().subscribe(list => {
        this.files5 = list.data;
        this.getElement(list.data);
      });
    }
  }

  getElement(e: ListAuthozire[]) {
    let model = this.listRoleByUser;
    console.log(model);
    e.forEach(element => {
      if (element != undefined && element.id != undefined && model.find(x => element?.id.includes(x)))
        this.selectedNodes3.push(element);
    });
  }

  openDialogAuthozire() {
    var idselect = Array<string>();
    for (let index = 0; index < this.selectedNodes3.length; index++) {
      const element = this.selectedNodes3[index];
      if (element?.id)
        idselect.push(element?.id);
    }
    this.ref.close({
      idselect: idselect
    });
  }
}

