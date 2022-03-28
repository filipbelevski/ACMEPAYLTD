import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransactionsService } from './transactions.service';
import { TransactionsListComponent } from './list/transactions-list.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { MatTableModule } from '@angular/material/table'
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatPaginatorModule } from '@angular/material/paginator';

@NgModule({
  declarations: [
    TransactionsListComponent,
  ],
  imports: [
    CommonModule,
    MatTableModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    NgxPaginationModule,
    MatSnackBarModule,
    MatPaginatorModule
  ],
  providers: [TransactionsService]
})
export class TransactionsModule { }
