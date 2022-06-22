import { Component } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { LayoutState, selectShowSideNav, toggleSidenav } from './store/layout.store';

@Component({
  selector: 'crm-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Simple CRM';
  showSideNav$: Observable<boolean>;
  constructor(private store: Store<LayoutState>) {
    this.showSideNav$ = this.store.pipe(select(selectShowSideNav));
  }

  sideNavToggle() {
    this.store.dispatch(toggleSidenav());
  }
}
