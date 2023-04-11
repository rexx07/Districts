import {Route} from "@angular/router";

export const ROUTES: Route[] = [
  {
    path: 'customer',
    title: 'Customer',
    loadComponent: async () => (await import('./customer.component')).CustomerComponent
  }
]
