import {Pipe, PipeTransform} from '@angular/core';
import {Car} from '../../models';

@Pipe({
  name: 'carFilter',
  standalone: true
})
export class CarFilterPipe implements PipeTransform {

  transform(value: Car[], carFilter: string): Car[] {
    carFilter = carFilter ? carFilter.toLocaleLowerCase() : ""

    return carFilter ? value.filter((c: Car) => c.carName.toLocaleLowerCase().indexOf(carFilter) !== -1) : value;
  }

}
