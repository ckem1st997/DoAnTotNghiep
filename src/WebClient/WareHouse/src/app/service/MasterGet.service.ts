import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ResultMessageResponse } from '../model/ResultMessageResponse';
import { Observable, retry } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MasterGetService {
  private baseUrl = environment.baseApi+'MasterGet';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient) { }


  AsyncSqlElastic(): Observable <ResultMessageResponse<boolean>> {
    var url = this.baseUrl + `/GetDataWareHouseBook`;
    return this.http.get<ResultMessageResponse<boolean>>(url, this.httpOptions).pipe(
      retry(1),
    );
  }

}
