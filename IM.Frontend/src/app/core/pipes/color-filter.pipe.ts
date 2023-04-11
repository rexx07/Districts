import {Pipe, PipeTransform} from '@angular/core';
import {Color} from '../../models';

@Pipe({
  name: 'colorFilter',
  standalone: true
})
export class ColorFilterPipe implements PipeTransform {

  transform(value: Color[], colorFilter: string): Color[] {
    colorFilter = colorFilter ? colorFilter.toLocaleLowerCase() : ""
    return colorFilter ? value.filter((c: Color) => c.colorName.toLocaleLowerCase().indexOf(colorFilter) !== -1) : value
  }
}
