import {Route} from "@angular/router";
import {LoginGuard} from "../../core";

export const ROUTES: Route[] = [
  {
    path: 'car',
    title: 'Car',
    loadComponent: async () => (await import('./car.component')).CarComponent
  },
  {
    path: "cars/brand/:brandId",
    loadComponent: async () => (await import('./car.component')).CarComponent
  },
  {
    path: "cars/color/:colorId",
    loadComponent: async () => (await import('./car.component')).CarComponent
  },
  {
    path: "cars/brand/:brandId/color/:colorId",
    loadComponent: async () => (await import('./car.component')).CarComponent
  },
  {
    path: "car/details/:carId",
    loadComponent: async () => (await import('../car-detail/car-detail.component')).CarDetailComponent
  },
  {
    path: "cars/car-detail/:carId",
    loadComponent: async () => (await import('../car-detail/car-detail.component')).CarDetailComponent
  },
  {
    path: "cars/filter/:brandId/:colorId",
    loadComponent: async () => (await import('./car.component')).CarComponent
  },
  {
    path: "car/rental/:carId",
    loadComponent: async () => (await import('../rental/rental.component')).RentalComponent,
    canActivate: [LoginGuard]
  },
]
