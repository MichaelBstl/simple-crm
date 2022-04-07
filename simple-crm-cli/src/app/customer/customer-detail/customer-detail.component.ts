import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Customer } from '../customer.model';
import { CustomerService } from '../customer.service';
import { Observable, of } from 'rxjs';
@Component({
  selector: 'crm-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.scss']
})
export class CustomerDetailComponent implements OnInit {
  customerId!: number;
  customer$!: Customer;
  constructor(
    private route: ActivatedRoute,
    private customerService: CustomerService) {
   }

  ngOnInit(): void {
    this.customerId = +this.route.snapshot.params['id'];

    this.customerService //injected
    .get(this.customerId)
    .subscribe(cust => {  // like listening to a JavaScript fetch call to return
       if (cust) {
         this.customer$ = cust;
       }
    });  }
}
