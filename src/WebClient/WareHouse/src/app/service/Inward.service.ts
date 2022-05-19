import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NotifierService } from 'angular-notifier';
import { Observable, retry, catchError, tap, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Inward } from '../entity/Inward';
import { InwardDTO } from '../model/InwardDTO';
import { ResultDataResponse } from '../model/ResultDataResponse';
import { ResultMessageResponse } from '../model/ResultMessageResponse';

@Injectable({
  providedIn: 'root'
})
export class InwardService {
  private baseUrl = environment.baseApi + 'Inward';
  private readonly notifier!: NotifierService;
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient, notifierService: NotifierService) {
    this.notifier = notifierService;
  }


  getById(id: string): Observable<ResultMessageResponse<InwardDTO>> {
    var url = this.baseUrl + `/get-list?`;
    return this.http.get<ResultMessageResponse<InwardDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
    );
  }
  ExportExcel(id: string){
    var url = environment.baseApi + `ExportExcel/export-in-ward?id=`+id+``;
    return this.http.get(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
    );
  }

  CheckItemExist(itemId: string,wareHouseId:string): Observable<ResultMessageResponse<boolean>> {
    var url = this.baseUrl + `/check-item-exist?itemId=`+itemId+`&warehouseId=`+wareHouseId+``;
    return this.http.get<ResultMessageResponse<boolean>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
    );
  }

  Edit(model: Inward): Observable<ResultMessageResponse<Inward>> {
    var url = this.baseUrl + `/edit`;
    return this.http.post<ResultMessageResponse<Inward>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`edit WareHouses id=${model.id}`)),
    );
  }

  EditIndex(id: string): Observable<ResultDataResponse<InwardDTO>> {
    var url = this.baseUrl + `/edit?id=` + id;
    return this.http.get<ResultDataResponse<InwardDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`edit`)),
    );
  }

  Details(id: string): Observable<ResultDataResponse<InwardDTO>> {
    var url = this.baseUrl + `/details?id=` + id;
    return this.http.get<ResultDataResponse<InwardDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`edit`)),
    );
  }

  AddIndex(idwh: string | null): Observable<ResultDataResponse<InwardDTO>> {
    var url = this.baseUrl + `/create?idWareHouse=` + idwh;
    return this.http.get<ResultDataResponse<InwardDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
    );
  }
  Add(model: Inward): Observable<ResultMessageResponse<Inward>> {
    var url = this.baseUrl + `/create`;
    return this.http.post<ResultMessageResponse<Inward>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`create  id=${model.id}`)),
    );
  }

  Delete(ids: string[]): Observable<ResultMessageResponse<Inward>> {
    var url = this.baseUrl + `/delete`;
    return this.http.post<ResultMessageResponse<Inward>>(url, ids, this.httpOptions).pipe(
      tap(_ => console.log(`delete  id=${ids}`)),
    );
  }
}