import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { LayoutState, toggleSidenav } from './store/layout.store';

@Component({
  selector: 'crm-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Simple CRM';
  constructor(private store: Store<LayoutState>) { }

  sideNavToggle() {
    this.store.dispatch(toggleSidenav());
  }
}
