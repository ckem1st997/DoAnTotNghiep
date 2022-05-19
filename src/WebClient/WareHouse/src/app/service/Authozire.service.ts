import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NotifierService } from 'angular-notifier';
import { Observable, retry, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HistoryNoticationDT0 } from '../model/HistoryNoticationDT0';
import { ResultDataResponse } from '../model/ResultDataResponse';
import { ResultMessageResponse } from '../model/ResultMessageResponse';
import { UserMaster } from '../model/UserMaster';

@Injectable({
  providedIn: 'root'
})
export class AuthozireService {

  private baseUrlMaster = environment.authorizeApi;
  private readonly notifier!: NotifierService;
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient, notifierService: NotifierService) {
    this.notifier = notifierService;
  }

  //get list user master
  getList(key: string, whId: string, pages: number, num: number): Observable<ResultMessageResponse<UserMaster>> {
    var url = this.baseUrlMaster + `AuthorizeMaster/get-list?Number=` + num + `&Pages=` + pages + `&Key=` + key + `&Id=` + whId + ``;
    return this.http.get<ResultMessageResponse<UserMaster>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times

    );
  }

  getUser(): Observable<ResultDataResponse<UserMaster>> {
    var url = this.baseUrlMaster + `ApiPublic/get-user`;
    return this.http.get<ResultDataResponse<UserMaster>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
    );
  }

  getListHistoryByUser(): Observable<ResultMessageResponse<HistoryNoticationDT0>> {
    var url = this.baseUrlMaster + `ApiPublic/get-list-by-user`;
    return this.http.get<ResultMessageResponse<HistoryNoticationDT0>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times

    );
  }

  EditIndex(iduser: string | null): Observable<ResultDataResponse<UserMaster>> {
    var url = this.baseUrlMaster + `AuthorizeMaster/role-edit?id=` + iduser;
    return this.http.get<ResultDataResponse<UserMaster>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
    );
  }

  Edit(model: UserMaster): Observable<ResultDataResponse<UserMaster>> {
    var url = this.baseUrlMaster + `AuthorizeMaster/role-edit`;
    return this.http.post<ResultDataResponse<UserMaster>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
    );
  }


  ActiveRead(ids:string[]): Observable<ResultMessageResponse<HistoryNoticationDT0>> {
    var url = this.baseUrlMaster + `ApiPublic/active-read`;
    return this.http.post<ResultMessageResponse<HistoryNoticationDT0>>(url,ids, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times

    );
  }
}

