import { Component, OnInit, Input, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { IBicycleType } from '../../../shared/model/IBicycleType';
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';
import { BicycleTypeService } from '../../services/bicycletype.service';

@Component({
  selector: 'app-bicycle-detail',
  templateUrl: './bicycle-detail.component.html',
  styleUrls: ['./bicycle-detail.component.css']
})
export class BicycleDetailComponent implements OnInit {

  @Input() hubid: number;
  @Output() SelectBicycleId = new EventEmitter<number>();
  currentcycle: number;
  bicycles: Array<IBicycleType>;
  constructor(private bikeService: BicycleTypeService,
              private spinnerService: Ng4LoadingSpinnerService) {
    this.bicycles = [];
    this.currentcycle = -1;
  }
  checknumber() {
    if (this.bicycles.length > 0) {
      return true;
    }
    return false;
  }
  ngOnInit() {
    console.log(this.hubid);
    this.spinnerService.show();
    if (this.hubid == null) {
      this.bikeService.getAllBicycleType().subscribe(
        res => {
          this.bicycles = res;
          console.log(this.bicycles);
        },
        error => {
          console.log('error');
        }, () => {

          this.spinnerService.hide();
        }
      );
    } else {
      this.bikeService.getBicycleType(this.hubid).subscribe(
        res => {
          this.bicycles = res;
          console.log(this.bicycles);

        },
        error => {
          console.log('error');
        }, () => {
          this.spinnerService.hide();
        }
      );
      console.log(this.bicycles);

    }

  }
  ngOnChanges(changes: SimpleChanges) {
    if (this.hubid === null) {
      this.bikeService.getAllBicycleType().subscribe(
        res => {
          this.bicycles = res;
          console.log(this.bicycles);
        },
        error => {
          console.log('error');
        }
      );
    }
    else {
      this.bikeService.getBicycleType(this.hubid).subscribe(
        res => {
          this.bicycles = res;
          console.log(this.bicycles);
        },
        error => {
          console.log("error");
        }
      );
      console.log(this.bicycles);
    }




  }
  isBicycleThere() {
    if (this.bicycles.length == 0)
      return false;
    else
      return true;
  }
  SelectBicycle(bicycleTypeId: number) {
    this.currentcycle = bicycleTypeId;
    console.log(bicycleTypeId);
    this.SelectBicycleId.emit(bicycleTypeId);
  }


}
