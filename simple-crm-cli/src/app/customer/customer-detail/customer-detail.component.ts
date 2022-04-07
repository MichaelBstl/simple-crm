import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Customer } from '../customer.model';
import { CustomerService } from '../customer.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'crm-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.scss'],
})
export class CustomerDetailComponent implements OnInit {
  customerId!: number;
  customer!: Customer;
  detailForm!: FormGroup;
  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private customerService: CustomerService,
    private snackBar: MatSnackBar
  ) {
    this.createForm();
  }

  createForm(): void {
    this.detailForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phoneNumber: [''],
      emailAddress: ['', [Validators.required, Validators.email]],
      preferredContactMethod: ['email'],
    });
  }

  ngOnInit(): void {
    this.customerId = +this.route.snapshot.params['id'];

    this.customerService //injected
      .get(this.customerId)
      .subscribe((cust) => {
        // like listening to a JavaScript fetch call to return
        if (cust) {
          this.customer = cust;
          if (this.customer) {
            this.detailForm.patchValue(this.customer);
          }
        }
      });
  }
  save(): void {
    if (!this.detailForm.valid) {
      return;
    }
    const customer = { ...this.customer, ...this.detailForm.value };
    this.customerService.update(customer);
    this.snackBar.open('Customer saved', 'OK');
  }
}
