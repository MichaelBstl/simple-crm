import { PlatformLocation } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import {
  anonymousUser,
  CredentialsViewModel,
  MicrosoftOptions,
  UserSummaryViewModel,
} from './account.model';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private baseUrl: string;
  private cachedUser = new BehaviorSubject<UserSummaryViewModel>(
    anonymousUser()
  );

  constructor(
    private http: HttpClient,
    private router: Router,
    private platformLocation: PlatformLocation,
    private snackBar: MatSnackBar
  ) {
    this.baseUrl = environment.server + environment.apiUrl + 'auth/';
    const cu = localStorage.getItem('currentUser');
    if (cu) {
      // if already logged in from before, just use that... it has a JWT in it.
      this.cachedUser.next(JSON.parse(cu)); // <- JSON is built into JavaScript and always available.
    }
  }
  get user(): BehaviorSubject<UserSummaryViewModel> {
    // components can pipe off of this to get a new value as they login/logout
    return this.cachedUser;
  }
  setUser(user: UserSummaryViewModel): void {
    // called by your components that process a login from password, Google, Microsoft
    this.cachedUser.next(user);
    localStorage.setItem('currentUser', JSON.stringify(user));
  }

  public loginMicrosoftOptions(): Observable<MicrosoftOptions> {
    //TODO: it's up to you to add interface to MicrosoftOptions to account.model.ts
    return this.http.get<MicrosoftOptions>(this.baseUrl + 'external/microsoft');
  }

  get isAnonymous(): boolean {
    if (this.user.next.name === 'Anonymous') {
      return true;
    } else {
      return false;
    }
  }
  /**
   * Name and password login API call.
   * If a successful login is completed, you may want to call login Complete
   * to handle updates to the current user and redirect to where they
   * originally wanted to go.
   */
  public loginPassword(credentials: CredentialsViewModel): Observable<UserSummaryViewModel> {
    this.cachedUser.next(anonymousUser());
    localStorage.removeItem('currentUser');
    return this.http.post<UserSummaryViewModel>(this.baseUrl + 'login', credentials);
  }

  loginComplete(user: UserSummaryViewModel, message: string) {
    this.setUser(user);
    localStorage.getItem('destinationURL');
    //TODO: direct to proper URL
    this.snackBar.open('You are logged in', 'OK', { duration: 10000 });
    this.router.navigate(['customers']);
  }

  public loginMicrosoft(code: string, state: string): Observable<UserSummaryViewModel> {
    const body = { accessToken: code, state, baseHref: this.platformLocation.getBaseHrefFromDOM }
    return this.http.post<UserSummaryViewModel>(this.baseUrl + 'external/microsoft', body);
  }

  /*
  removes any cached login data and sends the user to the main website home page.
  */
  logout(options: { navigate: boolean;} = {navigate: false}): void  {
    this.cachedUser.next(anonymousUser());
    localStorage.setItem('currentUser', JSON.stringify(anonymousUser()));
    if (options.navigate)
    {

    }
  }
}
