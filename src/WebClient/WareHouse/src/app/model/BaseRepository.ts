import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, retry } from "rxjs";
import { environment } from "src/environments/environment";
import { IBaseRepository } from "./IBaseRepository";
import { ResultDataResponse } from "./ResultDataResponse";
import { ResultMessageResponse } from "./ResultMessageResponse";

export abstract class BaseRepository<T> implements IBaseRepository<T> {
    private nameApi: string = "";
    private baseUrlMaster = environment.authorizeApi + this.nameApi;
    httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    constructor(private http: HttpClient, name: string) {
        this.nameApi = name;
    }

    GetAll(): Observable<ResultMessageResponse<T>> {
        var url = this.baseUrlMaster + `/get-list`;
        return this.http.get<ResultMessageResponse<T>>(url, this.httpOptions).pipe(
            retry(1), // retry a failed request up to 3 times
        );
    }
    CreateIndex(id: string): Observable<ResultDataResponse<T>> {
        throw new Error("Method not implemented.");
    }
    UpdateIndex(id: string): Observable<ResultDataResponse<T>> {
        throw new Error("Method not implemented.");
    }
    Create(item: T): Observable<ResultDataResponse<boolean>> {
        throw new Error("Method not implemented.");
    }
    Update(id: string, item: T): Observable<ResultDataResponse<boolean>> {
        throw new Error("Method not implemented.");
    }
    Delete(id: string): Observable<ResultDataResponse<boolean>> {
        throw new Error("Method not implemented.");
    }

}