import {Route} from "@angular/router";

export const ROUTES: Route[] = [
  {
    path: 'rental',
    title: 'Rental',
    loadComponent: async () => (await import('./rental.component')).RentalComponent
  }
]
