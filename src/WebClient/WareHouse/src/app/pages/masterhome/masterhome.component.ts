import { Component, OnInit, OnDestroy } from '@angular/core';
import { ThemePalette } from '@angular/material/core';
import { NotifierService } from 'angular-notifier';
import { MasterGetService } from 'src/app/service/MasterGet.service';
import { SignalRService } from 'src/app/service/SignalR.service';

@Component({
  selector: 'app-masterhome',
  templateUrl: './masterhome.component.html',
  styleUrls: ['./masterhome.component.scss']
})
export class MasterhomeComponent implements OnInit, OnDestroy {
  indexString: string = '0-0';
  color: ThemePalette = 'accent';
  checkedRedis = false;
  checkSql = false;
  checkElastic = false;
  disabled = false;
  constructor(private signalRService: SignalRService, private master: MasterGetService, private notifierService: NotifierService) { }

  ngOnInit() {
    this.GetIndexSqlElastic();
    this.CanConnectElastic();
    this.CanConnectRedis();
    this.CanConnectSql();
  }



  CanConnectRedis() {
    this.master.CanConnectRedis().subscribe(res => {
      this.checkedRedis = res.success;
    }
    )
  }

  GetStatus(t: boolean): string {
    return t ? "Đang hoạt động" : "Ngừng hoạt động";
  }


  CanConnectElastic() {
    this.master.CanConnectElastic().subscribe(res => {
      this.checkElastic = res.success;
    }
    )
  }



  CanConnectSql() {
    this.master.CanConnectSql().subscribe(res => {
      this.checkSql = res.success;
    }
    )
  }


  DeleteAllElastic() {
    this.master.DeleteAllElastic().subscribe(res => {
      if (res.success) {
        this.GetIndexSqlElastic();
        this.notifierService.notify('success', 'Đã xóa dữ liệu thành công !');
      }
      else
        this.notifierService.notify('error', 'Có lỗi xảy ra, chưa xóa dữ liệu thành công !');
    }
    )
  }



  GetIndexSqlElastic() {
    this.master.GetIndexSqlElastic().subscribe(res => {
      if (res.success)
        this.indexString = res.data;
    });
  }

  AsyncSqlElastic() {
    this.master.AsyncSqlElastic().subscribe(res => {
      if (res.success) {
        this.GetIndexSqlElastic();
        this.notifierService.notify('success', 'Đã cập nhật dữ liệu thành công !');
        this.signalRService.SendAsyncWareHouseBookTrachking();
      }
      else
        this.notifierService.notify('error', 'Có lỗi xảy ra, chưa update thành công !');
    })

  }
  DeleteAllCache() {
    this.master.DeleteAllCache().subscribe(res => {
      console.log(res);
      if (res.success) {
        this.notifierService.notify('success', 'Đã xóa cache thành công !');
      }
      else
        this.notifierService.notify('error', 'Có lỗi xảy ra, chưa xóa cache thành công !');
    }
    )
  }
  ngOnDestroy() {
    this.signalRService.off(this.signalRService.WareHouseBookTrachkingToCLient);
    this.signalRService.hubConnection.off(this.signalRService.CreateWareHouseBookTrachking);
    this.signalRService.hubConnection.off(this.signalRService.DeleteWareHouseBookTrachking);
  }
}
