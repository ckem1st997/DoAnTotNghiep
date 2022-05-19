import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, retry, catchError, tap, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BeginningWareHouse } from '../entity/BeginningWareHouse';
import { BeginningWareHouseDTO } from '../model/BeginningWareHouseDTO';
import { BeginningWareHouseSearchModel } from '../model/BeginningWareHouseSearchModel';
import { ResultMessageResponse } from '../model/ResultMessageResponse';
import { WareHouseItemDTO } from '../model/WareHouseItemDTO';
import { WareHouseSearchModel } from '../model/WareHouseSearchModel';

@Injectable({
  providedIn: 'root'
})

export class BeginningWareHouseService {
  private baseUrl = environment.baseApi+'BeginningWareHouse';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient) { }


  getById(id:string): Observable<ResultMessageResponse<BeginningWareHouseDTO>> {
    var url = this.baseUrl + `/get-list?`;
    return this.http.get<ResultMessageResponse<BeginningWareHouseDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
     
    );
  }


  getList(search: BeginningWareHouseSearchModel): Observable<ResultMessageResponse<BeginningWareHouseDTO>> {
    var check = search.active == null ? '' : search.active;
    var wareHouseId = search.wareHouseId == null ? '' : search.wareHouseId;
    var url = this.baseUrl + `/get-list?KeySearch=` + search.keySearch + `&Active=` + check + `&Skip=` + search.skip + `&Take=` + search.take + `&WareHouseId=`+wareHouseId+``;
    return this.http.get<ResultMessageResponse<BeginningWareHouseDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
    );
  }
  getListDropDown(): Observable<ResultMessageResponse<BeginningWareHouseDTO>> {
    var url = this.baseUrl + `/get-drop-tree?Active=true`;
    return this.http.get<ResultMessageResponse<BeginningWareHouseDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
     
    );
  }

  Edit(model: BeginningWareHouse): Observable<ResultMessageResponse<BeginningWareHouse>> {
    var url = this.baseUrl + `/edit`;
    return this.http.post<ResultMessageResponse<BeginningWareHouse>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`edit WareHouses id=${model.id}`)),
     
    );
  }

  EditIndex(id:string): Observable<ResultMessageResponse<BeginningWareHouseDTO>> {
    var url = this.baseUrl + `/edit?id=`+id;
    return this.http.get<ResultMessageResponse<BeginningWareHouseDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`edit`)),
     
    );
  }
  AddIndex(id:string): Observable<ResultMessageResponse<BeginningWareHouseDTO>> {
    var url = this.baseUrl + `/create?idWareHouse=`+id;
    return this.http.get<ResultMessageResponse<BeginningWareHouseDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
     
    );
  }
  Add(model: BeginningWareHouse): Observable<ResultMessageResponse<BeginningWareHouse>> {
    var url = this.baseUrl + `/create`;
    return this.http.post<ResultMessageResponse<BeginningWareHouse>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`create  id=${model.id}`)),
     
    );
  }

 Delete(ids:string[]): Observable<ResultMessageResponse<BeginningWareHouse>> {
    var url = this.baseUrl + `/delete`;
    return this.http.post<ResultMessageResponse<BeginningWareHouse>>(url, ids, this.httpOptions).pipe(
      tap(_ => console.log(`delete  id=${ids}`)),
     
    );
  }

}