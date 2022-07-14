import { Component, OnInit, OnDestroy } from '@angular/core';
import { NotifierService } from 'angular-notifier';
import { MasterGetService } from 'src/app/service/MasterGet.service';
import { SignalRService } from 'src/app/service/SignalR.service';

@Component({
  selector: 'app-masterhome',
  templateUrl: './masterhome.component.html',
  styleUrls: ['./masterhome.component.scss']
})
export class MasterhomeComponent implements OnInit, OnDestroy  {

  constructor(private signalRService: SignalRService,private master:MasterGetService,private notifierService: NotifierService) { }

  ngOnInit() {
    
  }
  AsyncSqlElastic(){
    this.master.AsyncSqlElastic().subscribe(res => {
      if(res.success)
      {
        this.notifierService.notify('success', 'Đã cập nhật dữ liệu thành công !');
        this.signalRService.SendAsyncWareHouseBookTrachking();
      }
      else
        this.notifierService.notify('error', 'Có lỗi xảy ra, chưa update thành công !');
    })

  }
  
  ngOnDestroy() {
    this.signalRService.off(this.signalRService.WareHouseBookTrachkingToCLient);
    this.signalRService.hubConnection.off(this.signalRService.CreateWareHouseBookTrachking);
    this.signalRService.hubConnection.off(this.signalRService.DeleteWareHouseBookTrachking);
  }
}
