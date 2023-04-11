import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Car, CarStandard, DashboardCars, ListResponseModel, ResponseModel, SingleResponseModel} from '../models';
import {BaseService} from "./base.service";

@Injectable({
  providedIn: 'root'
})
export class CarService extends BaseService<Car>{
  constructor(httpClient: HttpClient) {
    super(httpClient);
  }

  getCars(): Observable<ListResponseModel<Car>> {
    let url = this.getUrl("cars/getcardetails");
    return this.httpClient.get<ListResponseModel<Car>>(url)
  }

  getCarById(carId: number): Observable<SingleResponseModel<Car>> {
    let url = this.getUrl("cars/getbyıd?carId=" + carId);
    return this.httpClient.get<SingleResponseModel<Car>>(url);
  }

  addCar(car: Car): Observable<ResponseModel> {
    let url = this.getUrl( "cars/add");
    return this.httpClient.post<ResponseModel>(url, car);
  }

  updateCar(car: CarStandard): Observable<ResponseModel> {
    let url = this.getUrl("cars/update");
    return this.httpClient.post<ResponseModel>(url, car);
  }

  deletCar(car: CarStandard): Observable<ResponseModel> {
    let url = this.getUrl("cars/delete");
    return this.httpClient.post<ResponseModel>(url, car);
  }

  getCarsByBrand(brandId: number): Observable<ListResponseModel<Car>> {
    let url = this.getUrl("cars/getbybrand?brandId=" + brandId);
    return this.httpClient.get<ListResponseModel<Car>>(url);
  }

  getCarsByColor(colorId: number): Observable<ListResponseModel<Car>> {
    let url = this.getUrl("cars/getbycolor?colorId=" + colorId);
    return this.httpClient.get<ListResponseModel<Car>>(url);
  }

  getCarsBySelect(brandId: number, colorId: number): Observable<ListResponseModel<Car>> {
    let url = this.getUrl("cars/getbyselected?brandId=" + brandId + "&colorId=" + colorId);
    return this.httpClient
      .get<ListResponseModel<Car>>(url);
  }

  getCarDetail(carId: number): Observable<ListResponseModel<Car>> {
    let url = this.getUrl("cars/getcardetail?carId=" + carId);
    return this.httpClient
      .get<ListResponseModel<Car>>(url);
  }

  getAllCarDetail(): Observable<ListResponseModel<DashboardCars>> {
    let url = this.getUrl("cars/getallcardetail");
    return this.httpClient
      .get<ListResponseModel<DashboardCars>>(url);
  }
}
