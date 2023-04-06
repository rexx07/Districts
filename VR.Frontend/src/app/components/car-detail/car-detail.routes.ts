import {Route} from "@angular/router";

export const ROUTES: Route[] = [
  {
    path: 'car-detail',
    title: 'Car-Detail',
    loadComponent: async () => (await import('./car-detail.component')).CarDetailComponent
  }
]
