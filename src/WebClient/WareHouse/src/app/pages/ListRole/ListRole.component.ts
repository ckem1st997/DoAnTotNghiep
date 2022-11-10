import { Component, OnInit } from '@angular/core';
import { FormSearchWareHouseComponent } from './../../method/search/FormSearchWareHouse/FormSearchWareHouse.component';
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
import { BehaviorSubject } from 'rxjs';
import { UnitCreateComponent } from 'src/app/method/create/UnitCreate/UnitCreate.component';
import { UnitDeleteComponent } from 'src/app/method/delete/UnitDelete/UnitDelete.component';
import { UnitDetailsComponent } from 'src/app/method/details/UnitDetails/UnitDetails.component';
import { RoleEditComponent } from 'src/app/method/edit/RoleEdit/RoleEdit.component';
import { UnitEditComponent } from 'src/app/method/edit/UnitEdit/UnitEdit.component';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';
import { RoleSearchModel } from 'src/app/model/RoleSearchModel';
import { UnitDTO } from 'src/app/model/UnitDTO';
import { UserMaster } from 'src/app/model/UserMaster';
import { WareHouseSearchModel } from 'src/app/model/WareHouseSearchModel';
import { AuthozireService } from 'src/app/service/Authozire.service';
import { UnitService } from 'src/app/service/Unit.service';
import { WarehouseService } from 'src/app/service/warehouse.service';
import { ListRoleService } from './../../service/ListRole.service';
import { ListRoleCreateComponent } from 'src/app/method/create/ListRoleCreate/ListRoleCreate.component';
import { ListRoleEditComponent } from 'src/app/method/edit/ListRoleEdit/ListRoleEdit.component';
import { ListRole } from 'src/app/model/ListApp';
import { ListApp } from 'src/app/model/ListApp';
import { TreeView } from 'src/app/model/TreeView';
import { ListAppService } from 'src/app/service/ListApp.service';
import { TreeNode } from 'primeng/api';


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
  selector: 'app-ListRole',
  templateUrl: './ListRole.component.html',
  styleUrls: ['./ListRole.component.scss']
})
export class ListRoleComponent implements OnInit {
  files5: TreeNode[] = [];
  selectedNodes3: TreeNode[] = [];

  cols: any[] = [];
  ///
  selectedValue: string = '';
  selectedCar: string = '';

  foods: ListApp[] = [];

  //
  listDelete: ListRole[] = [];
  //select
  selection = new SelectionModel<ListRole>(true, []);
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
  displayedColumns: string[] = ['select', 'id', 'name', 'key', 'description', 'isAPI', 'inActive', 'method'];
  //

  private _transformer = (node: ListRole, level: number) => {
    return {
      expandable: !!node.children && node.children.length > 0,
      name: node.name,
      level: level,
      key: node.key,
      inActive: node.inActive,
      description: node.description,
      isAPI: node.isAPI,
      id: node.id
    };
  };
  hasChild = (_: number, node: ExampleFlatNode) => node.expandable;
  treeControl = new FlatTreeControl<ExampleFlatNode>(
    node => node.level,
    node => node.expandable
  );

  treeFlattener = new MatTreeFlattener(
    this._transformer,
    node => node.level,
    node => node.expandable,
    node => node.children
  );

  dataSourceRole = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);
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
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  sort!: MatSort;

  constructor(private serviceapp: ListAppService, private service: ListRoleService, private _liveAnnouncer: LiveAnnouncer, public dialog: MatDialog, notifierService: NotifierService) {

    this.notifier = notifierService;

  }
  ngOnInit() {
    this.serviceapp.getList().subscribe(x => this.foods = x.data);
    this.GetData();
    this.cols = [
      { field: 'name', header: 'Name' },
      { field: 'key', header: 'Key' },
      { field: 'description', header: 'Mô tả' },
      { field: 'isAPI', header: 'API' },
      { field: 'inActive', header: 'Hoạt động' }
    ];

    // this.getScreenWidth = window.innerWidth;
    // this.getScreenHeight = window.innerHeight;
    // this.selection.clear();
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

  selectEvent($event:any) {
    console.log($event)
    this.GetData();
  }

  GetData() {
    if (this.selectedValue != undefined && this.selectedValue.length > 0)
    this.service.getListTreeTable(this.selectedValue).subscribe(list => {
      this.files5 = list.data;
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
  openDialog(model: ListRole): void {
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

  openDialogCreate(): void {
    console.log(this.selectedValue)
    const dialogRef = this.dialog.open(ListRoleCreateComponent, {
      width: '550px',
      data:this.selectedValue
    });

    dialogRef.afterClosed().subscribe(result => {
      var res = result;
      if (res) {
        this.notifier.notify('success', 'Thêm mới thành công !');
        this.GetData();
      }
    });
  }

  openDialogDeleteAll(): void {
    var model = this.selection.selected;
    console.log(model)
    if (model.length > 0) {
      var ids = Array<string>();
      for (let index = 0; index < model.length; index++) {
        const element = model[index];
        ids.push(element.id);
      }
      if (model !== undefined && model.length > 0) {
        this.service.Delete(ids).subscribe(x => {
          if (x.success) {
            this.notifier.notify('success', 'Xóa thành công !');
            this.GetData();
          }
        },
        );
      }
    }
    else
      this.notifier.notify('warning', "Bạn chưa chọn đơn vị tính nào !");

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
  checkboxLabel(row?: ListRole): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.id}`;
  }
}

