import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NotifierService } from 'angular-notifier';
import { Observable, retry, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HistoryNoticationDT0 } from '../model/HistoryNoticationDT0';
import { ListAuthozire } from '../model/ListApp';
import { ResultDataResponse } from '../model/ResultDataResponse';
import { ResultMessageResponse } from '../model/ResultMessageResponse';

@Injectable({
  providedIn: 'root'
})
export class ListAuthozireService {

  private baseUrlMaster = environment.authorizeApi+'ListAuthozire';
  private readonly notifier!: NotifierService;
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient, notifierService: NotifierService) {
    this.notifier = notifierService;
  }

  //get list user master
  getList(): Observable<ResultMessageResponse<ListAuthozire>> {
    var url = this.baseUrlMaster + `/get-list`;
    return this.http.get<ResultMessageResponse<ListAuthozire>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times

    );
  }

  EditIndex(iduser: string | null): Observable<ResultDataResponse<ListAuthozire>> {
    var url = this.baseUrlMaster + `/edit?id=` + iduser;
    return this.http.get<ResultDataResponse<ListAuthozire>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
    );
  }

  Edit(model: ListAuthozire): Observable<ResultDataResponse<ListAuthozire>> {
    var url = this.baseUrlMaster + `/edit`;
    return this.http.post<ResultDataResponse<ListAuthozire>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
    );
  }

  CreateIndex(iduser: string | null): Observable<ResultDataResponse<ListAuthozire>> {
    var url = this.baseUrlMaster + `/create?id=` + iduser;
    return this.http.get<ResultDataResponse<ListAuthozire>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
    );
  }

  Create(model: ListAuthozire): Observable<ResultDataResponse<ListAuthozire>> {
    var url = this.baseUrlMaster + `/create`;
    return this.http.post<ResultDataResponse<ListAuthozire>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
    );
  }

  Delete(ids:string[]): Observable<ResultMessageResponse<ListAuthozire>> {
    var url = this.baseUrlMaster + `/delete`;
    return this.http.post<ResultMessageResponse<ListAuthozire>>(url, ids, this.httpOptions).pipe(
      tap(_ => console.log(`delete  id=${ids}`)),
     
    );
  }
}

