import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../account.service';


@Component({
  selector: 'crm-signin-microsoft',
  templateUrl: './signin-microsoft.component.html',
  styleUrls: ['./signin-microsoft.component.scss']
})
//export class SigninMicrosoftComponent implements OnInit {

  export class SigninMicrosoftComponent {

    loading = false;

    constructor(
      public route: ActivatedRoute,
      private router: Router,
      private accountService: AccountService,
      public snackBar: MatSnackBar
    ) {
      // the token is in the querystring value named 'code'
      this.route.queryParamMap.subscribe(prms => {
        const code = prms.get('code') || '';
        const sessionState = prms.get('session_state') || '';
        if (code) {
          this.snackBar.open('Validating Login...', '', { duration: 8000 }); // user feedback
          this.loading = true; // show spinner when this is true
          this.accountService.loginMicrosoft(code, sessionState).subscribe(
            // call your API endpoint with the code and process the result.
            result => {
              // your Angular service code can handle storing the current user.
              this.accountService.loginComplete(result, 'Email has been verified');
            },
            msg => { // _ is an error
              this.loading = false;
              this.accountService.logout({navigate: false});
              if (typeof msg === 'string') {
                this.snackBar.open('Verification Failed. ${msg}', 'OK', { duration: 10000 });
                this.router.navigate(['./login']);
              }
              else  if (msg.error) {
                this.snackBar.open('Verification Failed. ${msg.error}', 'OK', { duration: 10000 });
                this.router.navigate(['./register']);
                }
                else {
                  this.snackBar.open('Verification Failed. Try to login with another account.', '', { duration: 10000 });
                this.router.navigate(['./account/login']);
                }
            }
          );
        }
      });
    }
}
