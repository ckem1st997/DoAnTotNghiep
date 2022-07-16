import { Injectable } from "@angular/core";
import { HttpClient, HttpRequest } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
//https://github.com/abdulrafay02/angular-speed-test/blob/master/src/app/file-downloader.service.ts
export class FileDownloaderService {
  url: string = "assets/Upload/SpeedTest_32MB.dat";

  constructor(private http: HttpClient) {}

  download() {
    const req = new HttpRequest("GET", this.url, {
      responseType: "blob",
      reportProgress: true
    });
    return this.http.request(req);
  }
}
