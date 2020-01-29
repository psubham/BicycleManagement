import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IBooking } from '../../shared/model/Ibooking';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  url;
  constructor(private http: HttpClient) {
    this.url = 'http://localhost:57207';
   }
   headerJson = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Cache-Control': 'no-cache'
    })
  }
  book(bicycle: IBooking) {
    return this.http.post(this.url + '/api/Booking', bicycle);
  }
  isActive(UserName:string)
  {
    console.log(UserName);

    return this.http.post<IBooking>(this.url+'/api/Booking/ActiveRide',`"${UserName}"`,this.headerJson);
  }
  GetBookingDetail(UserName:string)
  {
    return this.http.post<Array<IBooking>>(this.url+'/api/Booking/GetDetailOfUser',`"${UserName}"`,this.headerJson);
  }
}
