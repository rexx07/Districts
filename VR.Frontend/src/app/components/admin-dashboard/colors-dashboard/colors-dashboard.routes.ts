import {Route} from "@angular/router";

export const ROUTES: Route[] = [
  {
    path: '',
    title: 'View',
    loadComponent: async () => (await import('./colors-dashboard.component')).ColorsDashboardComponent
  },
  {
    path: 'add',
    title: 'Add',
    loadComponent: async () => (await import('./color-add/color-add.component')).ColorAddComponent
  },
  {
    path: 'edit/:colorId',
    title: 'Edit',
    loadComponent: async () => (await import('./color-edit/color-edit.component')).ColorEditComponent
  }
]
