import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import {MatToolbarModule} from '@angular/material/toolbar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatIconModule, MatIconRegistry } from '@angular/material/icon'
import { MatSidenavModule} from '@angular/material/sidenav'
import { MatListModule } from '@angular/material/list'
import { CustomerModule } from './customer/customer.module';
import { MatButtonModule } from '@angular/material/button';
import { AppIconsService } from './customer/app-icons.service';
import { NotAuthorizedComponent } from './account/not-authorized/not-authorized.component';
import { AccountModule } from './account/account.module';

@NgModule({
  declarations: [
    AppComponent,
    NotAuthorizedComponent
  ],
  imports: [
    BrowserModule,
    MatToolbarModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    AccountModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    CustomerModule,
    MatButtonModule
  ],
  providers: [AppIconsService],
  bootstrap: [AppComponent]
})
export class AppModule {
    // simply passing in the icon service, instantiates it and registers its icons
    constructor(iconService: AppIconsService) {}  // <-- NEW
 }
