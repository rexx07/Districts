import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {Color} from 'src/app/models/color';
import {ColorService} from 'src/app/services/color.service';
import {CommonModule} from "@angular/common";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-color-add',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './color-add.component.html',
  styleUrls: ['./color-add.component.css']
})
export class ColorAddComponent implements OnInit {

  colors: Color[] = [];
  colorAddForm: FormGroup = new FormGroup({});
  dataLoaded = false;

  constructor(
    private colorService: ColorService,
    private toastrService: ToastrService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {
  }

  ngOnInit(): void {
    this.createCarAddForm();
  }

  createCarAddForm() {
    this.colorAddForm = this.formBuilder.group({
      colorName: ["", Validators.required]

    })
  }

  getColors() {
    this.colorService.getColors().subscribe(response => {
      this.colors = response.data,
        this.dataLoaded = true;
    })
  }

  addColor() {
    if (this.colorAddForm.valid) {
      let colorModel = Object.assign({}, this.colorAddForm.value);
      this.colorService.addColor(colorModel).subscribe(
        response => {
          this.toastrService.success(response.message, "Başarılı")
          this.router.navigate(['admin', 'colors']);
        },
        responseError => {
          if (responseError.error.ValidationErrors.length > 0) {
            for (let i = 0; i < responseError.error.ValidationErrors.length; i++) {
              this.toastrService.error(responseError.error.ValidationErrors[i].ErrorMessage, "Doğrulama Hatası")
            }
          }
        })
    } else {
      this.toastrService.error("Formunuz Eksik", "Dikkat!")
    }
  }

}
