import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {AuthService} from 'src/app/core/core-services/auth.service';
import {LocalStorageService} from 'src/app/core/core-services/local-storage-service.service';
import {CommonModule} from "@angular/common";

@Component({
  selector: 'app-navi',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './navi.component.html',
  styleUrls: ['./navi.component.css']
})
export class NaviComponent implements OnInit {
  lastName = this.authService.name;
  firstName = this.authService.surname;
  userRol = this.authService.role

  constructor(
    private authService: AuthService,
    private toasterService: ToastrService,
    private localStorageService: LocalStorageService,
    private router: Router
  ) {
  }

  ngOnInit(): void {
    if (this.isAuthenticated()) {
      this.authService.userDetailFromToken();

    }


  }

  isAuthenticated() {
    if (this.authService.isAuthenticated()) {
      return true


    } else {
      return false
    }
  }

  checkAdminRole() {


    if (this.authService.role[0] == "admin") {
      return true
    } else {
      return false

    }
  }

  checkUserRole() {
    if (this.authService.role == "user") {
      return true
    } else {
      return false
    }
  }

  checkNotRole() {
    if (this.authService.role == null) {
      return true
    } else {
      return false
    }
  }

  logout() {
    this.authService.logout()
    this.toasterService.success("Çıkış Yapıldı", "Başarılı")
  }

}
