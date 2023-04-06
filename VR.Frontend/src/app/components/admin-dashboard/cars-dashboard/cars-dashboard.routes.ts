import {Route} from "@angular/router";

export const ROUTES: Route[] = [
  {
    path: '',
    title: 'View',
    loadComponent: async () => (await import('./cars-dashboard.component')).CarsDashboardComponent
  },
  {
    path: 'add',
    title: 'Add',
    loadComponent: async () => (await import('./car-add/car-add.component')).CarAddComponent
  },
  {
    path: 'edit/:carId',
    title: 'Edit',
    loadComponent: async () => (await import('./car-edit/car-edit.component')).CarEditComponent
  }
]
