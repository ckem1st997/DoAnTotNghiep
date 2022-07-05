import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { LoadingService } from '../service/Loading.service';

@Injectable({
  providedIn: 'root'
})
export class HttpCancelService {

  private pendingHTTPRequests$ = new Subject<void>();

  constructor(private _loading: LoadingService,
  ) { }

  // Cancel Pending HTTP calls
  public cancelPendingRequests(url: string) {
    this.pendingHTTPRequests$.next();
    this._loading.setLoading(false, url);

  }

  public onCancelPendingRequests() {
    return this.pendingHTTPRequests$.asObservable();
  }

}