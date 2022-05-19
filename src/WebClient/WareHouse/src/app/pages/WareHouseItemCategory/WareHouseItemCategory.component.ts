import { LiveAnnouncer } from '@angular/cdk/a11y';
import { SelectionModel } from '@angular/cdk/collections';
import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { NotifierService } from 'angular-notifier';
import { WareHouseCreateComponent } from 'src/app/method/create/WareHouseCreate/WareHouseCreate.component';
import { WareHouseItemCategoryCreateComponent } from 'src/app/method/create/WareHouseItemCategoryCreate/WareHouseItemCategoryCreate.component';
import { WareHouseDeleteComponent } from 'src/app/method/delete/WareHouseDelete/WareHouseDelete.component';
import { WareHouseItemCategoryDelelteComponent } from 'src/app/method/delete/WareHouseItemCategoryDelelte/WareHouseItemCategoryDelelte.component';
import { WareHouseDetailsComponent } from 'src/app/method/details/WareHouseDetails/WareHouseDetails.component';
import { WareHouseItemCategoryEditDetailsComponent } from 'src/app/method/details/WareHouseItemCategoryEditDetails/WareHouseItemCategoryEditDetails.component';

import { WareHouseItemCategoryEditComponent } from 'src/app/method/edit/WareHouseItemCategoryEdit/WareHouseItemCategoryEdit.component';
import { FormSearchWareHouseComponent } from 'src/app/method/search/FormSearchWareHouse/FormSearchWareHouse.component';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';
import { WareHouseItemCategoryDTO } from 'src/app/model/WareHouseItemCategoryDTO';
import { WareHouseSearchModel } from 'src/app/model/WareHouseSearchModel';
import { WareHouseItemCategoryService } from 'src/app/service/WareHouseItemCategory.service';

@Component({
  selector: 'app-WareHouseItemCategory',
  templateUrl: './WareHouseItemCategory.component.html',
  styleUrls: ['./WareHouseItemCategory.component.scss']
})
export class WareHouseItemCategoryComponent implements OnInit {
  //
  listDelete: WareHouseItemCategoryDTO[] = [];
  //select
  selection = new SelectionModel<WareHouseItemCategoryDTO>(true, []);
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
  displayedColumns: string[] = ['select', 'id', 'name', 'code', 'description', 'parentId', 'inactive', 'method'];
  dataSource = new MatTableDataSource<WareHouseItemCategoryDTO>();
  model: WareHouseSearchModel = {
    active: null,
    keySearch: '',
    skip: this.currentPage * this.pageSize,
    take: this.pageSize
  };
  list: ResultMessageResponse<WareHouseItemCategoryDTO> = {
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
  constructor(private service: WareHouseItemCategoryService, private _liveAnnouncer: LiveAnnouncer, public dialog: MatDialog, notifierService: NotifierService) {
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
  openDialog(model: WareHouseItemCategoryDTO): void {
    this.service.EditIndex(model.id).subscribe(x => {
      const dialogRef = this.dialog.open(WareHouseItemCategoryEditComponent, {
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
  openDialogDetals(model: WareHouseItemCategoryDTO): void {
    this.service.Details(model.id).subscribe(x => {
      const dialogRef = this.dialog.open(WareHouseItemCategoryEditDetailsComponent, {
        width: '550px',
        data: x.data,
      });

    });

  
  }
  openDialogCreate(): void {
    this.service.AddIndex().subscribe(x => {

      const dialogRef = this.dialog.open(WareHouseItemCategoryCreateComponent, {
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

  openDialogDelelte(model: WareHouseItemCategoryDTO): void {
    this.listDelete.push(model);
    const dialogRef = this.dialog.open(WareHouseItemCategoryDelelteComponent, {
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
      const dialogRef = this.dialog.open(WareHouseItemCategoryDelelteComponent, {
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
      this.notifier.notify('warning', "Bạn chưa loại vật tư nào !");

  }
  //searchQueryDialog
  searchQueryDialog(): void {
    const dialogRef = this.dialog.open(FormSearchWareHouseComponent, {
      width: '550px',
      height: '350px',
    });

    dialogRef.afterClosed().subscribe(result => {
      var res = result;
      this.model.keySearch = res.key;
      this.model.active = res.inactive;
      this.GetData();
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
  checkboxLabel(row?: WareHouseItemCategoryDTO): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.id}`;
  }
}
