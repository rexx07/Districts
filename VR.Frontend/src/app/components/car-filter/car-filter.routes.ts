import {Route} from "@angular/router";

export const ROUTES: Route[] = [
  {
    path: 'car-filter',
    title: 'Car-Filter',
    loadComponent: async () => (await import('./car-filter.component')).CarFilterComponent
  }
]
