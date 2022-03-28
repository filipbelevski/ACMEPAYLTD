import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SnackbarService } from './snackbar/snackbar.service';
import { MatDialogModule } from '@angular/material/dialog';




@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule
  ],
  providers:[SnackbarService]
})
export class SharedModule { }
