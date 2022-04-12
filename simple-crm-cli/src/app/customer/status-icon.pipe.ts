import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'statusIcon'
})
export class StatusIconPipe implements PipeTransform {

  transform(value: string | null | undefined, ...args: unknown[]): string {
    if (value?.search(/runner/i) === 0) {
      return 'running';
    }
    else if (value?.search(/cyclist/i) === 0) {
      return 'cycling'
    }
    else {
      return 'sleeping'
    }
  }
}
