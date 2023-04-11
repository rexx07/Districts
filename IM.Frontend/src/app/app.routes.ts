import {LoginGuard} from './core';
import {Routes} from "@angular/router";

export const routes: Routes = [
  {
    path: "home",
    loadChildren: async () => (await import('./components/home/home.routes')).ROUTES
  },
  {
    path: "cars",
    loadChildren: async () => (await import('./components/car/car.routes')).ROUTES
  },
  {
    path: "brands",
    loadChildren: async () => (await import('./components/brand/brand.routes')).ROUTES
  },
  {
    path: "credit-card",
    loadChildren: async () => (await import('./components/credit-card/credit-card.routes')).ROUTES
  },
  {
    path: "",
    loadChildren: async () => (await import('./components/auth/auth.routes')).ROUTES
  },
  {
    path: "user",
    loadChildren: async () => (await import('./components/user-profile/user.routes')).ROUTES
  },
  {
    path: 'admin',
    loadChildren: async () => (await import('./components/admin-dashboard/admin-dashboard.routes')).ROUTES,
    canActivate: [LoginGuard]
  },
  {
    path: "",
    redirectTo: 'home',
    pathMatch: "full",
  },
  {
    path: '**',
    loadComponent: async () => (await import('./components/not-found/not-found.page')).NotFoundPage,
  }
];
