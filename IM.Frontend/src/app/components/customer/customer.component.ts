import {Component, OnInit} from '@angular/core';
import {Customer} from 'src/app/models/customer';
import {CustomerService} from 'src/app/services/customer.service';
import {CommonModule} from "@angular/common";

@Component({
  selector: 'app-customer',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {
  customers: Customer[] = [];
  dataLoaded = false;

  constructor(private customerService: CustomerService) {
  }

  ngOnInit(): void {
    this.getCustomer();
  }

  getCustomer() {
    this.customerService.getCustomer().subscribe(response => {
      this.customers = response.data;
      this.dataLoaded = true;
    })
  }

}
