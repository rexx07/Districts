import {Component, OnInit} from '@angular/core';
import {Brand} from 'src/app/models/brand';
import {BrandService} from 'src/app/services/brand.service';
import {CommonModule} from "@angular/common";
import {BrandAddComponent} from "./brand-add/brand-add.component";
import {BrandEditComponent} from "./brand-edit/brand-edit.component";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-brands-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    BrandAddComponent,
    BrandEditComponent,
    RouterLink
  ],
  templateUrl: './brands-dashboard.component.html',
  styleUrls: ['./brands-dashboard.component.css']
})
export class BrandsDashboardComponent implements OnInit {

  brands: Brand[] = [];
  dataLoaded = false;

  constructor(
    private brandService: BrandService,
  ) {
  }

  ngOnInit(): void {
    this.getBrands()
  }

  getBrands() {
    this.brandService.getBrands().subscribe(response => {
      this.brands = response.data,
        this.dataLoaded = true;
    })

  }
}
