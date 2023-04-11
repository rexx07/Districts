import {Pipe, PipeTransform} from '@angular/core';
import {Brand} from '../../models';

@Pipe({
  name: 'brandFilter',
  standalone: true
})
export class BrandFilterPipe implements PipeTransform {

  transform(value: Brand[], brandFilter: string): Brand[] {
    brandFilter = brandFilter ? brandFilter.toLocaleLowerCase() : ""

    return brandFilter ? value.filter((b: Brand) => b.brandName.toLocaleLowerCase().indexOf(brandFilter) !== -1) : value;
  }

}
