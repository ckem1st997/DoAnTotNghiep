import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NotifierService } from 'angular-notifier';
import { Observable, retry, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HistoryNoticationDT0 } from '../model/HistoryNoticationDT0';
import { ListRole } from '../model/ListApp';
import { ResultDataResponse } from '../model/ResultDataResponse';
import { ResultMessageResponse } from '../model/ResultMessageResponse';
import { TreeView } from './../model/TreeView';

@Injectable({
  providedIn: 'root'
})
export class ListRoleService {

  private baseUrlMaster = environment.authorizeApi+'ListRole';
  private readonly notifier!: NotifierService;
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient, notifierService: NotifierService) {
    this.notifier = notifierService;
  }

  //get list user masterget-list-tree
  getList(id:string): Observable<ResultMessageResponse<ListRole>> {
    var url = this.baseUrlMaster + `/get-list?appId=`+id;
    return this.http.get<ResultMessageResponse<ListRole>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times

    );
  }

  getListTree(id:string): Observable<ResultMessageResponse<ListRole>> {
    var url = this.baseUrlMaster + `/get-list-tree?appId=`+id;
    return this.http.get<ResultMessageResponse<ListRole>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times

    );
  }


  EditIndex(iduser: string | null): Observable<ResultDataResponse<ListRole>> {
    var url = this.baseUrlMaster + `/edit?id=` + iduser;
    return this.http.get<ResultDataResponse<ListRole>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
    );
  }

  Edit(model: ListRole): Observable<ResultDataResponse<ListRole>> {
    var url = this.baseUrlMaster + `/edit`;
    return this.http.post<ResultDataResponse<ListRole>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
    );
  }

  CreateIndex(iduser: string | null): Observable<ResultDataResponse<ListRole>> {
    var url = this.baseUrlMaster + `/create?id=` + iduser;
    return this.http.get<ResultDataResponse<ListRole>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
    );
  }

  Create(model: ListRole): Observable<ResultDataResponse<ListRole>> {
    var url = this.baseUrlMaster + `/create`;
    return this.http.post<ResultDataResponse<ListRole>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
    );
  }

  Delete(ids:string[]): Observable<ResultMessageResponse<ListRole>> {
    var url = this.baseUrlMaster + `/delete`;
    return this.http.post<ResultMessageResponse<ListRole>>(url, ids, this.httpOptions).pipe(
      tap(_ => console.log(`delete  id=${ids}`)),
     
    );
  }
}

