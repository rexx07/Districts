import {Route} from "@angular/router";

export const ROUTES: Route[] = [
  {
    path: 'creditcard/:rental',
    loadComponent: async () => (await import('./creditcard.component')).CreditCardComponent
  }
]
