import { HttpHeaders, HttpClient, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, retry, catchError, tap, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { WareHouseItem } from '../entity/WareHouseItem';
import { BaseSearchModel } from '../model/BaseSearchModel';
import { ResultMessageResponse } from '../model/ResultMessageResponse';
import { WareHouseDTO } from '../model/WareHouseDTO';
import { WareHouseItemDTO } from '../model/WareHouseItemDTO';
import { WareHouseItemUnitDTO } from '../model/WareHouseItemUnitDTO';
import { WareHouseSearchModel } from '../model/WareHouseSearchModel';

@Injectable({
  providedIn: 'root'
})
export class WareHouseItemService {
  private baseUrl = environment.baseApi+'WareHouseItem';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient) { }

  getById(id:string): Observable<ResultMessageResponse<WareHouseItemDTO>> {
    var url = this.baseUrl + `/get-list?`;
    return this.http.get<ResultMessageResponse<WareHouseItemDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
     
    );
  }

  getListItemUnit(id:string): Observable<ResultMessageResponse<WareHouseItemUnitDTO>> {
    var urlGet=environment.baseApi+'WareHouseItemUnit'
    var url =urlGet + `/get-list?idItem=`+id;
    return this.http.get<ResultMessageResponse<WareHouseItemUnitDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
     
    );
  }

  getList(search: WareHouseSearchModel): Observable<ResultMessageResponse<WareHouseItemDTO>> {
    var check = search.active == null ? '' : search.active;
    var url = this.baseUrl + `/get-list?KeySearch=` + search.keySearch + `&Active=` + check + `&Skip=` + search.skip + `&Take=` + search.take + ``;
    return this.http.get<ResultMessageResponse<WareHouseItemDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
     
    );
  }
  getListDropDown(): Observable<ResultMessageResponse<WareHouseItemDTO>> {
    var url = this.baseUrl + `/get-drop-tree?Active=true`;
    return this.http.get<ResultMessageResponse<WareHouseItemDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
     
    );
  }

  Edit(model: WareHouseItem): Observable<ResultMessageResponse<WareHouseItem>> {
    var url = this.baseUrl + `/edit`;
    return this.http.post<ResultMessageResponse<WareHouseItem>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`edit WareHouses id=${model.id}`)),
     
    );
  }

  EditIndex(id:string): Observable<ResultMessageResponse<WareHouseItemDTO>> {
    var url = this.baseUrl + `/edit?id=`+id;
    return this.http.get<ResultMessageResponse<WareHouseItemDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`edit`)),
     
    );
  }
  AddIndex(): Observable<ResultMessageResponse<WareHouseItemDTO>> {
    var url = this.baseUrl + `/create`;
    return this.http.get<ResultMessageResponse<WareHouseItemDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
     
    );
  }
  Add(model: WareHouseItem): Observable<ResultMessageResponse<WareHouseItem>> {
    var url = this.baseUrl + `/create`;
    return this.http.post<ResultMessageResponse<WareHouseItem>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`create  id=${model.id}`)),
     
    );
  }

 Delete(ids:string[]): Observable<ResultMessageResponse<WareHouseItem>> {
    var url = this.baseUrl + `/delete`;
    return this.http.post<ResultMessageResponse<WareHouseItem>>(url, ids, this.httpOptions).pipe(
      tap(_ => console.log(`delete  id=${ids}`)),
     
    );
  }

 
}