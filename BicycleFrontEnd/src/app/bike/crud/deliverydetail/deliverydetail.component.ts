import { Component, OnInit } from '@angular/core';
import { IUserDetails } from 'src/app/shared/model/IUserDetails';
import { DeliveryService } from 'src/app/deliverymap/delivery.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-deliverydetail',
  templateUrl: './deliverydetail.component.html',
  styleUrls: ['./deliverydetail.component.css']
})
export class DeliverydetailComponent implements OnInit {
  page: number;
  pagesize: number;
  details: Array<IUserDetails>
  constructor(private route: Router, private deliveryService: DeliveryService) {
    this.details = []
    this.page = 1;
    this.pagesize = 4;
  }

  ngOnInit() {

    debugger
    this.deliveryService.getUserRole().subscribe(res => {
      debugger
      this.details = res;
    },
      err => {
        console.log(err);

      })
  }
  updateRole(detail:string){

   this.route.navigate(['editdelivery/',{'role':detail}]);
  }


}
