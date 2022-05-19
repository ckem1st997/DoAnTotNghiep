import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { catchError, retry, tap } from 'rxjs/operators';
import { VendorDTO } from './../model/VendorDTO';
import { VendorSearchModel } from './../model/VendorSearchModel';
import { BaseSearchModel } from './../model/BaseSearchModel';
import { ResultMessageResponse } from '../model/ResultMessageResponse';
import { environment } from './../../environments/environment';
import { TreeView } from '../model/TreeView';
import { Vendor } from '../entity/Vendor';
@Injectable({
  providedIn: 'root'
})
export class VendorService {
  private baseUrl = environment.baseApi;
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient) { }

  private GetParams(model: BaseSearchModel): HttpParams {

    var param = new HttpParams();
    param.append("KeySearch", model.keySearch);
    if (model.active != null)
      param.append("Active", model.active);
    param.append("Skip", model.skip);
    param.append("Take", model.take);
    return param;
  }

  getList(search: VendorSearchModel): Observable<ResultMessageResponse<VendorDTO>> {
    var param = this.GetParams(search);
    var check = search.active == null ? '' : search.active;
    var url = this.baseUrl + `Vendor/get-list?KeySearch=` + search.keySearch + `&Active=` + check + `&Skip=` + search.skip + `&Take=` + search.take + ``;
    return this.http.get<ResultMessageResponse<VendorDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
     
    );
  }


  Details(id:string): Observable<ResultMessageResponse<VendorDTO>> {
    var url = this.baseUrl + `Vendor/details?id=` + id;
    return this.http.get<ResultMessageResponse<VendorDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`details Vendor id=${id}`)),
     
    );
  }

  EditIndex(id:string): Observable<ResultMessageResponse<VendorDTO>> {
    var url = this.baseUrl + `Vendor/edit?id=` + id;
    return this.http.get<ResultMessageResponse<VendorDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`edit Vendor id=${id}`)),
     
    );
  }

  AddIndex(): Observable<ResultMessageResponse<VendorDTO>> {
    var url = this.baseUrl + `Vendor/create`;
    return this.http.get<ResultMessageResponse<VendorDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`add Vendor`)),
     
    );
  }



  EditVendor(model: Vendor): Observable<ResultMessageResponse<Vendor>> {
    var url = this.baseUrl + `Vendor/edit`;
    return this.http.post<ResultMessageResponse<Vendor>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`edit vendor id=${model.id}`)),
     
    );
  }

  AddVendor(model: Vendor): Observable<ResultMessageResponse<Vendor>> {
    var url = this.baseUrl + `Vendor/create`;
    return this.http.post<ResultMessageResponse<Vendor>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`create vendor id=${model.id}`)),
     
    );
  }

 DeleteVendor(ids:string[]): Observable<ResultMessageResponse<Vendor>> {
    var url = this.baseUrl + `Vendor/delete`;
    return this.http.post<ResultMessageResponse<Vendor>>(url, ids, this.httpOptions).pipe(
      tap(_ => console.log(`delete vendor id=${ids}`)),
     
    );
  }

  getTreeView(): Observable<ResultMessageResponse<TreeView>> {

    var url = this.baseUrl + `WareHouses/get-tree-view?Active=true`;
    return this.http.get<ResultMessageResponse<TreeView>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
     
    );
  }
  
}