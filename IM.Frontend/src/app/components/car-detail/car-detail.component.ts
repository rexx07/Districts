import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, RouterLink} from '@angular/router';
import {Car} from 'src/app/models/car';
import {CarImage} from 'src/app/models/carImage';
import {CarImageService} from 'src/app/services/car-image.service';
import {CarService} from 'src/app/services/car.service';
import {CommonModule} from "@angular/common";

@Component({
  selector: 'app-car-detail',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink
  ],
  templateUrl: './car-detail.component.html',
  styleUrls: ['./car-detail.component.css']
})
export class CarDetailComponent implements OnInit {

  cars: Car[] = [];
  carImages: CarImage[] = [];
  currentImage!: CarImage;
  dataLoaded = false;
  imageUrl = "https://localhost:44388";

  constructor(private carService: CarService,
              private carImageService: CarImageService,
              private activatedRoute: ActivatedRoute,) {
  }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      if (params["carId"]) {
        this.getCarDetail(params["carId"])

      }
      this.getImagesByCarId();
    })
  }

  getCarDetail(carId: number) {
    this.carService.getCarDetail(carId).subscribe((response) => {
      this.cars = response.data;
      this.dataLoaded = true;
    });
  }

  getImagesByCarId() {

    this.carImageService.getCarImages(this.activatedRoute.snapshot.params["carId"]).subscribe((response) => {
      this.carImages = response.data;
    });
  }

  getCurrentImageClass(image: CarImage) {
    if (image == this.carImages[0]) {
      return "carousel-item active"
    } else {
      return "carousel-item"
    }
  }

  getButtonClass(image: CarImage) {
    if (image == this.carImages[0]) {
      return "active"
    } else {
      return ""
    }
  }

}
