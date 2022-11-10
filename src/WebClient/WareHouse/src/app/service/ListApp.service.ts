import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NotifierService } from 'angular-notifier';
import { Observable, retry, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HistoryNoticationDT0 } from '../model/HistoryNoticationDT0';
import { ResultDataResponse } from '../model/ResultDataResponse';
import { ResultMessageResponse } from '../model/ResultMessageResponse';
import { ListApp } from '../model/ListApp';
import { TreeNode } from 'primeng/api';

@Injectable({
  providedIn: 'root'
})
export class ListAppService {

  private baseUrlMaster = environment.authorizeApi+'ListApp';
  private readonly notifier!: NotifierService;
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient, notifierService: NotifierService) {
    this.notifier = notifierService;
  }

  //get list user master
  getList(): Observable<ResultMessageResponse<ListApp>> {
    var url = this.baseUrlMaster + `/get-list`;
    return this.http.get<ResultMessageResponse<ListApp>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times

    );
  }

   

  EditIndex(iduser: string | null): Observable<ResultDataResponse<ListApp>> {
    var url = this.baseUrlMaster + `/edit?id=` + iduser;
    return this.http.get<ResultDataResponse<ListApp>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
    );
  }

  Edit(model: ListApp): Observable<ResultDataResponse<ListApp>> {
    var url = this.baseUrlMaster + `/edit`;
    return this.http.post<ResultDataResponse<ListApp>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
    );
  }

  CreateIndex(iduser: string | null): Observable<ResultDataResponse<ListApp>> {
    var url = this.baseUrlMaster + `/create?id=` + iduser;
    return this.http.get<ResultDataResponse<ListApp>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
    );
  }

  Create(model: ListApp): Observable<ResultDataResponse<ListApp>> {
    var url = this.baseUrlMaster + `/create`;
    return this.http.post<ResultDataResponse<ListApp>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
    );
  }

  Delete(ids:string[]): Observable<ResultMessageResponse<ListApp>> {
    var url = this.baseUrlMaster + `/delete`;
    return this.http.post<ResultMessageResponse<ListApp>>(url, ids, this.httpOptions).pipe(
      tap(_ => console.log(`delete  id=${ids}`)),
     
    );
  }
}

