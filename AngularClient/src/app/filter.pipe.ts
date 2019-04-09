import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filterPipe'
})
export class FilterPipe implements PipeTransform {

  transform(value: any, args?: any): any {
    if (!args) {
      return value;
    } else {
      return value.filter(items => {
        return items.question.toLowerCase().includes(args.toLowerCase()) === true;
      });
    }
  }

}
