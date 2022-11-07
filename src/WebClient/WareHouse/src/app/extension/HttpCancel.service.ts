import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { LoadingService } from '../service/Loading.service';

@Injectable({
  providedIn: 'root'
})
export class HttpCancelService {

  private cancelPendingRequests$ = new Subject<void>()

  constructor() { }

  /** Cancels all pending Http requests. */
  public cancelPendingRequests() {
    this.cancelPendingRequests$.next()
  }

  public onCancelPendingRequests() {
    return this.cancelPendingRequests$.asObservable()
  }

}