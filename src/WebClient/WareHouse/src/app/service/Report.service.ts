import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, retry } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ReportDetailsDTO } from '../model/ReportDetailsDTO';
import { ReportValueTotalDT0 } from '../model/ReportValueTotalDT0';
import { ResultMessageResponse } from '../model/ResultMessageResponse';
import { TreeView } from '../model/TreeView';

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  private baseUrl = environment.baseApi+"Report";
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient) { }


  getList(wareHouseId:string|null,itemId:string|null,start:string|null,end:string|null,skip:number,take:number): Observable<ResultMessageResponse<ReportValueTotalDT0>> {
    var url = this.baseUrl + `/get-report-total?WareHouseItemId=`+itemId+`&WareHouseId=`+wareHouseId+`&FromDate=`+start+`&ToDate=`+end+`&Skip=`+skip+`&Take=`+take+``;
    return this.http.get<ResultMessageResponse<ReportValueTotalDT0>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
     
    );
  }
  
  getExportTotal(wareHouseId:string|null,itemId:string|null,start:string|null,end:string|null) {
    var url = environment.baseApi + `ExportExcel/export-report-total?WareHouseItemId=`+itemId+`&WareHouseId=`+wareHouseId+`&FromDate=`+start+`&ToDate=`+end+`&Skip=0&Take=1&Excel=true`;
    window.location.href=url;
  }
  getExportDetails(key:string|null,wareHouseId:string|null,itemId:string|null,start:string|null,end:string|null) {
    var url = environment.baseApi + `ExportExcel/export-report-details?WareHouseItemId=`+itemId+`&WareHouseId=`+wareHouseId+`&FromDate=`+start+`&ToDate=`+end+`&Skip=0&Take=1&Excel=true&KeySearch=`+key+``;
    window.location.href=url;
  }

  getListDetails(key:string|null,wareHouseId:string|null,itemId:string|null,start:string|null,end:string|null,skip:number,take:number): Observable<ResultMessageResponse<ReportDetailsDTO>> {
    var url = this.baseUrl + `/get-report-details?WareHouseItemId=`+itemId+`&WareHouseId=`+wareHouseId+`&FromDate=`+start+`&ToDate=`+end+`&Skip=`+skip+`&Take=`+take+`&KeySearch=`+key+``;
    return this.http.get<ResultMessageResponse<ReportDetailsDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
     
    );
  }
  getTreeView(): Observable<ResultMessageResponse<TreeView>> {

    var url = this.baseUrl + `/get-report-treeview`;
    return this.http.get<ResultMessageResponse<TreeView>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
    );
  }
}
