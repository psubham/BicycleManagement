import { Component, OnInit } from '@angular/core';
import { Hub } from 'src/app/map/hub';
import { MapService } from 'src/app/map/map.service';

import { FormBuilder, Validators } from "@angular/forms";
import { ToastrService } from 'ngx-toastr';
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';
import { IBicycleType } from 'src/app/shared/model/IBicycleType';
import { IBicycle } from 'src/app/shared/model/IBicycle';
import { BicycleTypeService } from '../../services/bicycletype.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-add-bicycle',
  templateUrl: './add-bicycle.component.html',
  styleUrls: ['./add-bicycle.component.css']
})
export class AddBicycleComponent implements OnInit {

  hubs: Array<Hub>;
  lat: any;
  lng: any;
  bicycleTypes: Array<IBicycleType>;
  bicycle: IBicycle;
  flag: boolean;
  opt1:boolean;
  opt2:boolean;
  constructor(private bikeTypeService: BicycleTypeService,
    private mapService: MapService,
    private spinnerService: Ng4LoadingSpinnerService,
    private toastr: ToastrService,
    private route:Router,
    public fb: FormBuilder) {
    this.bicycle = {
      BicycleId: 0,
      TypeId: this.form.value.TypeId,
      HubId: this.form.value.HubId,
      IsRent: false,
      Latitude: 0.0,
      Longitude: 0.0,
      BicycleNumber: " "
    }
    this.hubs = [];
    this.bicycleTypes = [];
    this.flag = false;
    this.opt1=false;
    this.opt2=false;
  }
  form = this.fb.group({
    bicycletype: ['', [Validators.required]],
    hub: ['', [Validators.required]],
    number: ['', [Validators.required]]
  })

  ngOnInit() {
    this.spinnerService.show();
    this.bikeTypeService.getAllBicycleType().subscribe(res => {
      this.bicycleTypes = res;
    }, error => {
      console.log("error bike type error" + error.message);
    });

    this.mapService.getHubPoint().subscribe(HubRes => {
      console.log(HubRes);
      this.hubs = HubRes;
    }, error => {
      console.log("error hub type error" + error.message);
    },()=>{
      this.spinnerService.hide
    })
  }
  changeHub(event: string) {
    this.bicycle.HubId = Number(event);
    this.opt1=true;
  }
  changeBicycleTypes(event: string) {
    this.bicycle.TypeId = Number(event);
    this.opt2=true;
  }
  onAddBike() {
    this.flag = true;
    console.log(this.bicycle);
    this.spinnerService.show();
    this.bikeTypeService.postBicycle(this.bicycle).subscribe(
      res => {
        this.toastr.success('new Bicycle type created', 'Bicycle Added');
        this.route.navigateByUrl('/home');
      }, error => {
        this.toastr.error(error.message, 'Bicycle cycle cannot be added')
      },()=>{
        this.spinnerService.hide();
      }

    );

  }
}
