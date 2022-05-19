import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, retry, catchError, tap, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { WareHouseItemUnit } from '../entity/WareHouseItemUnit';
import { ResultMessageResponse } from '../model/ResultMessageResponse';
import { WareHouseItemCategorySearchModel } from '../model/WareHouseItemCategorySearchModel';
import { WareHouseItemUnitDTO } from '../model/WareHouseItemUnitDTO';

@Injectable({
  providedIn: 'root'
})
export class WareHouseItemUnitService {
  private baseUrl = environment.baseApi+'WareHouseItemUnit';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient) { }


  getById(id:string): Observable<ResultMessageResponse<WareHouseItemUnitDTO>> {
    var url = this.baseUrl + `/get-list?`;
    return this.http.get<ResultMessageResponse<WareHouseItemUnitDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
     
    );
  }

  getList(search: WareHouseItemCategorySearchModel): Observable<ResultMessageResponse<WareHouseItemUnitDTO>> {
    var check = search.active == null ? '' : search.active;
    var url = this.baseUrl + `/get-list?KeySearch=` + search.keySearch + `&Active=` + check + `&Skip=` + search.skip + `&Take=` + search.take + ``;
    return this.http.get<ResultMessageResponse<WareHouseItemUnitDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
     
    );
  }
  getListDropDown(): Observable<ResultMessageResponse<WareHouseItemUnitDTO>> {
    var url = this.baseUrl + `/get-drop-tree?Active=true`;
    return this.http.get<ResultMessageResponse<WareHouseItemUnitDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
     
    );
  }


  getCheckItemUnit(): Observable<ResultMessageResponse<WareHouseItemUnitDTO>> {
    var url = this.baseUrl + `/get-drop-tree?Active=true`;
    return this.http.get<ResultMessageResponse<WareHouseItemUnitDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
     
    );
  }

  Edit(model: WareHouseItemUnit): Observable<ResultMessageResponse<WareHouseItemUnit>> {
    var url = this.baseUrl + `/edit`;
    return this.http.post<ResultMessageResponse<WareHouseItemUnit>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`edit WareHouses id=${model.id}`)),
     
    );
  }

  Add(model: WareHouseItemUnit): Observable<ResultMessageResponse<WareHouseItemUnit>> {
    var url = this.baseUrl + `/create`;
    return this.http.post<ResultMessageResponse<WareHouseItemUnit>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`create id=${model.id}`)),
     
    );
  }

 Delete(ids:string[]): Observable<ResultMessageResponse<WareHouseItemUnit>> {
    var url = this.baseUrl + `/delete`;
    return this.http.post<ResultMessageResponse<WareHouseItemUnit>>(url, ids, this.httpOptions).pipe(
      tap(_ => console.log(`delete id=${ids}`)),
     
    );
  }

  
}