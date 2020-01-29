import { Component, OnInit, Input, OnChanges, SimpleChange, SimpleChanges, AfterViewInit, OnDestroy } from '@angular/core';
import { MockService } from '../services/mock.service';
import { IPosition } from './deliveyboy/IPosition';
import { PositionService } from '../services/position.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IDelivery } from './deliveyboy/Delivery';
import { Route } from '@angular/compiler/src/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-deliverymap',
  templateUrl: './deliverymap.component.html',
  styleUrls: ['./deliverymap.component.css']
})
export class DeliverymapComponent implements OnInit, OnDestroy {
  deliverySubscription: Subscription
  changeStatusSubscripton: Subscription;
  deliveryDetail = {} as IDelivery;
  deliveryid: number;

  public lat;
  public lng;
  det: any;
  public origin: any;
  public destination: any;

  zoom: number;
  constructor(private positionService: PositionService,
    private route: ActivatedRoute,
    private deliverService: MockService,
    private routes: Router,
    private modalService: NgbModal) {
    this.zoom = 0;
    this.lat = 0.0;
    this.lng = 0.0;
  }


  ngOnInit() {
    this.deliveryid = Number(this.route.snapshot.paramMap.get('id'));
    console.log(this.deliveryid);
    this.deliverService.getDelivery(this.deliveryid).subscribe(res => {
      console.log(res);
      this.deliveryDetail = res;

      console.log(this.deliveryDetail);
      // this.getLocation();
    },
      error => {
        console.log(error);

      });
  }

  Ondelivery() {
   this.deliverySubscription= this.deliverService.changeStatus('deliver', this.deliveryDetail.deliveryId).subscribe(
      res => {
        console.log(res);

        this.routes.navigate(['/deliveryBoy']);
      },
      error => {
        console.log(error);

        this.routes.navigate(['/deliveryBoy']);
      }
    );
  }

  Oncancel() {
    this.changeStatusSubscripton = this.deliverService.changeStatus('cancel', this.deliveryDetail.deliveryId).subscribe(
      res => {
        console.log(res);

        this.routes.navigate(['/deliveryBoy', { 'id': this.deliveryDetail.deliveryId }]);
      },
      error => {
        console.log(error);

      }
    );

  }

  ngOnDestroy() {
    if (this.changeStatusSubscripton) {
      this.changeStatusSubscripton.unsubscribe();
    }
    if(this.deliverySubscription){
      this.deliverySubscription.unsubscribe();
    }
  }
}

