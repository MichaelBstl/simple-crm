import { Injectable } from '@angular/core';
import { CustomerService } from './customer.service';

import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Customer } from './customer.model';


@Injectable()
export class CustomerMockService extends CustomerService {
  customers: Customer[] = [];
  lastCustomerId: number;
  index: number = 0;

  constructor(http: HttpClient)  {
    super(http);
    console.warn('Warning: You are using the CustomerMockService, not intended for production use.');
    const localCustomers= localStorage.getItem('customers');
    if (localCustomers) {
      this.customers = JSON.parse(localCustomers);
    } else {
      this.customers = [
        {
          customerId: 1,
          firstName: 'John',
          lastName: 'Smith',
          phoneNumber: '314-555-1234',
          preferredContactMethod: 'email',
          emailAddress: 'xya@123.com',
          statusCode: '',
          lastContactDate: '01/-01/2020'
        }
      ];
      localStorage.setItem('customers', JSON.stringify(this.customers));
    }
    this .lastCustomerId = Math.max(...this.customers.map(x => x.customerId));
  }
  override search(term: string): Observable<Customer[]> {
    return of(this.customers.filter(x => x.firstName || x.lastName == term));
  }
  override insert(customer: Customer): Observable<Customer> {
    customer.customerId = Math.max(...this.customers.map(x => x.customerId)) + 1;
    this.customers.push(customer);
    localStorage.setItem('customers', JSON.stringify(this.customers));
    return of(customer);
  }
  override update(customer: Customer): Observable<Customer> {
    this.index = this.customers.findIndex(x => x.customerId);
    this.customers[this.index] = customer;
    localStorage.setItem('customers', JSON.stringify(this.customers));
    return of(customer);
  }
}
