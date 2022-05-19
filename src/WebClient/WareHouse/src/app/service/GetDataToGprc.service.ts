import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NotifierService } from 'angular-notifier';
import { Observable, retry } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BaseSelectDTO } from '../model/BaseSelectDTO';
import { InwardDTO } from '../model/InwardDTO';
import { ResultMessageResponse } from '../model/ResultMessageResponse';

@Injectable({
  providedIn: 'root'
})

export class GetDataToGprcService {
  private baseUrl = environment.baseApi + 'GetDataGPRC';
  private readonly notifier!: NotifierService;
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient, notifierService: NotifierService) {
    this.notifier = notifierService;
  }


  getListAccount(): Observable<ResultMessageResponse<BaseSelectDTO>> {
    var url = this.baseUrl + `/get-list-account?`;
    return this.http.get<ResultMessageResponse<BaseSelectDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
    );
  }
}
