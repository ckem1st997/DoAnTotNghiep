import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NotifierService } from 'angular-notifier';
import { Observable, retry } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DashBoardSelectTopInAndOutDTO } from '../model/DashBoardSelectTopInAndOutDTO';
import { ResultMessageResponse } from '../model/ResultMessageResponse';

@Injectable({
  providedIn: 'root'
})
export class CheckRoleService {

  private baseUrlMaster = environment.authorizeApi + 'Index/role';
  private baseUrlWareHouse = environment.baseApi + 'Index/role';
  private readonly notifier!: NotifierService;
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient, notifierService: NotifierService) {
    this.notifier = notifierService;
  }


  CheckAuthozireMaster(){
    return this.http.get(this.baseUrlMaster, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
    );
  }
  CheckAuthozireWareHouse(){
    return this.http.get(this.baseUrlWareHouse, this.httpOptions).pipe(
      retry(1), // retry a failed request up to 3 times
    );
  }
}