import { Component, OnInit, ViewChild } from '@angular/core';
import { Customer } from '../customer.model';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { CustomerService } from '../customer.service';
import { combineLatest, debounce, Observable } from 'rxjs';
import { debounceTime, map, startWith, switchMap } from 'rxjs/operators';
import { CustomerCreateDialogComponent } from '../customer-create-dialog/customer-create-dialog.component';
import { Router } from '@angular/router';
import {MatDialog} from '@angular/material/dialog';
import { MatInputModule} from '@angular/material/input';
import { FormControl } from '@angular/forms';
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
    ) {
      this.filteredCustomers$ = this.filterInput.valueChanges.pipe(
        startWith(''),
        debounceTime(700),
        switchMap((filterTerm: string) => {
          return this.customerService.search(filterTerm);
        } )
      )
    }

  filteredCustomers$!: Observable<Customer[]>;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  displayColumns = ['name', 'phoneNumber', 'emailAddress', 'statusCode', 'action', 'lastContactDate'];
  filterInput = new FormControl();


  ngOnInit(): void {
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
