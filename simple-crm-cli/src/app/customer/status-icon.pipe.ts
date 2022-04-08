import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'statusIcon'
})
export class StatusIconPipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): string {
    if (value === 'Runner') {
      return 'running';
    }
    else if (value === 'Cyclist') {
      return 'cycling'
    }
    else {
      return 'sleeping'
    }
  }
}
