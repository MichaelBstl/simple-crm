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
import { AccountModule } from './account/account.module';
import { JwtInterceptorInterceptor } from './account/jwt-interceptor.interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { StoreModule } from '@ngrx/store';
import { layoutFeatureKey, layoutReducer } from './store/layout.store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';

@NgModule({
  declarations: [
    AppComponent
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
    MatButtonModule,
    AccountModule,
    StoreModule.forRoot({}), // for no global state, use an empty object,  {}.
    StoreModule.forFeature(layoutFeatureKey, layoutReducer),
    StoreDevtoolsModule.instrument({
      name: 'Nexul Academy - Simple CRM'
    })
  ],
  providers: [{
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptorInterceptor,
      multi: true
      },
    AppIconsService],
  bootstrap: [AppComponent]
})
export class AppModule {
    // simply passing in the icon service, instantiates it and registers its icons
    constructor(
      iconService: AppIconsService
    )
    {}
}
