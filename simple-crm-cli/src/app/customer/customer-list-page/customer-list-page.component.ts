import { Component, OnInit, ViewChild } from '@angular/core';
import { Customer } from '../customer.model';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { CustomerService } from '../customer.service';
import { Observable } from 'rxjs';
import { CustomerCreateDialogComponent } from '../customer-create-dialog/customer-create-dialog.component';
import { Router } from '@angular/router';
import {MatDialog} from '@angular/material/dialog';
import { MatInputModule} from '@angular/material/input';
@Component({
  selector: 'crm-customer-list-page',
  templateUrl: './customer-list-page.component.html',
  styleUrls: ['./customer-list-page.component.scss']
})
export class CustomerListPageComponent implements OnInit {

  constructor(
    private customerService: CustomerService,
    private router: Router,
    public dialog: MatDialog
    ) { }

  customers$!: Observable<Customer[]>;

  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  displayColumns = ['name', 'phoneNumber', 'emailAddress', 'statusCode', 'action'];


  ngOnInit(): void {
    this.customers$ = this.customerService.search("SearchString");
  }

  addCustomer(): void {
    const dialogRef = this.dialog.open(CustomerCreateDialogComponent, {
      width: '250px',
      data: null
    });
  }
  goToDetails(customer: Customer): void {
    this.router.navigate([`./customer/${customer.customerId}`]);
  }
}
