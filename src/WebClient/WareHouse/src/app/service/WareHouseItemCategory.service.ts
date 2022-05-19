import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, retry, catchError, tap, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { WareHouseItemCategory } from '../entity/WareHouseItemCategory';
import { ResultMessageResponse } from '../model/ResultMessageResponse';
import { TreeView } from '../model/TreeView';
import { WareHouseItemCategoryDTO } from '../model/WareHouseItemCategoryDTO';
import { WareHouseItemCategorySearchModel } from '../model/WareHouseItemCategorySearchModel';

@Injectable({
  providedIn: 'root'
})
export class WareHouseItemCategoryService {
  private baseUrl = environment.baseApi+'WareHouseItemCategory';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient) { }


  getById(id:string): Observable<ResultMessageResponse<WareHouseItemCategoryDTO>> {
    var url = this.baseUrl + `/get-list?`;
    return this.http.get<ResultMessageResponse<WareHouseItemCategoryDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
     
    );
  }

  getList(search: WareHouseItemCategorySearchModel): Observable<ResultMessageResponse<WareHouseItemCategoryDTO>> {
    var check = search.active == null ? '' : search.active;
    var url = this.baseUrl + `/get-list?KeySearch=` + search.keySearch + `&Active=` + check + `&Skip=` + search.skip + `&Take=` + search.take + ``;
    return this.http.get<ResultMessageResponse<WareHouseItemCategoryDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
     
    );
  }
  getListDropDown(): Observable<ResultMessageResponse<WareHouseItemCategoryDTO>> {
    var url = this.baseUrl + `/get-drop-tree?Active=true`;
    return this.http.get<ResultMessageResponse<WareHouseItemCategoryDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
     
    );
  }


  Details(id:string): Observable<ResultMessageResponse<WareHouseItemCategoryDTO>> {
    var url = this.baseUrl + `/details?id=` + id;
    return this.http.get<ResultMessageResponse<WareHouseItemCategoryDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`details WareHouseItemCategoryDTO id=${id}`)),
     
    );
  }

  EditIndex(id:string): Observable<ResultMessageResponse<WareHouseItemCategoryDTO>> {
    var url = this.baseUrl + `/edit?id=` + id;
    return this.http.get<ResultMessageResponse<WareHouseItemCategoryDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`edit WareHouseItemCategoryDTO id=${id}`)),
     
    );
  }

  AddIndex(): Observable<ResultMessageResponse<WareHouseItemCategoryDTO>> {
    var url = this.baseUrl + `/create`;
    return this.http.get<ResultMessageResponse<WareHouseItemCategoryDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`add WareHouseItemCategory`)),
     
    );
  }



  Edit(model: WareHouseItemCategory): Observable<ResultMessageResponse<WareHouseItemCategory>> {
    var url = this.baseUrl + `/edit`;
    return this.http.post<ResultMessageResponse<WareHouseItemCategory>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`edit WareHouseItemCategory id=${model.id}`)),
     
    );
  }

  Add(model: WareHouseItemCategory): Observable<ResultMessageResponse<WareHouseItemCategory>> {
    var url = this.baseUrl + `/create`;
    return this.http.post<ResultMessageResponse<WareHouseItemCategory>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`create WareHouseItemCategory id=${model.id}`)),
     
    );
  }

 Delete(ids:string[]): Observable<ResultMessageResponse<WareHouseItemCategory>> {
    var url = this.baseUrl + `/delete`;
    return this.http.post<ResultMessageResponse<WareHouseItemCategory>>(url, ids, this.httpOptions).pipe(
      tap(_ => console.log(`delete WareHouseItemCategory id=${ids}`)),
     
    );
  }

  getTreeView(): Observable<ResultMessageResponse<TreeView>> {

    var url = this.baseUrl + `/get-tree-view?Active=true`;
    return this.http.get<ResultMessageResponse<TreeView>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
    );
  }

}