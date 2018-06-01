import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';

@Injectable()
export class AppInterceptor implements HttpInterceptor {
    public Token:string;

    constructor(private apiService: ApiService)
    {

    }

    public intercept (req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let authReq: HttpRequest<any>;
        this.Token = this.apiService.BearerToken;
        if(req.url.includes('login')
        || req.url.includes('registration'))
        {
            authReq = req;
        }
        else
        {
            authReq = req.clone({
                setHeaders: {
                    Authorization: this.Token
                }
            });
        }
        return next.handle(authReq);
  }
}