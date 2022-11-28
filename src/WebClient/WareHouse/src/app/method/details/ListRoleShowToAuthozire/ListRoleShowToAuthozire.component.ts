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
import { ListRole } from 'src/app/model/ListApp';
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


interface ExampleFlatNode {
  expandable: boolean;
  level: number;
  name: string
  key: string;
  inActive: boolean;
  description: string;
  isAPI: boolean;
  id: string;
}


@Component({
  selector: 'app-ListRoleShowToAuthozire',
  templateUrl: './ListRoleShowToAuthozire.component.html',
  styleUrls: ['./ListRoleShowToAuthozire.component.scss']
})
export class ListRoleShowToAuthozireComponent implements OnInit {



  @Input() item: ModelDialog | undefined;
  //
  files5: TreeNode<ListRole>[] = [];
  selectedNodes3: TreeNode<ListRole>[] = [];

  cols: any[] = [];

  ///
  selectedValue: ListApp | undefined;
  selectedCar: string = '';

  foods: ListApp[] = [];

  //
  listDelete: ListRole[] = [];
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
  list: ResultMessageResponse<ListRole> = {
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
  constructor(private authbyuser: ListAuthozireByListRoleService, private listrolebyusser: ListRoleByUserService, public ref: DynamicDialogRef, public config: DynamicDialogConfig, private messageService: MessageService, private serviceapp: ListAppService, private service: ListRoleService, private _liveAnnouncer: LiveAnnouncer, public dialog: MatDialog, notifierService: NotifierService) {

    this.notifier = notifierService;
    this.isRowSelectable = this.isRowSelectable.bind(this);

  }
  setMyPagination(event: any) {

    let startRow = event.first + 1;
    let endRow = startRow + event.rows;
    this.totalRows = 1000;
  }
  next() {
    if (!this.isLastPage())
      this.first = this.first + this.rows;
  }

  prev() {
    if (!this.isFirstPage())
      this.first = this.first - this.rows;
  }

  reset() {
    this.first = 0;
  }

  isLastPage(): boolean {
    return this.files5 ? this.first === (this.files5.length - this.rows) : true;
  }

  isFirstPage(): boolean {
    return this.files5 ? this.first === 0 : true;
  }
  selectProduct(product: any) {
    //this.messageService.add({severity:'info', summary:'Product Selected', detail: product.name});
  }

  onRowSelect(event: any) {
    // this.messageService.add({severity:'info', summary:'Product Selected', detail: event.data.name});
  }

  onRowUnselect(event: any) {
    //this.messageService.add({severity:'info', summary:'Product Unselected',  detail: event.data.name});
  }

  isRowSelectable(event: any): boolean {
    return !this.isOutOfStock(event.data);
  }

  isOutOfStock(data: any): boolean {
    return data.inventoryStatus === 'OUTOFSTOCK';
  }
  ngOnInit() {

    this.serviceapp.getList().subscribe(x => this.foods = x.data);
    this.GetData();
    // 'id', 'name', 'key', 'description', 'isAPI', 'inActive'
    this.cols = [
      { field: 'name', header: 'Name' },
      { field: 'key', header: 'Key' },
      { field: 'description', header: 'Mô tả' },
      { field: 'isAPI', header: 'API' },
      { field: 'inActive', header: 'Hoạt động' }
    ];
  }

  ngAfterViewInit() {
    // this.dataSourceRole.paginator = this.paginator;
    // this.dataSourceRole.sort = this.sort;
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

  selectEvent($event: any) {
    this.GetData();
  }

  GetData() {
    if (this.selectedValue != undefined) {
      if (this.config.data.type == 1) {
        this.listrolebyusser.getListByUserId(this.config.data.id, this.selectedValue.id).subscribe(x => {
          this.listRoleByUser = x.data;
          if (this.selectedValue != undefined)
            this.service.getListTreeTable(this.selectedValue.id).subscribe(list => {
              this.files5 = list.data;
              this.getElement(list.data);
            });
        }
        );
      }
      else if (this.config.data.type == 2)
        this.authbyuser.getListByUserId(this.config.data.id, this.selectedValue.id).subscribe(x => {
          this.listRoleByUser = x.data;
          if (this.selectedValue != undefined)
            this.service.getListTreeTable(this.selectedValue.id).subscribe(list => {
              this.files5 = list.data;
              this.getElement(list.data);
            });
        }
        );


    }

  }

  getElement(e: TreeNode<ListRole>[]) {
    let model = this.listRoleByUser;
    e.forEach(element => {
      if (element.data != undefined && element.data.id != undefined && model.find(x => element.data?.id.includes(x)))
        this.selectedNodes3.push(element);
      if (element.children != undefined && element.children.length > 0)
        this.getElement(element.children);
    });
    // if (e.isAPI)
    //   this.selectedNodes3.push(e);
    // if (e.children != undefined && e.children.length > 0)
    //   e.children.forEach(element => {
    //     this.getElement(element);

    //   });
  }


  announceSortChange(sortState: Sort) {
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }

  }
  pageChanged(event: PageEvent) {
    this.model.pages = event.pageIndex
    this.model.num = event.pageSize;
    this.GetData();
  }
  openDialog(model: ListRole): void {
    console.log(model)
    this.service.EditIndex(model.id).subscribe(x => {
      if (x.success) {
        const dialogRef = this.dialog.open(ListRoleEditComponent, {
          width: '550px',
          data: x.data,
        });

        dialogRef.afterClosed().subscribe(result => {
          var res = result;
          if (res) {
            this.notifier.notify('success', 'Phân quyền thành công !');
            this.GetData();
          }
        });
      }
    });

  }

  openDialogAuthozire() {
    var idselect = Array<string>();
    for (let index = 0; index < this.selectedNodes3.length; index++) {
      const element = this.selectedNodes3[index];
      if (element.data?.id)
        idselect.push(element.data?.id);
    }
    this.ref.close({
      idselect: idselect,
      appid: this.selectedValue?.id
    });
    // this.selectedNodes3.forEach(e => {
    //   this.messageService.add({ severity: 'info', summary: e.data?.name, detail: e.data?.key });
    // });

  }

  openDialogCreate(): void {
    console.log(this.selectedValue)
    const dialogRef = this.dialog.open(ListRoleCreateComponent, {
      width: '550px',
      data: this.selectedValue
    });

    dialogRef.afterClosed().subscribe(result => {
      var res = result;
      if (res) {
        this.notifier.notify('success', 'Thêm mới thành công !');
        this.GetData();
      }
    });
  }


}

