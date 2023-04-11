import {Component, OnInit} from "@angular/core";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {ActivatedRoute, Router, RouterLink} from "@angular/router";
import {ToastrService} from "ngx-toastr";
import {Color} from "src/app/models/color";
import {ColorService} from "src/app/services/color.service";
import {CommonModule} from "@angular/common";

@Component({
  selector: 'app-color-edit',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink
  ],
  templateUrl: './color-edit.component.html',
  styleUrls: ['./color-edit.component.css']
})
export class ColorEditComponent implements OnInit {

  color!: Color;
  colorEditForm: FormGroup = new FormGroup({});

  constructor(
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private colorService: ColorService,
    private toastrService: ToastrService,
    private router: Router
  ) {
  }

  ngOnInit(): void {
    this.createColorUpdateForm()
    this.activatedRoute.params.subscribe(params => {
      if (params["colorId"]) {
        this.getColorById(params["colorId"])
      }
    })
  }

  createColorUpdateForm() {
    this.colorEditForm = this.formBuilder.group({
      colorName: ["", Validators.required]
    })
  }

  getColorById(colorId: number) {
    this.colorService.getById(colorId).subscribe((response) => {
      this.color = response.data;
    });
  }

  updateColor() {
    if (this.colorEditForm.valid) {
      let colorModel = Object.assign({}, this.colorEditForm.value)
      colorModel.colorId = Number(this.color.colorId)
      this.colorService.updateColor(colorModel).subscribe(response => {
        this.toastrService.success(response.message, "Başarılı")
        this.router.navigate(['admin', 'colors']);
      }, responseError => {
        this.toastrService.success(responseError.message, "Hata")
      })
    } else {
      this.toastrService.error("Form eksik", "Dikkat")
    }
  }


  deleteColor() {
    if (window.confirm('Rengi Sildiğine  emin misin?')) {
      let colorModule: Color = {
        colorId: this.color.colorId,
        ...this.colorEditForm.value,
      };
      this.colorService.deleteColor(colorModule).subscribe(
        (response) => {
          this.toastrService.success(response.message);
          this.router.navigate(['admin', 'colors']);
        },
        (responseError) => {
          if (responseError.error.Errors.length > 0)
            responseError.error.Errors.forEach((error: any) =>
              this.toastrService.error(error.ErrorMessage)
            );
        }
      );
    }
  }
}
