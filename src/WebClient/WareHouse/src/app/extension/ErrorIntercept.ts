import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, retry, catchError, throwError, map, takeUntil, finalize, tap } from "rxjs";
import { LoadingService } from "../service/Loading.service";
import { AuthenticationService } from 'src/app/extension/Authentication.service';
import { ActivationEnd, Router } from "@angular/router";
import { NotifierService } from "angular-notifier";
import { MatDialog } from "@angular/material/dialog";
import { HttpCancelService } from "./HttpCancel.service";
/// bắt lỗi toàn client
@Injectable()
export class ErrorIntercept implements HttpInterceptor {
    constructor(
        private _loading: LoadingService,
        private service: AuthenticationService,
        private router: Router,
        private notife: NotifierService,
        private dialogRef: MatDialog,
        private httpCancelService: HttpCancelService
    ) {
        router.events.subscribe(event => {
            //An event triggered at the end of the activation part of the Resolve phase of routing.
            // if (event instanceof ActivationEnd) {
            //   // Cancel pending calls
            //   this.httpCancelService.cancelPendingRequests();
            // }
            // if (event instanceof NavigationStart ) {
            //    console
            //   // Cancel pending calls
            //   this.httpCancelService.cancelPendingRequests();
            // }
        });
    }
    intercept(
        request: HttpRequest<any>,
        next: HttpHandler
    ): Observable<HttpEvent<any>> {
        const begin = performance.now();
        this._loading.setLoading(true, request.url);
        if (this.service.userCheck)
            request = request.clone({
                setHeaders: { Authorization: `Bearer ${this.service.userValue.token}` }
            });
        // get timestamp
        const startTimestamp = new Date().getTime();

        const newReq = request.clone({
            headers: request.headers.set('startTimestamp', startTimestamp.toString())
        });
        return next.handle(request)
            .pipe(
                tap(event => {
                    // another timestamp
                    const endTimestamp: number = new Date().getTime();
                    const startTimestamp2: number = Number(
                        request.headers.get('startTimestamp')
                    );

                    // get the difference
                    const responseTimes = endTimestamp - startTimestamp2;
                    console.log("responseTimes:"+responseTimes);

                }),
                retry(1),
                catchError((error: HttpErrorResponse) => {
                    if (error.status === 0) {
                        this.notife.notify('error', "Không thể kết nối đến máy chủ, xin vui lòng thử lại sau ít phút !");

                    }
                    else if (error.status === 401) {
                        this.router.navigate(['/authozire/login']);
                        if (error.error !== null && error.error.message !== undefined)
                            this.notife.notify('error', error.error.message);

                    }
                    else if (error.status === 403) {
                        if (error.error !== null && error.error.message !== undefined) {
                            this.notife.notify('error', error.error.message);
                            if (error.error !== null && error.error.isRedirect)
                                this.router.navigate(['/403']);
                        }
                        else
                            this.router.navigate(['/403']);
                    }
                    else {

                        this.notife.notify('error', "Có lỗi xảy ra, xin vui lòng thử lại sau ít phút !");
                    }
                    this._loading.setLoading(false, request.url);
                    this.dialogRef.closeAll();

                    // Return an observable with a user-facing error message.
                    return throwError(error);
                }),
                finalize(() => {
                    this.logRequestTime(begin, request.url, request.method);
                })
            ).pipe(map<HttpEvent<any>, any>((evt: HttpEvent<any>) => {

                if (evt instanceof HttpResponse) {
                    // xử lí lỗi trả về từ api mà không cần xử lí trong  compent
                    if (evt.status === 200 && !evt.body.success) {
                        if (evt.body.errors.msg !== undefined && evt.body.errors.msg.length > 0) {
                            this.notife.notify('error', evt.body.errors.msg[0]);
                        }
                        else
                            this.notife.notify('error', evt.body.message);

                    }
                    this._loading.setLoading(false, request.url);
                }
                return evt;
            }))
            .pipe(takeUntil(this.httpCancelService.onCancelPendingRequests()));

        
    }
    private logRequestTime(startTime: number, url: string, method: string) {
        const requestDuration = `${performance.now() - startTime}`;
        console.log(`HTTP ${method} ${url} - ${requestDuration} milliseconds`);
       // this.notife.notify('info', `${requestDuration} milliseconds`);
        // this.notife.show({
        //     type: 'success',
        //     message: `${requestDuration} milliseconds`,
        //     id: requestDuration, // Again, this is optional
        //   });

    }
}