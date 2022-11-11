import { LiveAnnouncer } from '@angular/cdk/a11y';
import { SelectionModel } from '@angular/cdk/collections';
import { FlatTreeControl } from '@angular/cdk/tree';
import { StringMap } from '@angular/compiler/src/compiler_facade_interface';
import { Component, HostListener, Injectable, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatTreeFlattener, MatTreeFlatDataSource } from '@angular/material/tree';
import { NotifierService } from 'angular-notifier';
import { BehaviorSubject } from 'rxjs';
import { UnitCreateComponent } from 'src/app/method/create/UnitCreate/UnitCreate.component';
import { UnitDeleteComponent } from 'src/app/method/delete/UnitDelete/UnitDelete.component';
import { ListRoleShowToAuthozireComponent } from 'src/app/method/details/ListRoleShowToAuthozire/ListRoleShowToAuthozire.component';
import { UnitDetailsComponent } from 'src/app/method/details/UnitDetails/UnitDetails.component';
import { RoleEditComponent } from 'src/app/method/edit/RoleEdit/RoleEdit.component';
import { UnitEditComponent } from 'src/app/method/edit/UnitEdit/UnitEdit.component';
import { FormSearchWareHouseComponent } from 'src/app/method/search/FormSearchWareHouse/FormSearchWareHouse.component';
import { ExampleFlatNode } from 'src/app/model/ExampleFlatNode';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';
import { RoleSearchModel } from 'src/app/model/RoleSearchModel';
import { UnitDTO } from 'src/app/model/UnitDTO';
import { UserMaster } from 'src/app/model/UserMaster';
import { WareHouseSearchModel } from 'src/app/model/WareHouseSearchModel';
import { AuthozireService } from 'src/app/service/Authozire.service';
import { UnitService } from 'src/app/service/Unit.service';
import { WarehouseService } from 'src/app/service/warehouse.service';
import { ListRoleComponent } from '../ListRole/ListRole.component';
import { DialogService } from 'primeng/dynamicdialog';
import { MessageService } from 'primeng/api';
import { ListRoleByUserService } from './../../service/ListRoleByUser.service';

@Component({
  selector: 'app-RoleUser',
  templateUrl: './RoleUser.component.html',
  styleUrls: ['./RoleUser.component.scss'],

})
export class RoleUserComponent implements OnInit {

  ///
  //
  listDelete: UserMaster[] = [];
  //select
  selection = new SelectionModel<UserMaster>(true, []);
  //noti
  private readonly notifier!: NotifierService;
  //tree-view

  checkSizeWindows: boolean = true;
  public getScreenWidth: any;
  public getScreenHeight: any;
  //
  panelOpenState = false;
  checkedl = false;
  totalRows = 0;
  pageSize = 15;
  currentPage = 0;
  pageSizeOptions: number[] = [15, 50, 100];
  displayedColumns: string[] = ['select', 'id', 'userName', 'role', 'read', 'create', 'edit', 'delete', 'inActive', 'method'];
  dataSourceRole = new MatTableDataSource<UserMaster>();
  model: RoleSearchModel = {
    key: '',
    whId: '',
    num: 15,
    pages: 0
  };
  list: ResultMessageResponse<UserMaster> = {
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
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  sort!: MatSort;

  constructor(private listrolebyuser: ListRoleByUserService, private messageService: MessageService, public dialogService: DialogService, private service: AuthozireService, private _liveAnnouncer: LiveAnnouncer, public dialog: MatDialog, notifierService: NotifierService, private serviceW: WarehouseService) {

    this.notifier = notifierService;

  }
  ngOnInit() {
    this.GetData();
    this.getScreenWidth = window.innerWidth;
    this.getScreenHeight = window.innerHeight;
    this.selection.clear();
  }

  ngAfterViewInit() {
    this.dataSourceRole.paginator = this.paginator;
    this.dataSourceRole.sort = this.sort;
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
    this.service.getList(this.model.key, this.model.whId, this.model.pages, this.model.num).subscribe(list => {
      this.dataSourceRole.data = list.data; setTimeout(() => {
        this.paginator.pageIndex = this.currentPage;
        this.paginator.length = list.totalCount;
      });
    });
    this.listDelete = [];
    this.selection.clear();
  }
  announceSortChange(sortState: Sort) {
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }

  }
  pageChanged(event: PageEvent) {
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex;
    this.model.pages = event.pageIndex
    this.model.num = event.pageSize;
    this.GetData();
  }
  searchQuery() {
    var val = document.getElementById("searchInput") as HTMLInputElement;
    this.model.key = val.value;
    //   this.model.active = this.checkedl;
    this.GetData();
  }
  openDialog(model: UserMaster): void {
    this.service.EditIndex(model.id).subscribe(x => {
      if (x.success) {
        const dialogRef = this.dialog.open(RoleEditComponent, {
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
      //  else
      // this.notifier.notify('warn', x.message);


    });

  }

  openDialogByAuthozire(model: UserMaster): void {
    this.service.EditIndex(model.id).subscribe(x => {
      if (x.success) {
        const dialogRef = this.dialogService.open(ListRoleShowToAuthozireComponent, {
          width: '90%',
          data: {
            id: model.id,
            type: 1
          },
        });
        dialogRef.onClose.subscribe((result) => {
          console.log(result);
          this.listrolebyuser.EditOrCreate(result, model.id).subscribe(x => {
            if (x.success)
              this.messageService.add({ severity: 'success', summary: 'Thông báo', detail: 'Phân quyền thành công !' });
            else
              this.messageService.add({ severity: 'error', summary: 'Thông báo', detail: 'Phân quyền thất bại !' });

          })
          // if (result) {
          //  }
        });
        // dialogRef.afterClosed().subscribe(result => {
        //   var res = result;
        //   if (res) {
        //     this.notifier.notify('success', 'Phân quyền thành công !');
        //     this.GetData();
        //   }
        // });
      }
      //  else
      // this.notifier.notify('warn', x.message);


    });

  }

  openDialogDetals(model: UnitDTO): void {
    var val = document.getElementById("searchInput") as HTMLInputElement;

    const dialogRef = this.dialog.open(UnitDetailsComponent, {
      width: '550px',
      data: model,
    });
  }
  openDialogCreate(): void {
    var val = document.getElementById("searchInput") as HTMLInputElement;

    const dialogRef = this.dialog.open(UnitCreateComponent, {
      width: '550px'
    });

    dialogRef.afterClosed().subscribe(result => {
      var res = result;
      if (res) {
        this.notifier.notify('success', 'Thêm mới thành công !');
        this.GetData();
      }
    });
  }

  openDialogDelelte(model: UnitDTO): void {
    // this.listDelete.push(model);
    const dialogRef = this.dialog.open(UnitDeleteComponent, {
      width: '550px',
      data: this.listDelete
    });

    dialogRef.afterClosed().subscribe(result => {
      var res = result;
      if (res) {
        this.notifier.notify('success', 'Xoá thành công !');
        this.GetData();
      }
    });
  }
  openDialogDeleteAll(): void {
    var model = this.selection.selected;
    if (model.length > 0) {
      this.listDelete = model;
      const dialogRef = this.dialog.open(UnitDeleteComponent, {
        width: '550px',
        data: this.listDelete,
      });

      dialogRef.afterClosed().subscribe(result => {
        var res = result;
        if (res) {
          this.notifier.notify('success', 'Xoá thành công !');
          this.GetData();
        }
      });
    }
    else
      this.notifier.notify('warning', "Bạn chưa chọn đơn vị tính nào !");

  }
  //searchQueryDialog
  searchQueryDialog(): void {
    const dialogRef = this.dialog.open(FormSearchWareHouseComponent, {
      width: '550px',
      height: '350px',
    });

    dialogRef.afterClosed().subscribe(result => {
      var res = result;
      if (result) {
        this.model.key = res.key;
        this.GetData();
      }
    });
  }
  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSourceRole.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }

    this.selection.select(...this.dataSourceRole.data);
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: UserMaster): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.id}`;
  }
}

