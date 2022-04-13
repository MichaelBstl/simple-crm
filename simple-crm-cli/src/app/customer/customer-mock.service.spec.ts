import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed, getTestBed } from '@angular/core/testing';

import { CustomerMockService } from './customer-mock.service';
import { Customer } from './customer.model';

fdescribe('CustomerMockService', () => {
  let injector: TestBed;
  let service: CustomerMockService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule ],
      providers: [ CustomerMockService ]
    });
    injector = getTestBed();
    service = TestBed.inject(CustomerMockService);
    httpMock = injector.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify(); // ensures there are no outstanding requests between tests.
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('Make sure JOHN is retured', () => {
    let customer = service.get(1);

    let foundJohn:boolean = false;
    customer.subscribe(cust => {
      customer.forEach (element => {
        if (element?.firstName === "John") {
          foundJohn = true;
        }
        expect(foundJohn).toBe(true);
      })

    })
  });
});
