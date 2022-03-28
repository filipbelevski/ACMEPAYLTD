import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, tap } from 'rxjs';
import { CaptureModel, ListResponseDto, TransactionResponse, VoidModel } from './models/models';

@Injectable({
  providedIn: 'root'
})
export class TransactionsService {

  private transactionsUrl: string = 'https://localhost:44307/api/transactions'
  constructor(private http: HttpClient) { }

  private options = {
    headers: new HttpHeaders().append('Content-type', 'application/json').append('Access-Control-Allow-Origin', 'https://localhost:44307')
    };

    getPayments(page: number, pageSize: number, from?: string, to?:string, status?: number, searchString?: string): Observable<ListResponseDto<TransactionResponse>>{
      let url = this.transactionsUrl;

      url += `?page=${page}&pageSize=${pageSize}`;

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

    voidPayment(paymentId:string, orderReference:string):Observable<VoidModel>{
      let url = `${this.transactionsUrl}/${paymentId}/voids`;

      return this.http.post(url, orderReference).pipe<VoidModel>(tap((response: any) => {
        return response;
      }));
    }

    capturePayment(paymentId:string, orderReference:string){
      let url = `${this.transactionsUrl}/${paymentId}/capture`;


      return this.http.post(url, orderReference).pipe<CaptureModel>(tap((response: any) => {
        return response;
      }));
    }
}
