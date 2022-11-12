import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NotifierService } from 'angular-notifier';
import { Observable, retry, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HistoryNoticationDT0 } from '../model/HistoryNoticationDT0';
import { ListAuthozireByListRole } from '../model/ListApp';
import { ResultDataResponse } from '../model/ResultDataResponse';
import { ResultMessageResponse } from '../model/ResultMessageResponse';

@Injectable({
  providedIn: 'root'
})
export class ListAuthozireByListRoleService {

  private baseUrlMaster = environment.authorizeApi + 'ListAuthozireByListRole';
  private readonly notifier!: NotifierService;
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient, notifierService: NotifierService) {
    this.notifier = notifierService;
  }

  //get list user master
  getList(): Observable<ResultMessageResponse<ListAuthozireByListRole>> {
    var url = this.baseUrlMaster + `/get-list`;
    return this.http.get<ResultMessageResponse<ListAuthozireByListRole>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times

    );
  }



  getListByUserId(id: string,appId:string): Observable<ResultMessageResponse<string>> {
    var url = this.baseUrlMaster + `/get-list-id?id=` + id+'&appId='+appId;
    return this.http.get<ResultMessageResponse<string>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times

    );
  }

  EditIndex(iduser: string | null): Observable<ResultDataResponse<ListAuthozireByListRole>> {
    var url = this.baseUrlMaster + `/edit?id=` + iduser;
    return this.http.get<ResultDataResponse<ListAuthozireByListRole>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
    );
  }

  Edit(model: ListAuthozireByListRole): Observable<ResultDataResponse<ListAuthozireByListRole>> {
    var url = this.baseUrlMaster + `/edit`;
    return this.http.post<ResultDataResponse<ListAuthozireByListRole>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
    );
  }

  EditOrCreate(ids: Array<string>, id: string,appid:string): Observable<ResultDataResponse<ListAuthozireByListRole>> {
    var url = this.baseUrlMaster + `/edit-update?id=`+id+'&appId='+appid;
    return this.http.post<ResultDataResponse<ListAuthozireByListRole>>(url, ids, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
    );
  }


  CreateIndex(iduser: string | null): Observable<ResultDataResponse<ListAuthozireByListRole>> {
    var url = this.baseUrlMaster + `/create?id=` + iduser;
    return this.http.get<ResultDataResponse<ListAuthozireByListRole>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
    );
  }

  Create(model: ListAuthozireByListRole): Observable<ResultDataResponse<ListAuthozireByListRole>> {
    var url = this.baseUrlMaster + `/create`;
    return this.http.post<ResultDataResponse<ListAuthozireByListRole>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
    );
  }

  Delete(ids: string[]): Observable<ResultMessageResponse<ListAuthozireByListRole>> {
    var url = this.baseUrlMaster + `/delete`;
    return this.http.post<ResultMessageResponse<ListAuthozireByListRole>>(url, ids, this.httpOptions).pipe(
      tap(_ => console.log(`delete  id=${ids}`)),

    );
  }
}

