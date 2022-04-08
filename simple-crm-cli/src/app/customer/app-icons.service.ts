import { Injectable } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root'
})
export class AppIconsService {
  constructor(
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer
  ) {
      this.iconRegistry.addSvgIcon('running', this.sanitizer.bypassSecurityTrustResourceUrl('assets/exercise-game-svgrepo-com.svg'));
      this.iconRegistry.addSvgIcon('cycling', this.sanitizer.bypassSecurityTrustResourceUrl('assets/bicycle-bike-svgrepo-com.svg'));
      this.iconRegistry.addSvgIcon('sleeping', this.sanitizer.bypassSecurityTrustResourceUrl('assets/wrath-angry-anger-svgrepo-com.svg'));
    }
}
