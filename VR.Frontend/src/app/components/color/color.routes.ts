import {Route} from "@angular/router";

export const ROUTES: Route[] = [
  {
    path: 'color',
    title: 'Color',
    loadComponent: async () => (await import('./color.component')).ColorComponent
  }
]
