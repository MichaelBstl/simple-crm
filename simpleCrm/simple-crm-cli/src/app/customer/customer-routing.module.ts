import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerDetailComponent } from './customer-detail/customer-detail.component';
import { CustomerListPageComponent } from './customer-list-page/customer-list-page.component';
import { CustomerListPageAltComponent } from './customer-list-page-alt/customer-list-page-alt.component';
import { ActivatedRoute } from '@angular/router';

const routes: Routes = [
  {
    path: 'customers',
    pathMatch: 'full',
    component: CustomerListPageComponent
  },
  {
    path: 'altcustomers',
    pathMatch: 'full',
    component: CustomerListPageAltComponent
  },
  {
    path: 'customer/:id',
    pathMatch: 'full',
    component: CustomerDetailComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomerRoutingModule { }
