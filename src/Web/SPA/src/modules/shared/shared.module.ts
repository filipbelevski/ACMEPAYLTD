import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SnackbarService } from './snackbar/snackbar.service';

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule
  ],
  providers:[SnackbarService]
})
export class SharedModule { }
