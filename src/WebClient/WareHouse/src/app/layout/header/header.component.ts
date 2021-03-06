import { Component, OnInit, OnDestroy } from '@angular/core';
import { number } from 'echarts';
import { AuthenticationService } from 'src/app/extension/Authentication.service';
import { HistoryNoticationDT0 } from 'src/app/model/HistoryNoticationDT0';
import { ResultDataResponse } from 'src/app/model/ResultDataResponse';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';
import { AuthozireService } from 'src/app/service/Authozire.service';
import { SignalRService } from 'src/app/service/SignalR.service';
import { SpeedTestService } from 'ng-speed-test';
import isOnline from 'is-online';
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit, OnDestroy {
  networkStatus: boolean = true;
  speedTest: number=0;
  userName: string | undefined;
  activeNoticaonList: boolean = false;
  listBefor!: HistoryNoticationDT0;
  listAfter!: HistoryNoticationDT0[];
  countHistory: number = 0;
  listHistory: ResultMessageResponse<HistoryNoticationDT0> = {
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
  };
  constructor(private speedTestService: SpeedTestService, public signalRService: SignalRService, private service: AuthenticationService, private serviceAuthozire: AuthozireService) { }

  ngOnInit() {
    this.userName = this.service.userValue.username;
    this.getHistory();
    this.signalRService.hubConnection.on(this.signalRService.HistoryTrachking, (data: ResultDataResponse<string>) => {
      if (data.success) {
        console.log(data);
        if (this.service.userValue.username === data.data || this.service.userValue.role === 3) {
          this.getHistory();
        }
      }
    });
    setInterval(() => {
      (async () => {
        this.networkStatus = await isOnline();
        this.checkNetworkStatus();
      })();
    }, 3000);
  }

  checkNetworkStatus() {
    if (this.networkStatus)
      this.speedTestService.getMbps(
        {
          iterations: 1,
          retryDelay: 1500,
        }
      ).subscribe(
        (speed) => {
          this.speedTest = speed==null?0:speed;
        }
      );
    else
      this.speedTest = 0;
  }
  ngOnDestroy(): void {
    // t???t ph????ng th???c v???a g???i ????? tr??nh b??? g???i l???i nhi???u l???n
    this.signalRService.hubConnection.off(this.signalRService.HistoryTrachking);
  }
  getHistory() {
    this.serviceAuthozire.getListHistoryByUser().subscribe(res => { this.listHistory = res; this.listBefor = res.data[0]; this.listAfter = res.data.slice(1); this.countHistory = res.data.filter(x => x.userNameRead == null || !x.userNameRead.includes(this.service.userValue.username)).length; });

  }

  getCount() {
    if (this.countHistory > 99)
      return '99+';
    else
      return this.countHistory.toString();
  }

  /// active to click
  changActive(e: any): number {

    var activeMenu = e.target.parentElement.getAttribute("data-active");
    if (activeMenu != null && activeMenu != undefined && activeMenu.length > 0) {
      var elementA = DeleteActiveAll();
      document.getElementById(activeMenu)?.classList.add("active");
      return 1;
    }
    var check = e.target.className;
    if (check.includes("mat-icon")) {
      check = e.target.parentElement.className;
      if (check !== undefined && !check.includes("active")) {
        var elementA = DeleteActiveAll();
        e.target.parentElement.classList.add("active");
      }
    }
    else
      if (check !== undefined && !check.includes("active")) {
        var elementA = DeleteActiveAll();
        e.target.classList.add("active");
      }
    return 1;

    function DeleteActiveAll() {
      var elementA = document.getElementsByClassName("header-a");
      for (let index = 0; index < elementA.length; index++) {
        const element = elementA[index];
        element.classList.remove("active");
      }
      return elementA;
    }
  }
  logout() {
    this.signalRService.stop();
    this.service.logout();
  }
  showNotication() {
    this.activeNoticaonList = !this.activeNoticaonList;
    if (this.service.userCheck) {
      var listActive = this.listHistory.data.filter(x => x.userNameRead == null || !x.userNameRead.includes(this.service.userValue.username));
      if (listActive.length > 0) {
        var ids = new Array<string>();
        for (let index = 0; index < listActive.length; index++) {
          const element = listActive[index];
          ids.push(element.id);
        }
        this.serviceAuthozire.ActiveRead(ids).subscribe(res => {
          if (res)
            this.getHistory();
        });
      }
      //    if (this.activeNoticaonList)
      //     this.serviceAuthozire.getListHistoryByUser().subscribe(res => { this.listHistory = res; this.listBefor = res.data[0]; this.listAfter = res.data.slice(1); });
    }
  }

  GetDateTime(e: Date) {
    return e;
  }

  CheckMethod(method: string) {
    if (method.includes("T???o"))
      return 1;
    else if (method.includes("Ch???nh s???a"))
      return 2;
    else if (method.includes("X??a"))
      return 3;
    return 0;
  }
}








