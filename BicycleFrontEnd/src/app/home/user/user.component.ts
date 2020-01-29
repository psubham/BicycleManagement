import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { BookingService } from '../../booking/service/booking.service';
import { IBooking } from '../../shared/model/Ibooking';
import { IBicycleType } from 'src/app/shared/model/IBicycleType';
import { BicycleTypeService } from 'src/app/bike/services/bicycletype.service';
import { Router } from '@angular/router';
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';
import { MockService } from 'src/app/services/mock.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  userId:string;
  deliveryBoyName:string;
  flag:boolean;
  bookingDetail={} as IBooking;
  bicycle:IBicycleType;
  constructor(private userService:UserService,
              private mockService:MockService,
              private bookingService:BookingService,
              private bicycleService:BicycleTypeService,
              private modalService: NgbModal,
              private spinnerService: Ng4LoadingSpinnerService,
              private routes: Router) {
                this.flag=false;
                this.deliveryBoyName='';
              }

  ngOnInit() {
    this.userId= JSON.parse(this.userService.getUser()).userName;
    console.log(this.userId);
    this.spinnerService.show();
    this.bookingService.isActive(this.userId).subscribe(
      res=>{
        if(res==null){
          console.log("booking");
          this.spinnerService.hide();
           this.routes.navigateByUrl('/booking');
        }
        this.bookingDetail=res;
        this.bicycleService.GetType(this.bookingDetail.bicycleId).subscribe(
          recordthere=>{
            this.bicycle=recordthere;
            console.log(this.bicycle);

          },error=>{
            alert("error");
          },
          ()=>{
            this.spinnerService.hide();
          }
        );
      }
    );
  }

  openVerticallyCentered(content) {
    this.mockService.getDeliveryConfirmation(this.bookingDetail.bookingId).subscribe(
      confirm=>{
        console.log(confirm);

        if(confirm == 'Cancel')
          this.flag=true;
        else
          this.deliveryBoyName=confirm;
        this.modalService.open(content, { centered: true });
      }
    );
  }
}
