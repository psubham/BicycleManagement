import { Component, OnInit, NgZone, OnDestroy } from '@angular/core';
import { MockService } from '../../services/mock.service';
import { Router } from '@angular/router';
import { IPosition } from './IPosition';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IDelivery } from './Delivery';
import { PositionService } from '../../services/position.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-deliveyboy',
  templateUrl: './deliveyboy.component.html',
  styleUrls: ['./deliveyboy.component.css']
})
export class DeliveyboyComponent implements OnInit, OnDestroy {

  cancelSubscription: Subscription;
  changeStatusonSubscription: Subscription;
  title: string;
  users: IDelivery[];
  currentUser: IDelivery;
  status: boolean;
  modalclose: boolean;
  position: IPosition;
  page: number;
  pagesize: number;
  constructor(private mockService: MockService,
    private positionService: PositionService,
    private route: Router) {
    this.page = 1;
    this.pagesize = 4;
    this.status = false;
    console.log(this.users);

  }

  ngOnInit() {
    this.mockService.getDeliveries().subscribe((res: IDelivery[]) => {
      this.users = res;
      console.log('a', this.users);

    })
  }
  OnConfirm(user: IDelivery) {
    this.changeStatusonSubscription = this.mockService.changeStatus('confirm', user.deliveryId).subscribe(
      res => {
        console.log(res);

        this.route.navigate(['/deliverymap', { 'id': user.deliveryId }]);
      },
      error => {
        console.log(error);

      }
    );

  }
  OnCancel(user: IDelivery) {

    this.cancelSubscription = this.mockService.changeStatus('cancel', user.deliveryId).subscribe(
      res => {
        this.users = this.users.filter(x => x !== user);
        console.log(res);
      },
      error => {
        console.log(error);

      }
    );

  }
  ngOnDestroy() {
    if (this.cancelSubscription) {
      this.cancelSubscription.unsubscribe();
    }
    if (this.changeStatusonSubscription) {
      this.changeStatusonSubscription.unsubscribe();
    }
  }



}
