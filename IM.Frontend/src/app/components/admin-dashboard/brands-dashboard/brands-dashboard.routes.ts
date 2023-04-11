import {Route} from "@angular/router";

export const ROUTES: Route[] = [
  {
    path: '',
    title: 'View',
    loadComponent: async () => (await import('./brands-dashboard.component')).BrandsDashboardComponent
  },
  {
    path: 'add',
    title: 'Add',
    loadComponent: async () => (await import('./brand-add/brand-add.component')).BrandAddComponent
  },
  {
    path: 'edit/:brandId',
    title: 'Edit',
    loadComponent: async () => (await import('./brand-edit/brand-edit.component')).BrandEditComponent
  }
]
