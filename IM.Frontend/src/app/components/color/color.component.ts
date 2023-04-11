import {Component, OnInit} from '@angular/core';
import {Color} from 'src/app/models/color';
import {ColorService} from 'src/app/services/color.service';
import {CommonModule} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {ColorFilterPipe} from "../../core";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-color',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ColorFilterPipe,
    RouterLink
  ],
  templateUrl: './color.component.html',
  styleUrls: ['./color.component.css']
})
export class ColorComponent implements OnInit {
  colors: Color[] = [];
  dataLoaded = false;
  currentColor!: Color;
  colorFilter = "";

  constructor(private colorService: ColorService) {
  }

  ngOnInit(): void {
    this.getColors();
  }

  getColors() {
    this.colorService.getColors().subscribe(response => {
      this.colors = response.data,
        this.dataLoaded = true;
    })
  }

  setCurrentColor(color: Color) {
    this.currentColor = color;
  }

  getCurrentColorClass(color: Color) {
    if (color == this.currentColor) {
      return "list-group-item active cursorPointer";
    } else {
      return "list-group-item cursorPointer"
    }
  }

  getAllColorClass() {

    if (!this.currentColor) {
      return "list-group-item active cursorPointer";
    } else {
      return "list-group-item cursorPointer"
    }
  }

}
