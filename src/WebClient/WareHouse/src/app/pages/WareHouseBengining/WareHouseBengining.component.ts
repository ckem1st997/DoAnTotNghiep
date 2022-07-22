import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Unit } from 'src/app/entity/Unit';
import { UnitValidator } from 'src/app/validator/UnitValidator';
import { FlatTreeControl } from '@angular/cdk/tree';
import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree';
import { TreeView } from 'src/app/model/TreeView';
import { WarehouseService } from 'src/app/service/warehouse.service';
import { BeginningWareHouseDTO } from 'src/app/model/BeginningWareHouseDTO';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { SelectionModel } from '@angular/cdk/collections';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { NotifierService } from 'angular-notifier';
import { WareHouseCreateComponent } from 'src/app/method/create/WareHouseCreate/WareHouseCreate.component';
import { WareHouseDeleteComponent } from 'src/app/method/delete/WareHouseDelete/WareHouseDelete.component';
import { WareHouseDetailsComponent } from 'src/app/method/details/WareHouseDetails/WareHouseDetails.component';
import { WareHouseEditComponent } from 'src/app/method/edit/WareHouseEdit/WareHouseEdit.component';
import { FormSearchWareHouseComponent } from 'src/app/method/search/FormSearchWareHouse/FormSearchWareHouse.component';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';
import { WareHouseDTO } from 'src/app/model/WareHouseDTO';
import { WareHouseSearchModel } from 'src/app/model/WareHouseSearchModel';
import { BeginningWareHouse } from 'src/app/entity/BeginningWareHouse';
import { BeginningWareHouseService } from 'src/app/service/BeginningWareHouse.service';
import { WareHouseBenginingEditComponent } from 'src/app/method/edit/WareHouseBenginingEdit/WareHouseBenginingEdit.component';
import { WareHouseBenginingCreateComponent } from 'src/app/method/create/WareHouseBenginingCreate/WareHouseBenginingCreate.component';
import { WareHouseBenginingCreateDeleteComponent } from 'src/app/method/delete/WareHouseBenginingCreateDelete/WareHouseBenginingCreateDelete.component';
import { BeginningWareHouseSearchModel } from './../../model/BeginningWareHouseSearchModel';

interface ExampleFlatNode {
  expandable: boolean;
  name: string;
  level: number;
  key: string;
}
@Component({
  selector: 'app-WareHouseBengining',
  templateUrl: './WareHouseBengining.component.html',
  styleUrls: ['./WareHouseBengining.component.scss']
})
export class WareHouseBenginingComponent implements OnInit {
  //
  //
  listDelete: BeginningWareHouseDTO[] = [];
  //select
  selection = new SelectionModel<BeginningWareHouseDTO>(true, []);
  //noti
  private readonly notifier!: NotifierService;
  //tree-view
  modelCreate: BeginningWareHouseDTO[] = [];
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
  displayedColumns: string[] = ['select', 'id', 'wareHouseName', 'itemName', 'unitName', 'quantity', 'method'];
  dataSource = new MatTableDataSource<BeginningWareHouseDTO>();
  model: BeginningWareHouseSearchModel = {
    active: null,
    keySearch: '',
    skip: this.currentPage * this.pageSize,
    take: this.pageSize,
    wareHouseId: null
  };
  list: ResultMessageResponse<BeginningWareHouseDTO> = {
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
  message: object | undefined;
  //tree-view
  private _transformer = (node: TreeView, level: number) => {
    return {
      expandable: !!node.children && node.children.length > 0,
      name: node.name,
      level: level,
      key: node.key
    };
  };
  hasChild = (_: number, node: ExampleFlatNode) => node.expandable;
  treeControl = new FlatTreeControl<ExampleFlatNode>(
    node => node.level,
    node => node.expandable,
  );

  treeFlattener = new MatTreeFlattener(
    this._transformer,
    node => node.level,
    node => node.expandable,
    node => node.children,
  );

  dataSourceTreee = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  constructor(private service: BeginningWareHouseService, private serviceW: WarehouseService, private _liveAnnouncer: LiveAnnouncer, public dialog: MatDialog, notifierService: NotifierService) {
    this.notifier = notifierService;
  }



  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  ngOnInit() {
    this.GetData();
    this.getScreenWidth = window.innerWidth;
    this.getScreenHeight = window.innerHeight;
    this.selection.clear();
    this.serviceW.getTreeView().subscribe(x => this.dataSourceTreee.data = x.data);

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
    this.service.getList(this.model).subscribe(list => {
      this.dataSource.data = list.data; setTimeout(() => {
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
    this.model.skip = event.pageIndex * event.pageSize;
    this.model.take = event.pageSize;
    this.GetData();
  }
  searchQuery() {
    var val = document.getElementById("searchInput") as HTMLInputElement;
    this.model.keySearch = val.value;
    this.model.active = this.checkedl;
    this.GetData();
  }
  openDialog(model: BeginningWareHouseDTO): void {
    this.service.EditIndex(model.id).subscribe(x => {

      this.modelCreate = x.data;
      const dialogRef = this.dialog.open(WareHouseBenginingEditComponent, {
        width: '550px',
        data: this.modelCreate
      });

      dialogRef.afterClosed().subscribe(result => {
        var res = result;
        if (res) {
          this.notifier.notify('success', 'Chỉnh sửa thành công !');
          this.GetData();
        }
      });

    });
  }
  openDialogDetals(model: BeginningWareHouseDTO): void {
    var val = document.getElementById("searchInput") as HTMLInputElement;

    const dialogRef = this.dialog.open(WareHouseDetailsComponent, {
      width: '550px',
      data: model,
    });
  }
  openDialogCreate(): void {
    var idCheck: string | null = null;
    var selectDelete = document.querySelectorAll("#treeview button");
    selectDelete.forEach(element => {
      if (element.className.includes("activeButtonTreeView"))
        idCheck = element.getAttribute("id");
    });

    if (idCheck !== null) {
      this.service.AddIndex(idCheck).subscribe(x => {
        this.modelCreate = x.data;
        const dialogRef = this.dialog.open(WareHouseBenginingCreateComponent, {
          width: '550px',
          data: this.modelCreate
        });

        dialogRef.afterClosed().subscribe(result => {
          var res = result;
          if (res) {
            this.notifier.notify('success', 'Thêm thành công !');
            this.GetData();
          }
        });
      });
    }
    else
      this.notifier.notify('warning', "Bạn chưa chọn kho nào !");
  }

  openDialogDelelte(model: BeginningWareHouseDTO): void {
    this.listDelete.push(model);
    const dialogRef = this.dialog.open(WareHouseBenginingCreateDeleteComponent, {
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
      const dialogRef = this.dialog.open(WareHouseBenginingCreateDeleteComponent, {
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
      this.notifier.notify('warning', "Bạn chưa chọn kho nào !");

  }
  //searchQueryDialog
  searchQueryDialog(): void {
    const dialogRef = this.dialog.open(FormSearchWareHouseComponent, {
      width: '550px',
      height: '350px',
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        var res = result;
        this.model.keySearch = res.key;
        this.model.active = res.inactive;
        this.GetData();
      }
    });
  }
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }

    this.selection.select(...this.dataSource.data);
  }

  GetActive(e: any) {
    var select = document.getElementById(e.key) as HTMLButtonElement;
    var selectDelete = document.querySelectorAll("#treeview button");
    selectDelete.forEach(element => {
      element.className = element.className.replace("activeButtonTreeView", " ");
    });
    select.className += " activeButtonTreeView";
    this.model.wareHouseId = e.key;
    this.GetData();
  }

  GetAll() {
    var selectDelete = document.querySelectorAll("#treeview button");
    selectDelete.forEach(element => {
      element.className = element.className.replace("activeButtonTreeView", " ");
    });
    this.model.wareHouseId = null;
    this.GetData();
  }
  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: BeginningWareHouseDTO): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.id}`;
  }
}

