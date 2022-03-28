import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BrowserDynamicTestingModule } from '@angular/platform-browser-dynamic/testing';
import { map, Observable, tap } from 'rxjs';
import { CaptureModel, ListResponseDto, OrderReferenceBodyModel, TransactionResponse, VoidModel } from './models/models';

@Injectable({
  providedIn: 'root'
})
export class TransactionsService {

  private baseUrl: string = 'https://localhost:44307/api/';
  constructor(private http: HttpClient) { }

  private options = {
    headers: new HttpHeaders().append('Content-type', 'application/json').append('Access-Control-Allow-Origin', 'https://localhost:44307')
    };

    getPayments(page: number, pageSize: number, from?: string, to?:string, status?: number, searchString?: string): Observable<ListResponseDto<TransactionResponse>>{
      let url = this.baseUrl;

      url += `transactions?page=${page}&pageSize=${pageSize}`;

      if(from){
        url += `&from=${from}`
      }
      if(to){
        url += `&to=${to}`
      }
      if(status){
        url += `&status=${status}`
      }

      if(searchString){
        url+= `&searchString=${searchString}`
      }

      return this.http.get<ListResponseDto<TransactionResponse>>(url, this.options);
    }

    voidPayment(paymentId:string, orderReference : string):Observable<VoidModel>{
      let url = `${this.baseUrl}authorize/${paymentId}/voids`;
      let body = {
        orderReference: orderReference
      }

      return this.http.post<VoidModel>(url, body, this.options);
    }

    capturePayment(paymentId:string, orderReference:string){
      let url = `${this.baseUrl}authorize/${paymentId}/capture`;

      let body = {
        orderReference: orderReference
      }

      return this.http.post(url, body).pipe<CaptureModel>(tap((response: any) => {
        return response;
      }));
    }
}
