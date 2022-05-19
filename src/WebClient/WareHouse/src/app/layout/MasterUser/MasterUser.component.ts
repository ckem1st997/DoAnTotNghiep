import { Component, OnInit } from '@angular/core';
import { delay } from 'rxjs';
import { CheckRoleService } from 'src/app/service/CheckRole.service';
import { LoadingService } from 'src/app/service/Loading.service';

@Component({
  selector: 'app-MasterUser',
  templateUrl: './MasterUser.component.html',
  styleUrls: ['./MasterUser.component.scss']
})
export class MasterUserComponent implements OnInit {
  loading: boolean = false;

  constructor(
    private _loading: LoadingService,
    private role :CheckRoleService

  ){ }

  ngOnInit() {
    this.listenToLoading();
    this.role.CheckAuthozireMaster().subscribe(res=>{console.log(res)});;
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
