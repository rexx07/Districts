import {Route} from "@angular/router";

export const ROUTES: Route[] = [
  {
    path: 'admin-dashboard',
    title: 'Admin-Dashboard',
    loadComponent: async () => (await import( './admin-dashboard.component')).AdminDashboardComponent
  },
  {
    path: 'brands-dashboard',
    title: 'Brands-Dashboard',
    loadChildren: async () => (await import( './brands-dashboard/brands-dashboard.routes')).ROUTES
  },
  {
    path: 'cars-dashboard',
    title: 'Cars-Dashboard',
    loadChildren: async () => (await import( './cars-dashboard/cars-dashboard.routes')).ROUTES
  },
  {
    path: 'colors-dashboard',
    title: 'Colors-Dashboard',
    loadChildren: async () => (await import( './colors-dashboard/colors-dashboard.routes')).ROUTES
  }
]
