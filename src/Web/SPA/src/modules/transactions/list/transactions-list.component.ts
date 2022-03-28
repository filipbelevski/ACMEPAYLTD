import { HttpErrorResponse, HttpParams } from '@angular/common/http';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { take } from 'rxjs/operators';
import { SnackbarService } from 'src/modules/shared/snackbar/snackbar.service';
import { ListResponseDto, TransactionResponse } from '../models/models';
import { TransactionsService } from '../transactions.service';


@Component({
  selector: 'app-transactions-list',
  templateUrl: './transactions-list.component.html',
  styleUrls: ['./transactions-list.component.css']
})
export class TransactionsListComponent implements OnInit, AfterViewInit {

  public transactionResult!: ListResponseDto<TransactionResponse>;

  public page: number = 1;
  public pageSize: number = 5;
  public totalCount!: number;
  public selected?: string;

  public from? : string;
  public to?: string;
  public status?: number;
  public searchString?: string;

  public error?: HttpErrorResponse;

  public dataSource = new MatTableDataSource<TransactionResponse>();
  public displayedColumns = ['paymentId', 'amount', 'currency', 'cardholderNumber', 'createdOn', 'holderName', 'orderReference', 'status', 'void', 'capture'];


  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private transactionService: TransactionsService, private snackBar: SnackbarService) { }

  ngOnInit(): void {
    this.getTransactions();
  };

  ngAfterViewInit(): void {
    this.paginator.length = this.transactionResult.totalCount
    this.dataSource.paginator = this.paginator;
  }


  getTransactions(){
    this.transactionService.getPayments(this.page, this.pageSize, this.from, this.to, this.status, this.searchString)
    .pipe(
      take(1)
    )
    .subscribe({
      next: result => {
        this.transactionResult = result;
        this.dataSource.data = result.list;
        this.totalCount = this.transactionResult.totalCount;
        this.paginator.length = this.transactionResult.totalCount;
      },
      error: error => this.error = error
    })
  }

  voidTransaction(paymentId: string, orderReference: string){
      this.transactionService.voidPayment(paymentId, orderReference).pipe(
        take(1)
      )
      .subscribe({
        next: result => {
          this.dataSource.data.filter(x => x.paymentId === result.id)[0].status = 'Voided';
          this.snackBar.openSnackBar(`Voided transaction with ID:${result.id}`);

      },
        error: error => console.log(error)
      })
  }

  captureTransaction(paymentId: string, orderReference: string){
    this.transactionService.capturePayment(paymentId, orderReference).pipe(
      take(1)
    )
    .subscribe({
      next: result => {
        this.dataSource.data.filter(x => x.paymentId === result.id)[0].status = 'Captured';
        this.snackBar.openSnackBar(`Captured transaction with ID:${result.id}`);

    },
      error: error => console.log(error)
    })
  }

  public search = (value: string) => {
    this.searchString = value;
    this.getTransactions();
  }

  public pageChanged(event: PageEvent){
    this.pageSize = event.pageSize;
    this.page = event.pageIndex + 1;
    this.getTransactions();
  }

  public statusChange(value: any){
    this.status = value;
    this.getTransactions();
  }
}
