import {Route} from '@angular/router';

export const ROUTES: Route[] = [
  {
    path: 'profile',
    title: 'Profile',
    loadComponent: async () => (await import('./user-profile.component')).UserComponent,
  },
  {
    path: 'edit',
    title: 'Edit',
    loadComponent: async () => (await import('./user-edit/user-edit.component')).UserEditComponent,
  },
];
