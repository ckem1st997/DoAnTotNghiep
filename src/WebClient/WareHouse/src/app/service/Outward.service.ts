import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, retry, catchError, tap, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Outward } from '../entity/Outward';
import { OutwardDTO } from '../model/OutwardDTO';
import { ResultDataResponse } from '../model/ResultDataResponse';
import { ResultMessageResponse } from '../model/ResultMessageResponse';

@Injectable({
  providedIn: 'root'
})
export class OutwardService {
  private baseUrl = environment.baseApi+'Outward';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient) { }


  getById(id:string): Observable<ResultMessageResponse<OutwardDTO>> {
    var url = this.baseUrl + `/get-list?`;
    return this.http.get<ResultMessageResponse<OutwardDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
     
    );
  }



  Edit(model: Outward): Observable<ResultMessageResponse<Outward>> {
    var url = this.baseUrl + `/edit`;
    return this.http.post<ResultMessageResponse<Outward>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`edit WareHouses id=${model.id}`)),
     
    );
  }

  Details(id: string): Observable<ResultDataResponse<OutwardDTO>> {
    var url = this.baseUrl + `/details?id=` + id;
    return this.http.get<ResultDataResponse<OutwardDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`edit`)),
    );
  }
  EditIndex(id:string): Observable<ResultDataResponse<OutwardDTO>> {
    var url = this.baseUrl + `/edit?id=`+id;
    return this.http.get<ResultDataResponse<OutwardDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`edit`)),
     
    );
  }
  AddIndex(idwh:string|null): Observable<ResultDataResponse<OutwardDTO>> {
    var url = this.baseUrl + `/create?idWareHouse=`+idwh;
    return this.http.get<ResultDataResponse<OutwardDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
     
    );
  }
  Add(model: Outward): Observable<ResultMessageResponse<Outward>> {
    var url = this.baseUrl + `/create`;
    return this.http.post<ResultMessageResponse<Outward>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`create  id=${model.id}`)),
     
    );
  }

 Delete(ids:string[]): Observable<ResultMessageResponse<Outward>> {
    var url = this.baseUrl + `/delete`;
    return this.http.post<ResultMessageResponse<Outward>>(url, ids, this.httpOptions).pipe(
      tap(_ => console.log(`delete  id=${ids}`)),
     
    );
  }

}