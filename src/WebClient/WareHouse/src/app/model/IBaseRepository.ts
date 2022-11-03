import { Observable } from "rxjs";
import { ResultDataResponse } from "./ResultDataResponse";
import { ResultMessageResponse } from "./ResultMessageResponse";

export interface IBaseRepository<T> {
    GetAll(): Observable<ResultMessageResponse<T>>;
    CreateIndex(id:string): Observable<ResultDataResponse<T>>;
    UpdateIndex(id: string): Observable<ResultDataResponse<T>>;
    Create(item: T): Observable<ResultDataResponse<boolean>>;
    Update(id: string, item: T): Observable<ResultDataResponse<boolean>>;
    Delete(id: string): Observable<ResultDataResponse<boolean>>;
  }