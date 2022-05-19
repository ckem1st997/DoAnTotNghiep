import { Component, OnInit } from '@angular/core';
import { delay } from 'rxjs';
import { LoadingService } from 'src/app/service/Loading.service';

@Component({
  selector: 'app-Authozire',
  templateUrl: './Authozire.component.html',
  styleUrls: ['./Authozire.component.scss']
})
export class AuthozireComponent implements OnInit {
  loading: boolean = false;

  constructor(
    private _loading: LoadingService
  ){ }

  ngOnInit() {
    this.listenToLoading();
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
