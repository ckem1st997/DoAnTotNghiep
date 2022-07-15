import { Component, HostListener } from '@angular/core';
import { trigger, transition, useAnimation } from "@angular/animations";
import { fromRightEasing, rotateCubeToLeft } from "ngx-router-animations";
import { delay } from 'rxjs';
import { LoadingService } from './service/Loading.service';
import { NotifierService } from 'angular-notifier';
import { SignalRService } from './service/SignalR.service';
import { AuthenticationService } from './extension/Authentication.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  animations: [
    trigger('rotateCubeToLeft', [transition('* => *', useAnimation(fromRightEasing, {
      params: { enterTiming: '1', leaveTiming: '1', enterDelay: '0.2', leaveDelay: '0.2' }
    }
    ))])
  ]
})
export class AppComponent {
  title = 'WareHouse';
  loading: boolean = false;
  checkSizeWindows: boolean = true;
  public getScreenWidth: any;
  public getScreenHeight: any;
  constructor(
    private _loading: LoadingService,
    private notifierService: NotifierService,
    private signalRService: SignalRService,
    private auth: AuthenticationService
  ) { }
  @HostListener('window:resize', ['$event'])
  ngAfterViewInit(){
    console.log("Time until reaching run phase: ", window.performance.now());
 }
  onWindowResize() {
    this.getScreenWidth = window.innerWidth;
    this.getScreenHeight = window.innerHeight;
    if (this.getScreenWidth <= 768)
      this.checkSizeWindows = false;
    else
      this.checkSizeWindows = true;
      console.log(this.checkSizeWindows);
  }
  ngOnInit() {
    this.listenToLoading();
    this.signalRService.startConnection();
    //  this.signalRService.CallMethodToServiceByInwardChange('SendMessageToCLient');
  }

  /**
   * Listen to the loadingSub property in the LoadingService class. This drives the
   * display of the loading spinner.
   */
  listenToLoading(): void {
    this._loading.loadingSub
      .pipe(delay(0)) // This prevents a ExpressionChangedAfterItHasBeenCheckedError for subsequent requests
      .subscribe((loading) => {
        this.loading = loading;
      });
  }

}
