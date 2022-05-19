
import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr";  // or from "@microsoft/signalr" if you are using a new library
import { Observable, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthenticationService } from '../extension/Authentication.service';
import { InwardEditComponent } from '../method/edit/InwardEdit/InwardEdit.component';
import { InwarDetailsEditComponent } from '../method/edit/InwarDetailsEdit/InwarDetailsEdit.component';
import { ResultMessageResponse } from '../model/ResultMessageResponse';
import { AuthozireService } from './Authozire.service';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private baseUrl = environment.baseSignalr + "signalr";
  public hubConnection!: signalR.HubConnection;
  changeInward!: ResultMessageResponse<string>;
  private msgSignalrSource = new Subject<ResultMessageResponse<string>>();
  msgReceived$ = this.msgSignalrSource.asObservable();
  //name method call
  public WareHouseBookTrachkingToCLient: string = "WareHouseBookTrachkingToCLient";
  public CreateWareHouseBookTrachking: string = "CreateWareHouseBookTrachkingToCLient";
  public DeleteWareHouseBookTrachking: string = "DeleteWareHouseBookTrachkingToCLient";
  public HistoryTrachking: string = "HistoryTrachkingToCLient";
  public constructor(private auth: AuthenticationService) {

  }

  // connect to hub
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.baseUrl, { accessTokenFactory: () => '' + this.auth.userValue.token + '',withCredentials:false })
      .configureLogging(signalR.LogLevel.Information)
      .withAutomaticReconnect()
      .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }
  public stop() {
    this.hubConnection.stop();
  }
  public off(nameMethod: string) {
    this.hubConnection.off(nameMethod);
  }
  // public WareHouseBookTrachking() {
  //   this.hubConnection.on(this.WareHouseBookTrachkingToCLient, (data: ResultMessageResponse<string>) => {
  //     this.changeInward = data;
  //     this.msgSignalrSource.next(data);
  //   });
  // }

  // send message to server

  public SendWareHouseBookTrachking(id: string) {
    this.hubConnection.send("WareHouseBookTrachking", id);
  }
  public SendCreateWareHouseBookTrachking(type: string) {
    this.hubConnection.send("CreateWareHouseBookTrachking", type);
  }
  public SendDeleteWareHouseBookTrachking(type: string, id: string) {
    this.hubConnection.send("DeleteWareHouseBookTrachking", type, id);
  }
  public SendHistoryTrachking() {
    this.hubConnection.send("HistoryTrachking");
  }
}