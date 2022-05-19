import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NotifierService } from 'angular-notifier';
import { Observable, retry } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BaseSelectDTO } from '../model/BaseSelectDTO';
import { DashBoardChartInAndOutCountDTO } from '../model/DashBoardChartInAndOutCountDTO';
import { DashBoardSelectTopInAndOutDTO } from '../model/DashBoardSelectTopInAndOutDTO';
import { ResultDataResponse } from '../model/ResultDataResponse';
import { ResultMessageResponse } from '../model/ResultMessageResponse';
import { SelectTopDashBoardDTO } from '../model/SelectTopDashBoardDTO';
import { SelectTopWareHouseDTO } from '../model/SelectTopWareHouseDTO';
import { WareHouseBookDTO } from '../model/WareHouseBookDTO';

@Injectable({
  providedIn: 'root'
})
export class DashBoardService {
  private baseUrl = environment.baseApi + 'DashBoard';
  private readonly notifier!: NotifierService;
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient, notifierService: NotifierService) {
    this.notifier = notifierService;
  }


  getTopInward(): Observable<ResultMessageResponse<DashBoardSelectTopInAndOutDTO>> {
    var url = this.baseUrl + `/get-select-top-inward-order-by?order=desc&selectTopWareHouseBook=0`;
    return this.http.get<ResultMessageResponse<DashBoardSelectTopInAndOutDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
    );
  }

  getTopOutward(): Observable<ResultMessageResponse<DashBoardSelectTopInAndOutDTO>> {
    var url = this.baseUrl + `/get-select-top-outward-order-by?order=desc&selectTopWareHouseBook=0`;
    return this.http.get<ResultMessageResponse<DashBoardSelectTopInAndOutDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
    );
  }

  getTopIndex(): Observable<ResultDataResponse<SelectTopDashBoardDTO>> {
    var url = this.baseUrl + `/get-select-chart-by-index`;
    return this.http.get<ResultDataResponse<SelectTopDashBoardDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
    );
  }

  getChartByYear(year:number): Observable<ResultDataResponse<DashBoardChartInAndOutCountDTO> > {
    var url = this.baseUrl + `/get-select-chart-by-year?Year=`+year+``;
    return this.http.get<ResultDataResponse<DashBoardChartInAndOutCountDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
    );
  }

  getChartByMouth(f:string,t:string): Observable<ResultDataResponse<DashBoardChartInAndOutCountDTO> > {
    var url = this.baseUrl + `/get-select-chart-by-mouth?fromDate=`+f+`&toDate=`+t+``;
    return this.http.get<ResultDataResponse<DashBoardChartInAndOutCountDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
    );
  }
  getChartByWareHouse(): Observable<ResultMessageResponse<SelectTopWareHouseDTO>> {
    var url = this.baseUrl + `/get-select-top-warehouse-beginning-order-by?order=desc`;
    return this.http.get<ResultMessageResponse<SelectTopWareHouseDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
    );
  }

  getHistory(): Observable<ResultMessageResponse<WareHouseBookDTO>> {
    var url = environment.baseApi + `WareHouseBook/get-list?Skip=0&Take=5`;
    return this.http.get<ResultMessageResponse<WareHouseBookDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
    );
  }
}
