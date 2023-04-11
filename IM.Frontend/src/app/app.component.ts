import {Component} from '@angular/core';
import {NaviComponent} from "./components/layout";
import {RouterOutlet} from "@angular/router";

export function tokenGetter() {
  return localStorage.getItem("token");
}

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    NaviComponent,
    RouterOutlet
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Vehicle Rental';

}
