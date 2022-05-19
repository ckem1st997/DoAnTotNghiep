import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NotifierService } from 'angular-notifier';
import { Observable, retry, catchError, tap, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Inward } from '../entity/Inward';
import { InwardEvent } from '../entity/InwardEvent';
import { InwardDTO } from '../model/InwardDTO';
import { ResultDataResponse } from '../model/ResultDataResponse';
import { ResultMessageResponse } from '../model/ResultMessageResponse';

@Injectable({
  providedIn: 'root'
})
export class KafKaService {
  private baseUrl = environment.authorizeApi + 'CallKafKa';
  private readonly notifier!: NotifierService;
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient, notifierService: NotifierService) {
    this.notifier = notifierService;
  }

  Add(model: Inward): Observable<ResultMessageResponse<Inward>> {
    var url = this.baseUrl + `/create-inward`;
    return this.http.post<ResultMessageResponse<InwardEvent>>(url, model, this.httpOptions).pipe(
      tap(_ => console.log(`create  id=${model.id}`)),
    );
  }

}