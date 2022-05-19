import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { catchError, retry, tap } from 'rxjs/operators';
import { ResultMessageResponse } from '../model/ResultMessageResponse';
import { environment } from './../../environments/environment';
import { TreeView } from '../model/TreeView';
import { UnitDTO } from '../model/UnitDTO';
import { Unit } from '../entity/Unit';
import { UnitSearchModel } from '../model/UnitSearchModel';

@Injectable({
  providedIn: 'root'
})
export class UnitService {
  private baseUrl = environment.baseApi+'Unit';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient) { }

  getById(id:string): Observable<ResultMessageResponse<UnitDTO>> {
    var url = this.baseUrl + `/get-list?`;
    return this.http.get<ResultMessageResponse<UnitDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
     
    );
  }

  getList(search: UnitSearchModel): Observable<ResultMessageResponse<UnitDTO>> {
    var check = search.active == null ? '' : search.active;
    var url = this.baseUrl + `/get-list?KeySearch=` + search.keySearch + `&Active=` + check + `&Skip=` + search.skip + `&Take=` + search.take + ``;
    return this.http.get<ResultMessageResponse<UnitDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
     
    );
  }
  getListDropDown(): Observable<ResultMessageResponse<UnitDTO>> {
    var url = this.baseUrl + `/get-drop-tree?Active=true`;
    return this.http.get<ResultMessageResponse<UnitDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
     
    );
  }

  Details(id:string): Observable<ResultMessageResponse<Unit>> {
    var url = this.baseUrl + `/details?id=` + id;
    return this.http.get<ResultMessageResponse<Unit>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`details Unit id=${id}`)),
     
    );
  }

  EditIndex(id:string): Observable<ResultMessageResponse<Unit>> {
    var url = this.baseUrl + `/edit?id=` + id;
    return this.http.get<ResultMessageResponse<Unit>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`edit Unit id=${id}`)),
     
    );
  }

  AddIndex(): Observable<ResultMessageResponse<Unit>> {
    var url = this.baseUrl + `/create`;
    return this.http.get<ResultMessageResponse<Unit>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`add Unit`)),
     
    );
  }


  Edit(model: Unit): Observable<ResultMessageResponse<Unit>> {
    var url = this.baseUrl + `/edit`;
    return this.http.post<ResultMessageResponse<Unit>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`edit id=${model.id}`)),
     
    );
  }

  Add(model: Unit): Observable<ResultMessageResponse<Unit>> {
    var url = this.baseUrl + `/create`;
    return this.http.post<ResultMessageResponse<Unit>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`create id=${model.id}`)),
     
    );
  }

 Delete(ids:string[]): Observable<ResultMessageResponse<Unit>> {
    var url = this.baseUrl + `/delete`;
    return this.http.post<ResultMessageResponse<Unit>>(url, ids, this.httpOptions).pipe(
      tap(_ => console.log(`delete id=${ids}`)),
     
    );
  }

}