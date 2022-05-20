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
@Component({
  selector: 'app-Vendor',
  templateUrl: './Vendor.component.html',
  styleUrls: ['./Vendor.component.scss']
})
export class VendorComponent implements OnInit {
  //
  listDelete: VendorDTO[] = [];
  //select
  selection = new SelectionModel<VendorDTO>(true, []);
  //noti
  private readonly notifier!: NotifierService;
  //tree-view
  private _transformer = (node: TreeView, level: number) => {
    return {
      expandable: !!node.children && node.children.length > 0,
      name: node.name,
      level: level,
    };
  };

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
  checkSizeWindows: boolean = true;
  public getScreenWidth: any;
  public getScreenHeight: any;
  //
  panelOpenState = false;
  animal: string = '';
  name: string = '';
  checkedl = false;
  totalRows = 0;
  pageSize = 15;
  currentPage = 0;
  pageSizeOptions: number[] = [15, 50, 100];
  displayedColumns: string[] = ['select', 'id', 'name', 'code', 'address', 'phone', 'email', 'contactPerson', 'inactive', 'method'];
  dataSource = new MatTableDataSource<VendorDTO>();
  model: VendorSearchModel = {
    active: null,
    keySearch: '',
    skip: this.currentPage * this.pageSize,
    take: this.pageSize
  };
  list: ResultMessageResponse<VendorDTO> = {
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
  constructor(private service: VendorService, private _liveAnnouncer: LiveAnnouncer, public dialog: MatDialog, notifierService: NotifierService) {
    this.notifier = notifierService;
  }
  hasChild = (_: number, node: ExampleFlatNode) => node.expandable;
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  ngOnInit() {
    this.GetData();
    this.service.getTreeView().subscribe(x => this.dataSourceTreee.data = x.data);
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
  lll(l: string) {
    console.log(l);
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
  openDialog(model: VendorDTO): void {
    this.service.EditIndex(model.id).subscribe(x => {
      const dialogRef = this.dialog.open(VendorEditComponent, {
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
  openDialogDetals(model: VendorDTO): void {
    this.service.Details(model.id).subscribe(x => {
      const dialogRef = this.dialog.open(VendorDetailsComponent, {
        width: '550px',
        data: x.data,
      });

    });

  }
  openDialogCreate(): void {
    this.service.AddIndex().subscribe(x => {

      const dialogRef = this.dialog.open(VendorCreateComponent, {
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

  openDialogDelelte(model: VendorDTO): void {
    var val = document.getElementById("searchInput") as HTMLInputElement;
    this.listDelete.push(model);
    const dialogRef = this.dialog.open(VendorDeleteComponent, {
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
      var val = document.getElementById("searchInput") as HTMLInputElement;
      this.listDelete = model;
      const dialogRef = this.dialog.open(VendorDeleteComponent, {
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
      this.notifier.notify('warning', "Bạn chưa nhà cung cấp nào !");


  }
  //searchQueryDialog
  searchQueryDialog(): void {
    const dialogRef = this.dialog.open(FormsearchComponent, {
      width: '550px',
    });

    dialogRef.afterClosed().subscribe(result => {
      var res = result;
      console.log(res);
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
  checkboxLabel(row?: VendorDTO): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.id}`;
  }
}
