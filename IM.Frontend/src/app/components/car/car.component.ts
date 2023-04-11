import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, RouterLink} from '@angular/router';
import {Car} from 'src/app/models/car';
import {CarService} from 'src/app/services/car.service';
import {CommonModule} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {BrandComponent} from "../brand/brand.component";
import {ColorComponent} from "../color/color.component";
import {CarFilterComponent} from "../car-filter/car-filter.component";
import {CarFilterPipe} from "../../core";
import {FooterComponent} from "../layout";

@Component({
  selector: 'app-car',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    BrandComponent,
    ColorComponent,
    CarFilterComponent,
    CarFilterPipe,
    FooterComponent,
    RouterLink
  ],
  templateUrl: './car.component.html',
  styleUrls: ['./car.component.css']
})
export class CarComponent implements OnInit {
  cars: Car[] = [];
  dataLoaded = false
  imageUrl = "https://localhost:44388";
  carFilter = "";

  constructor(
    private carService: CarService,
    private activatedRoute: ActivatedRoute,
  ) {


  }

  ngOnInit(): void {

    this.activatedRoute.params.subscribe(params => {
      if (params["brandId"] && params["colorId"]) {
        this.getCarsBySelect(params["brandId"], params["colorId"])
      } else if (params["colorId"]) {
        this.getCarsByColor(params["colorId"]);
      } else if (params["brandId"]) {
        this.getCarsByBrand(params["brandId"])

      } else {
        this.getCars()
      }
    })

  }

  getCars() {
    this.carService.getCars().subscribe(response => {
      this.cars = response.data;
      this.dataLoaded = true;
    })
  }

  getCarsByBrand(brandId: number) {

    this.carService.getCarsByBrand(brandId).subscribe(response => {
      this.cars = response.data;
      this.dataLoaded = true;


    })
  }

  getCarsByColor(colorId: number) {

    this.carService.getCarsByColor(colorId).subscribe(response => {
      this.cars = response.data;
      this.dataLoaded = true;

    })
  }

  getCarsBySelect(brandId: number, colorId: number) {
    this.carService.getCarsBySelect(brandId, colorId).subscribe(response => {
      this.cars = response.data
      this.dataLoaded = true;
    })
  }

}
