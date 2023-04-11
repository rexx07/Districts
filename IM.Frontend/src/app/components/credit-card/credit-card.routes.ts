import {Route} from "@angular/router";

export const ROUTES: Route[] = [
  {
    path: 'credit-card/:rental',
    loadComponent: async () => (await import('./credit-card.component')).CreditCardComponent
  }
]
