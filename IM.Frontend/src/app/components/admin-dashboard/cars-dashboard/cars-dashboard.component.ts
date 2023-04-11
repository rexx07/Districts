import {Component, OnInit} from '@angular/core';
import {DashboardCars} from 'src/app/models/dashboard-cars';
import {CarService} from 'src/app/services/car.service';
import {CommonModule} from "@angular/common";
import {CarAddComponent} from "./car-add/car-add.component";
import {CarEditComponent} from "./car-edit/car-edit.component";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-cars-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    CarAddComponent,
    CarEditComponent,
    RouterLink
  ],
  templateUrl: './cars-dashboard.component.html',
  styleUrls: ['./cars-dashboard.component.css']
})
export class CarsDashboardComponent implements OnInit {

  cars: DashboardCars[] = [];
  dataLoaded = false;

  constructor(
    private carService: CarService,
  ) {
  }

  ngOnInit(): void {
    this.getCars()
  }

  getCars() {
    this.carService.getAllCarDetail().subscribe(response => {
      this.cars = response.data,
        this.dataLoaded = true

    })
  }
}
