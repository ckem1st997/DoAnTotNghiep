import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FlatTreeControl } from '@angular/cdk/tree';
import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree';
import { VendorService } from 'src/app/service/VendorService.service';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';
import { VendorDTO } from 'src/app/model/VendorDTO';
import { VendorSearchModel } from 'src/app/model/VendorSearchModel';
import { TreeView } from 'src/app/model/TreeView';
import { ExampleFlatNode } from 'src/app/model/ExampleFlatNode';
import { WareHouseBenginingComponent } from '../WareHouseBengining/WareHouseBengining.component';
import { VendorEditComponent } from 'src/app/method/edit/VendorEdit/VendorEdit.component';
import { NotifierService } from 'angular-notifier';
import { VendorDetailsComponent } from 'src/app/method/details/VendorDetails/VendorDetails.component';
import { VendorCreateComponent } from 'src/app/method/create/VendorCreate/VendorCreate.component';
import { Guid } from 'src/app/extension/Guid';
import { VendorDeleteComponent } from 'src/app/method/delete/VendorDelete/VendorDelete.component';
import { SelectionModel } from '@angular/cdk/collections';
import { FormsearchComponent } from 'src/app/method/search/formsearch/formsearch.component';
import { WareHouseDTO } from 'src/app/model/WareHouseDTO';
import { WarehouseService } from 'src/app/service/warehouse.service';
import { WareHouseCreateComponent } from 'src/app/method/create/WareHouseCreate/WareHouseCreate.component';
import { WareHouseEditComponent } from 'src/app/method/edit/WareHouseEdit/WareHouseEdit.component';
import { WareHouse } from 'src/app/entity/WareHouse';
import { WareHouseDetailsComponent } from 'src/app/method/details/WareHouseDetails/WareHouseDetails.component';
import { WareHouseDeleteComponent } from 'src/app/method/delete/WareHouseDelete/WareHouseDelete.component';
import { FormSearchWareHouseComponent } from 'src/app/method/search/FormSearchWareHouse/FormSearchWareHouse.component';
import { WareHouseSearchModel } from 'src/app/model/WareHouseSearchModel';

@Component({
  selector: 'app-WareHouse',
  templateUrl: './WareHouse.component.html',
  styleUrls: ['./WareHouse.component.scss']
})
export class WareHouseComponent implements OnInit {
  //
  listDelete: WareHouseDTO[] = [];
  //select
  selection = new SelectionModel<WareHouseDTO>(true, []);
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
  pageSize = 5;
  currentPage = 0;
  pageSizeOptions: number[] = [15, 50, 100];
  displayedColumns: string[] = ['select', 'id', 'name', 'code', 'address', 'description', 'parentId', 'inactive', 'method'];
  dataSource = new MatTableDataSource<WareHouseDTO>();
  model: WareHouseSearchModel = {
    active: null,
    keySearch: '',
    skip: this.currentPage * this.pageSize,
    take: this.pageSize
  };
  list: ResultMessageResponse<WareHouseDTO> = {
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
  constructor(private service: WarehouseService, private _liveAnnouncer: LiveAnnouncer, public dialog: MatDialog, notifierService: NotifierService) {
    this.notifier = notifierService;
  }
  hasChild = (_: number, node: ExampleFlatNode) => node.expandable;
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  ngOnInit() {
    this.GetData();
    this.getScreenWidth = window.innerWidth;
    this.getScreenHeight = window.innerHeight;
    this.selection.clear();

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
   const data= this.service.getListN(this.model);
   console.log(data)
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
  openDialog(model: WareHouseDTO): void {

    this.service.EditIndex(model.id).subscribe(x => {
      const dialogRef = this.dialog.open(WareHouseEditComponent, {
        width: '550px',
        data: x.data,
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
  openDialogDetals(model: WareHouseDTO): void {
    this.service.Details(model.id).subscribe(x => {
      const dialogRef = this.dialog.open(WareHouseDetailsComponent, {
        width: '550px',
        data: x.data,
      });

    });

  }
  openDialogCreate(): void {
    this.service.AddIndex().subscribe(x => {

      const dialogRef = this.dialog.open(WareHouseCreateComponent, {
        width: '550px',
        data: x.data,
      });

      dialogRef.afterClosed().subscribe(result => {
        var res = result;
        if (res) {
          this.notifier.notify('success', 'Thêm mới thành công !');
          this.GetData();
        }
      });

    });

  }

  openDialogDelelte(model: WareHouseDTO): void {
    this.listDelete.push(model);
    const dialogRef = this.dialog.open(WareHouseDeleteComponent, {
      width: '550px',
      data: this.listDelete
    });

    dialogRef.afterClosed().subscribe(result => {
      var res = result;
      if (res) {
        //
        var ids = Array<string>();
        // for (let index = 0; index < this.listDelete.length; index++) {
        //   const element = this.listDelete[index];
        //   ids.push(element.id);
        // }
        if (model.id !== undefined) {
          ids.push(model.id);
          this.service.Delete(ids).subscribe(x => {
            if (x.success) {
              this.notifier.notify('success', 'Xoá thành công !');
              this.GetData();
            }
          } ,

          );
        }
        else this.notifier.notify('error', 'Có lỗi xảy ra, xin vui lòng thử lại !');
      }


    });
  }
  openDialogDeleteAll(): void {
    var model = this.selection.selected;
    if (model.length > 0) {
      this.listDelete = model;
      const dialogRef = this.dialog.open(WareHouseDeleteComponent, {
        width: '550px',
        data: this.listDelete,
      });

      dialogRef.afterClosed().subscribe(result => {
        var res = result;
        if (res) {
          //
          var ids = Array<string>();
          for (let index = 0; index < this.listDelete.length; index++) {
            const element = this.listDelete[index];
            ids.push(element.id);
          }
          if (this.listDelete !== undefined && this.listDelete.length > 0) {
            this.service.Delete(ids).subscribe(x => {
              if (x.success) {
                this.notifier.notify('success', 'Xoá thành công !');
                this.GetData();
              }
            } ,

            );
          }
          else
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
      // height: '350px',
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
  /** Whether the number of selected elements matches the total number of rows. */
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

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: WareHouseDTO): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.id}`;
  }
}
