import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { IBicycle } from '../../../shared/model/IBicycle';
import {first} from 'rxjs/operators';
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';
import { BicycleTypeService } from '../../services/bicycletype.service';
import { Hub } from 'src/app/map/hub';
import { MapService } from 'src/app/map/map.service';


@Component({
  selector: 'app-bicycleedit',
  templateUrl: './bicycleedit.component.html',
  styleUrls: ['./bicycleedit.component.css']
})
export class BicycleeditComponent implements OnInit {
  bicycle = {}as IBicycle;
  bicycleId: number;
  hubs: Array<Hub>;
  hubid: number;
  opt: boolean;
  constructor(private formBuilder: FormBuilder,
    private router: Router,
    private bicycleService: BicycleTypeService,
    private route: ActivatedRoute,
    private mapService : MapService,
    private spinnerService: Ng4LoadingSpinnerService

) {
  this.bicycleId = -1;
  this.hubs = [];
  this.opt = false;
}
editForm = this.formBuilder.group({
  BicycleNumber: ['', Validators.required],
  TypeId: ['', Validators.required],
  HubId: ['', Validators.required],
  IsRent: ['', Validators.required]
});

  ngOnInit() {
    this.bicycleId = Number(this.route.snapshot.paramMap.get('id'));
    console.log(this.bicycleId);

    if (this.bicycleId === -1) {
      alert('Invalid action.');
      this.router.navigate(['crud']);
      return;
    }
    this.spinnerService.show();
    this.bicycleService.GetBicycleId(this.bicycleId)
      .subscribe( (data: any) => {
        this.bicycle = data;
        this.editForm.controls.TypeId.setValue(data.typeId);
        this.editForm.controls.HubId.setValue(data.hubId);
        this.hubid = data.hubId;
        this.editForm.controls.IsRent.setValue(data.isRent);
        this.editForm.controls.BicycleNumber.setValue(data.bicycleNumber);
        console.log(this.editForm);

      });
    this.mapService.getHubPoint().subscribe(HubRes => {
        console.log(HubRes);
        this.hubs = HubRes;
      }, error => {
        console.log('error hub type error' + error.message);
      }, () => {
        this.spinnerService.hide();
      });

  }

  changeHub(event: number){

    if (event !== this.hubid) {
      this.opt = true;
      this.hubid = event;
    }
  }
  onSubmit() {
    console.log(this.editForm);
    this.bicycle.HubId = this.hubid; 
    console.log(this.bicycle);

    this.bicycleService.UpdateBicycle(this.bicycle)
      .pipe(first())
      .subscribe(
        data => {
            this.router.navigate(['crud']);
         },
        error => {
          alert(error);
        });
  }
}
