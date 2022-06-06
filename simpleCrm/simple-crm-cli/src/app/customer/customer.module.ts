import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerListPageComponent } from './customer-list-page/customer-list-page.component';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { HttpClientModule } from '@angular/common/http';
import { CustomerService } from './customer.service';
import { environment } from 'src/environments/environment';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatIconModule} from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CustomerCreateDialogComponent } from './customer-create-dialog/customer-create-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { CustomerDetailComponent } from './customer-detail/customer-detail.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { StatusIconPipe } from './status-icon.pipe';
import { CustomerListPageAltComponent } from './customer-list-page-alt/customer-list-page-alt.component';
import { MatListModule } from '@angular/material/list';
import { AccountModule } from '../account/account.module';
@NgModule({
  declarations: [
    CustomerListPageComponent,
    CustomerListPageAltComponent,
    CustomerCreateDialogComponent,
    CustomerDetailComponent,
    StatusIconPipe
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    MatTableModule,
    MatCardModule,
    MatSortModule,
    CustomerRoutingModule,
    FlexLayoutModule,
    MatIconModule,
    MatButtonModule,
    MatDialogModule,
    MatInputModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatSnackBarModule,
    MatListModule,
    AccountModule
  ],
  providers: [
    {
      provide: CustomerService,
      useClass: CustomerService
    }
  ]
})
export class CustomerModule { }
