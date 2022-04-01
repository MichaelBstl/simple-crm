import { Component, OnInit, ViewChild } from '@angular/core';
import { Customer } from '../customer.model';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { CustomerService } from '../customer.service';
import { Observable } from 'rxjs';


@Component({
  selector: 'crm-customer-list-page',
  templateUrl: './customer-list-page.component.html',
  styleUrls: ['./customer-list-page.component.scss']
})
export class CustomerListPageComponent implements OnInit {

  constructor(private customerService: CustomerService) { }

  customers$!: Observable<Customer[]>;

  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  displayColumns = ['name', 'phoneNumber', 'emailAddress', 'statusCode'];


  ngOnInit(): void {
    this.customers$ = this.customerService.search("SearchString");
  }
}
