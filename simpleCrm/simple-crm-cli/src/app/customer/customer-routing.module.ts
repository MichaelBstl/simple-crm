import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerDetailComponent } from './customer-detail/customer-detail.component';
import { CustomerListPageComponent } from './customer-list-page/customer-list-page.component';
import { CustomerListPageAltComponent } from './customer-list-page-alt/customer-list-page-alt.component';
import { ActivatedRoute } from '@angular/router';
import { AuthenticatedGuard } from '../account/authenticated.guard';

const routes: Routes = [
  {
    path: 'customers',
    pathMatch: 'full',
    component: CustomerListPageComponent,
    canActivate: [AuthenticatedGuard]
  },
  {
    path: 'altcustomers',
    pathMatch: 'full',
    component: CustomerListPageAltComponent,
    canActivate: [AuthenticatedGuard]
  },
  {
    path: 'customer/:id',
    pathMatch: 'full',
    component: CustomerDetailComponent,
    canActivate: [AuthenticatedGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomerRoutingModule { }
