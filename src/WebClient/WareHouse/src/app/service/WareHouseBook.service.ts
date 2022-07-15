import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, retry, catchError, tap, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { WareHouseLimit } from '../entity/WareHouseLimit';
import { InwardDetailDTO } from '../model/InwardDetailDTO';
import { ResultDataResponse } from '../model/ResultDataResponse';
import { ResultMessageResponse } from '../model/ResultMessageResponse';
import { UnitDTO } from '../model/UnitDTO';
import { WareHouseBookSearchModel } from '../model/WareHouseBookSearchModel';
import { WareHouseBookDTO } from './../model/WareHouseBookDTO';
import { InwardDTO } from 'src/app/model/InwardDTO';
import { OutwardDetailDTO } from '../model/OutwardDetailDTO';
import { InwardDetail } from '../entity/InwardDetail';
import { OutwardDetail } from '../entity/OutwardDetail';
import { DeleteWareHouseBook } from '../model/DeleteWareHouseBook';
import { GetDataWareHouseBookBaseDTO } from '../model/GetDataWareHouseBookBaseDTO';

@Injectable({
  providedIn: 'root'
})
export class WareHouseBookService {
  private baseUrl = environment.baseApi + 'WareHouseBook';
  private baseUrlInward = environment.baseApi + 'Inward';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient) { }


  // getById(id:string): Observable<ResultMessageResponse<WareHouseLimitDTO>> {
  //   var url = this.baseUrl + `/get-list?`;
  //   return this.http.get<ResultMessageResponse<WareHouseLimitDTO>>(url, this.httpOptions).pipe(
  //     retry(1), // retry a failed request up to 3 times
  //    
  //   );
  // }


  getList(search: WareHouseBookSearchModel): Observable<ResultMessageResponse<WareHouseBookDTO>> {
    var TypeWareHouseBook = search.typeWareHouseBook == null ? '' : search.typeWareHouseBook;
    var FromDate = search.fromDate == null ? '' : search.fromDate;
    var ToDate = search.toDate == null ? '' : search.toDate;
    var wareHouseId = search.wareHouseId == null ? '' : search.wareHouseId;
    var url = this.baseUrl + `/get-list?KeySearch=` + search.keySearch + `&TypeWareHouseBook=` + TypeWareHouseBook + `&Skip=` + search.skip + `&Take=` + search.take + `&FromDate=` + FromDate + `&ToDate=` + ToDate + `&WareHouseId=` + wareHouseId + ``;
    return this.http.get<ResultMessageResponse<WareHouseBookDTO>>(url, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times

    );
  }
  GetUnitByIdItem(id: string): Observable<ResultMessageResponse<UnitDTO>> {
    var url = this.baseUrl + `/get-unit-by-id?IdItem=` + id;
    return this.http.get<ResultMessageResponse<UnitDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),

    );
  }

  CheckQuantityIdItem(itemId: string,wareHouseId:string,unitId:string): Observable<ResultMessageResponse<UnitDTO>> {
    var url = this.baseUrl + `/check-ui-quantity?WareHouseId=`+wareHouseId+`&ItemId=`+itemId+`&UnitId=`+unitId+``;
    return this.http.get<ResultMessageResponse<UnitDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),

    );
  }
  // Edit(model: WareHouseLimit): Observable<ResultMessageResponse<WareHouseLimit>> {
  //   var url = this.baseUrl + `/edit`;
  //   return this.http.post<ResultMessageResponse<WareHouseLimit>>(url, model, this.httpOptions).pipe(
  //     tap(_ => console.log(`edit WareHouses id=${model.id}`)),
  //    
  //   );
  // }

  EditInwardIndex(id: string): Observable<ResultDataResponse<InwardDTO>> {
    var url = this.baseUrlInward + `/edit?id=` + id;
    return this.http.get<ResultDataResponse<InwardDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`edit`)),

    );
  }
  AddInwarDetailsIndex(): Observable<ResultDataResponse<InwardDetailDTO>> {
    var url = this.baseUrl + `/create-inward-details`;
    return this.http.get<ResultDataResponse<InwardDetailDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),

    );
  }
  AddInwarDetails(model:InwardDetail): Observable<ResultDataResponse<InwardDetailDTO>> {
    var url = this.baseUrl + `/create-inward-details`;
    return this.http.post<ResultDataResponse<InwardDetailDTO>>(url,model, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),

    );
  }
  AddOutwarDetails(model:InwardDetail): Observable<ResultDataResponse<OutwardDetailDTO>> {
    var url = this.baseUrl + `/create-OUTward-details`;
    return this.http.post<ResultDataResponse<OutwardDetailDTO>>(url,model, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),

    );
  }
  EditInwarDetailsIndex(id: string): Observable<ResultDataResponse<InwardDetailDTO>> {
    var url = this.baseUrl + `/edit-inward-details?id=` + id;
    return this.http.get<ResultDataResponse<InwardDetailDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),

    );
  }
  EditOutwarDetailsIndex(id: string): Observable<ResultDataResponse<OutwardDetailDTO>> {
    var url = this.baseUrl + `/edit-outward-details?id=` + id;
    return this.http.get<ResultDataResponse<OutwardDetailDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),

    );
  }
  InwarDetails(id: string): Observable<ResultDataResponse<InwardDetailDTO>> {
    var url = this.baseUrl + `/details-inward-details?id=` + id;
    return this.http.get<ResultDataResponse<InwardDetailDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),

    );
  }
  OutwarDetails(id: string): Observable<ResultDataResponse<OutwardDetailDTO>> {
    var url = this.baseUrl + `/details-outward-details?id=` + id;
    return this.http.get<ResultDataResponse<OutwardDetailDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),

    );
  }
  EditInwarDetailsIndexByModel(model: InwardDetail): Observable<ResultDataResponse<InwardDetail>> {
    var url = this.baseUrl + `/edit-inward-details`;
    return this.http.post<ResultDataResponse<InwardDetail>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),

    );
  }
  EditOutwarDetailsIndexByModel(model: OutwardDetail): Observable<ResultDataResponse<OutwardDetail>> {
    var url = this.baseUrl + `/edit-outward-details`;
    return this.http.post<ResultDataResponse<OutwardDetail>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),

    );
  }
  AddOutwarDetailsIndex(): Observable<ResultDataResponse<OutwardDetailDTO>> {
    var url = this.baseUrl + `/create-outward-details`;
    return this.http.get<ResultDataResponse<OutwardDetailDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
    );
  }


  GetDataToWareHouseBookIndex(): Observable<ResultDataResponse<GetDataWareHouseBookBaseDTO>> {
    var url = this.baseUrl + `/get-data-to-warehouse-book`;
    return this.http.get<ResultDataResponse<GetDataWareHouseBookBaseDTO>>(url, this.httpOptions).pipe(
      tap(_ => console.log(`create`)),
      
    );
  }
  DeleteInwarDetails(ids:string[]): Observable<ResultMessageResponse<WareHouseBookDTO>> {
    var url = this.baseUrl + `/delete-details-inward`;
    return this.http.post<ResultMessageResponse<WareHouseBookDTO>>(url, ids, this.httpOptions).pipe(
      tap(_ => console.log(`delete  id=${ids}`)),

    );
  }

  DeleteOutwarDetails(ids:string[]): Observable<ResultMessageResponse<WareHouseBookDTO>> {
    var url = this.baseUrl + `/delete-details-outward`;
    return this.http.post<ResultMessageResponse<WareHouseBookDTO>>(url, ids, this.httpOptions).pipe(
      tap(_ => console.log(`delete  id=${ids}`)),

    );
  }
  Delete(ids:DeleteWareHouseBook): Observable<ResultMessageResponse<WareHouseLimit>> {
    var url = this.baseUrl + `/delete`;
    return this.http.post<ResultMessageResponse<WareHouseLimit>>(url, ids, this.httpOptions).pipe(
      tap(_ => console.log(`delete  id=${ids}`)),

    );
  }


}

