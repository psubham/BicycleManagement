import { Component, OnInit } from '@angular/core';
import { BookingService } from '../../../booking/service/booking.service';
import { IBooking } from '../../../shared/model/Ibooking';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-bookinghistory',
  templateUrl: './bookinghistory.component.html',
  styleUrls: ['./bookinghistory.component.css']
})
export class BookinghistoryComponent implements OnInit {

  bookings:Array<IBooking>;
  page:number;
  pagesize:number;
  constructor(private bookingServie:BookingService,
              private userService:UserService) {

                this.page = 1;
                this.pagesize = 4;
                this.bookings=[];
  }
  ngOnInit() {

    this.bookingServie.GetBookingDetail(JSON.parse(this.userService.getUser()).userName).subscribe(
      res=>{
        this.bookings=res;
      },error=>{
        console.log(error);
      }
    )

  }

}
