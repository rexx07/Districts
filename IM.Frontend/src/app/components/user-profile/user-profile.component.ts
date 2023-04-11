import {CommonModule} from '@angular/common';
import {Component, OnInit} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {User} from 'src/app/models/user';
import {AuthService} from 'src/app/core/core-services/auth.service';
import {UserService} from 'src/app/core/core-services/user.service';
import {UserEditComponent} from "./user-edit/user-edit.component";
import {RouterOutlet} from "@angular/router";

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [
    CommonModule,
    UserEditComponent,
    RouterOutlet
  ],
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserComponent implements OnInit {
  userForm: FormGroup = new FormGroup({});
  user!: User;
  lastName = this.authService.name;
  firstName = this.authService.surname;
  email = this.authService.email;

  constructor(
    private authService: AuthService,
    private userService: UserService,
  ) {
  }

  ngOnInit(): void {
    this.getUser();
  }

  getUser() {
    this.userService.getbyid(this.authService.userId).subscribe(response => {
      this.user = response.data
      this.userForm.patchValue(response.data)
    })
  }


}
