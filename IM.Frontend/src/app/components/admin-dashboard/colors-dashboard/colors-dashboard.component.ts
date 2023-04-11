import {Component, OnInit} from '@angular/core';
import {Color} from 'src/app/models/color';
import {ColorService} from 'src/app/services/color.service';
import {CommonModule} from "@angular/common";
import {ColorAddComponent} from "./color-add/color-add.component";
import {ColorEditComponent} from "./color-edit/color-edit.component";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-colors-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    ColorAddComponent,
    ColorEditComponent,
    RouterLink
  ],
  templateUrl: './colors-dashboard.component.html',
  styleUrls: ['./colors-dashboard.component.css']
})
export class ColorsDashboardComponent implements OnInit {
  colors: Color[] = [];
  dataLoaded = false;

  constructor(
    private colorService: ColorService,
  ) {
  }

  ngOnInit(): void {
    this.getColors()
  }

  getColors() {
    this.colorService.getColors().subscribe(response => {
      this.colors = response.data,
        this.dataLoaded = true;
    })
  }


}
