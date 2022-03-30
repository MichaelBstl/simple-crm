import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerListPageComponent } from './customer-list-page/customer-list-page.component';

import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';

@NgModule({
  declarations: [CustomerListPageComponent],
  imports: [
    CommonModule,
    CustomerRoutingModule,
    MatCardModule,
    MatTableModule,
    MatSortModule
  ]
})
export class CustomerModule { }
