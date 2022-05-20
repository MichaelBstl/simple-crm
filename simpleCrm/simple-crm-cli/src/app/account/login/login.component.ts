import { PlatformLocation } from '@angular/common';
import { HttpParams } from '@angular/common/http';
import { Component, NgModule, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserSummaryViewModel } from '../account.model';
import { AccountService } from '../account.service';
import { AccountModule } from '../account.module';


@Component({
  selector: 'crm-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})


export class LoginComponent implements OnInit {
  loginType: 'undecided' | 'password' | 'microsoft' | 'google' = 'undecided';
  loginForm: FormGroup;
  currentUser: Observable<UserSummaryViewModel>;
  loading: boolean = false;

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private platformLocation: PlatformLocation,
    private snackBar: MatSnackBar,
    private router: Router
  ) {
    this.currentUser = this.accountService.user;
    this.loginForm = this.fb.group({
      emailAddress: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
  }

  useUndecided(): void {
    this.loginType = 'undecided';
  }

  usePassword(): void {
    this.loginType = 'password';
  }
  useMicrosoft(): void {
    this.loginType = 'microsoft';
    this.snackBar.open('Signing In with Microsoft...', '', { duration: 2000 });
    const baseUrl =
      'https://login.microsoftonline.com/common/oauth2/v2.0/authorize?';
    this.accountService.loginMicrosoftOptions().subscribe((opts) => {
      const options: { [key: string]: string } = {
        ...opts,
        response_type: 'code',
        redirect_uri:
          window.location.origin +
          this.platformLocation.getBaseHrefFromDOM() +
          'signin-microsoft',
      };
      console.log(options['redirect_uri']);
      let params = new HttpParams();
      for (const key of Object.keys(options)) {
        params = params.set(key, options[key]); // encodes values automatically.
      }

      window.location.href = baseUrl + params.toString();
    });
  }

  useGoogle(): void {
    this.loginType = 'google';
    this.snackBar.open('Signing In with Google...', '', { duration: 2000 });
    const baseUrl =
      'https://login.microsoftonline.com/common/oauth2/v2.0/authorize?';
    this.accountService.loginMicrosoftOptions().subscribe((opts) => {
      const options: { [key: string]: string } = {
        ...opts,
        response_type: 'code',
        redirect_uri:
          window.location.origin +
          this.platformLocation.getBaseHrefFromDOM() +
          'signin-microsoft',
      };
      console.log(options['redirect_uri']);
      let params = new HttpParams();
      for (const key of Object.keys(options)) {
        params = params.set(key, options[key]); // encodes values automatically.
      }

      window.location.href = baseUrl + params.toString();
    });
  }

  onSubmit(): void {
    if (!this.loginForm.valid) {
      return;
    }
    this.loading = true;
    const creds = {...this.loginForm.value };
    this.accountService.loginPassword(creds).subscribe({
      next: result => {
        this.accountService.loginComplete(result, 'Login Complete');
      },
      error: _ => {
        this.loading = false;
      }
    });
  }
  register(): void {
    this.router.navigate(['./register']);
  }
  logout(): void {
    this.accountService.logout();
    this.router.navigate(['./login']);
  }

}
