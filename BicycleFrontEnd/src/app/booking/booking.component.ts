import { Component, OnInit } from '@angular/core';
import { Hub } from '../map/hub';
import { MapService } from '../map/map.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, Validators } from '@angular/forms';
import { BookingService } from './service/booking.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IBooking } from '../shared/model/Ibooking';
import { UserService } from '../services/user.service';
import { IPosition } from '../deliverymap/deliveyboy/IPosition';
import { BicycleTypeService } from '../bike/services/bicycletype.service';


@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css']
})
export class BookingComponent implements OnInit {
  hubs: Array<Hub>;
  hubId: number;
  typeId: number;
  bicycleTypes: any[];
  address: string;
  latlng = {} as IPosition;

  action: string;
  booking = {} as IBooking;
  constructor(private mapService: MapService,
    private router: Router,
    private bikeTypeService: BicycleTypeService,
    private bookingServise: BookingService,
    private modalService: NgbModal,
    private toastr: ToastrService,
    private userService: UserService,
    public fb: FormBuilder) {
    this.action = 'booking';
    this.hubId = null;
    this.hubs = [];


    this.bicycleTypes = [];
  }


  form = this.fb.group({
    bicycletype: ['', [Validators.required]],
    hub: ['', [Validators.required]]
  });


  getHubId(event: number) {
    console.log(event);
    this.hubId = event;
    this.form.controls.hub.patchValue(this.hubId);
    // this.form.controls.(this.hubId);
    console.log(this.hubId);
    console.log(this.form.get('hub'));


  }
  getBicycleId(event) {
    console.log(event);
    this.typeId = event;
    this.form.controls.bicycletype.patchValue(this.typeId);

  }
  getLatLng(event) {
    console.log(event);
    this.latlng = event;

  }
  getAddress(event) {
    console.log(event);
    this.address = event;

  }
  openVerticallyCentered(content) {
    this.modalService.open(content, { centered: true });
  }
  // routetomap()
  // {
  //   console.log("click");

  //   this.router.navigate(['/booking/map']);
  // }
  // routetobike(){
  //   this.router.navigate(['/showBikeType']);
  // }
  ngOnInit() {

    this.bikeTypeService.getAllBicycleType().subscribe(res => {
      this.bicycleTypes = res;
      console.log(this.bicycleTypes);
    }, error => {
      console.log('error bike type error' + error.message);
    });

    this.mapService.getHubPoint().subscribe(HubRes => {
      console.log(HubRes);
      this.hubs = HubRes;
      this.mapService.publish(this.hubs);
    }, error => {
      console.log('error hub type error' + error.message);
    })
  }
  changeHub(event: string) {
    this.hubId = Number(event);
    console.log(this.hubId);
    this.bikeTypeService.getBicycleType(this.hubId).subscribe(
      res => {
        this.bicycleTypes = res;
      }
      ,
      error => {
        console.log(error);

      }
    )

    // this.bicycleTypes=this.bicycleTypes.filter((bicycleType)=>{
    //   bicycleType.hubId=this.hubId}
    //   );
    //   console.log(this.bicycleTypes);

  }
  changeBicycleTypes(event: string) {
    this.typeId = Number(event);
    this.form.controls[' hub'].setValue(event);
    console.log(this.form);

  }
  checkBook() {
    console.log(this.hubId);
    console.log(this.typeId);


    if (this.hubId == null || this.typeId == null) {
      return false;
    }
    return true;
  }
  onBook() {
    this.booking.hubId=this.hubId;
    this.booking.typeId=this.typeId;
    this.booking.userName=JSON.parse(this.userService.getUser()).userName;
    this.booking.bicycleNumber='';
    this.booking.deliveryAddress=this.address;
    this.booking.latitude=this.latlng.Latitude;
    this.booking.longitude=this.latlng.Longitude;
    this.booking.bookingId=0;

    console.log(this.hubId);
   console.log(this.typeId);
   console.log(this.booking);

   this.bookingServise.book(this.booking).subscribe(
     res=>{
        console.log(res);
        this.hubId = null;
        this.typeId = null;
        this.router.navigate(['/home']);
      },
      error => {
        console.log(error);

      }
    );

  }
}
