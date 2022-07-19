import { LiveAnnouncer } from '@angular/cdk/a11y';
import { SelectionModel } from '@angular/cdk/collections';
import { FlatTreeControl } from '@angular/cdk/tree';
import { Component, HostListener, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatTreeFlattener, MatTreeFlatDataSource } from '@angular/material/tree';
import { Router } from '@angular/router';
import { NotifierService } from 'angular-notifier';
import { WareHouseBookDeleteComponent } from 'src/app/method/delete/WareHouseBookDelete/WareHouseBookDelete.component';
import { WareHouseBookDeleteAllComponent } from 'src/app/method/delete/WareHouseBookDeleteAll/WareHouseBookDeleteAll.component';
import { FormSearchReportTotalComponent } from 'src/app/method/search/FormSearchReportTotal/FormSearchReportTotal.component';
import { FormSearchWareHouseBookComponent } from 'src/app/method/search/formSearchWareHouseBook/formSearchWareHouseBook.component';
import { BaseSelectDTO } from 'src/app/model/BaseSelectDTO';
import { ReportValueTotalDT0 } from 'src/app/model/ReportValueTotalDT0';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';
import { TreeView } from 'src/app/model/TreeView';
import { ReportTotalSearchModel } from 'src/app/model/ReportTotalSearchModel';
import { GetDataToGprcService } from 'src/app/service/GetDataToGprc.service';
import { ReportService } from 'src/app/service/Report.service';
import { WarehouseService } from 'src/app/service/warehouse.service';
import { WareHouseBookService } from 'src/app/service/WareHouseBook.service';
import { SignalRService } from 'src/app/service/SignalR.service';




interface ExampleFlatNode {
  expandable: boolean;
  name: string;
  level: number;
  key: string;
}

@Component({
  selector: 'app-ReportTotal',
  templateUrl: './ReportTotal.component.html',
  styleUrls: ['./ReportTotal.component.scss']
})
export class ReportTotalComponent implements OnInit,OnDestroy {
  //select
  selection = new SelectionModel<ReportValueTotalDT0>(true, []);
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
  displayedColumns: string[] = ['id', 'wareHouseItemName', 'wareHouseItemCode', 'unitName', 'beginning', 'import', 'export', 'balance',];
  dataSource = new MatTableDataSource<ReportValueTotalDT0>();
  model: ReportTotalSearchModel = {
    active: null,
    keySearch: '',
    skip: this.currentPage * this.pageSize,
    take: this.pageSize,
    wareHouseId: '',
    itemId: null,
    start: null,
    end: null
  };
  list: ResultMessageResponse<ReportValueTotalDT0> = {
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

  constructor(public signalRService: SignalRService,private getDataToGprc: GetDataToGprcService, private route: Router, private service: ReportService, private _liveAnnouncer: LiveAnnouncer, public dialog: MatDialog, notifierService: NotifierService) {
    this.notifier = notifierService;
  }



  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  ngOnInit() {
    this.signalRService.hubConnection.on(this.signalRService.WareHouseBookTrachkingToCLient, (data: ResultMessageResponse<string>) => {
      if (data.success) {
        this.notifier.notify('success', data.message);
        this.GetData();
      }
    });
    this.signalRService.hubConnection.on(this.signalRService.CreateWareHouseBookTrachking, (data: ResultMessageResponse<string>) => {
      if (data.success) {
        this.notifier.notify('success', data.message);
        this.GetData();
      }
    });
    this.signalRService.hubConnection.on(this.signalRService.DeleteWareHouseBookTrachking, (data: ResultMessageResponse<string>) => {
      if (data.success) {
        this.notifier.notify('success', data.message);
        this.GetData();
      }
    });
    this.getScreenWidth = window.innerWidth;
    this.getScreenHeight = window.innerHeight;
    this.service.getTreeView().subscribe(x => this.dataSourceTreee.data = x.data);
  }

  ngOnDestroy(): void {
    // tắt phương thức vừa gọi để tránh bị gọi lại nhiều lần
    this.signalRService.hubConnection.off(this.signalRService.WareHouseBookTrachkingToCLient);
    this.signalRService.hubConnection.off(this.signalRService.CreateWareHouseBookTrachking);
    this.signalRService.hubConnection.off(this.signalRService.DeleteWareHouseBookTrachking);

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
    if (this.model.wareHouseId && this.model.start && this.model.end)
      this.service.getList(this.model.wareHouseId, this.model.itemId == null ? '' : this.model.itemId, this.model.start, this.model.end, 0, 15).subscribe(list => {
        if (list.data && !list.message) {
          this.dataSource.data = list.data;
          setTimeout(() => {
            this.paginator.pageIndex = this.currentPage;
            this.paginator.length = list.totalCount;
          });
        }
        else {
          this.notifier.notify('error', list.message);
        }


      },      
      );
    else
      this.notifier.notify('error', 'Bạn chưa chọn ngày !');


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
  //searchQueryDialog
  searchQueryDialog(): void {
    const dialogRef = this.dialog.open(FormSearchReportTotalComponent, {
      width: '550px',
      height: '350px',
      data: this.model
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result && result.wareHouseId && result.start && result.end) {
        var res = result;
        this.model.wareHouseId = res.wareHouseId;
        this.model.itemId = res.itemId;
        this.model.start = res.end !== null ? new Date(res.start).toLocaleDateString('en-US') : '';
        this.model.end = res.start !== null ? new Date(res.end).toLocaleDateString('en-US') : '';
        this.GetData();
      }

    });
  }

  GetActive(e: any) {
    var select = document.getElementById(e.key) as HTMLButtonElement;
    var selectDelete = document.querySelectorAll("#treeview button");
    selectDelete.forEach(element => {
      element.className = element.className.replace("activeButtonTreeView", " ");
    });
    select.className += " activeButtonTreeView";
    this.model.wareHouseId = e.key;
    this.route.navigate(["/"+e.key]);
  }


  ExportReport() {
    if (this.model.wareHouseId && this.model.start && this.model.end)
      this.service.getExportTotal(this.model.wareHouseId, this.model.itemId == null ? '' : this.model.itemId, this.model.start, this.model.end);
    else
      this.notifier.notify('error', 'Có lỗi với dữ liệu tìm kiếm, xin vui lòng thử lại !');

  }
}




