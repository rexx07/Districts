import {Route} from "@angular/router";

export const ROUTES: Route[] = [
  {
    path: 'brand',
    title: 'Brand',
    loadComponent: async () => (await import('./brand.component')).BrandComponent
  }
]
