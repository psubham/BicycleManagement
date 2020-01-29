import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IDelivery } from '../deliverymap/deliveyboy/Delivery';

@Injectable({
  providedIn: 'root'
})
export class MockService {
  url = 'http://localhost:64966/';

  constructor(private http: HttpClient) {

  }
  headerJson = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Cache-Control': 'no-cache'
    })
  };
  getDeliveries() {
    return this.http.get<Array<IDelivery>>(this.url + 'api/DeliveryBike/GetDeliveries/');
  }
  getDelivery(deliveryid: number) {
    return this.http.post<IDelivery>(this.url + 'api/DeliveryBike/GetDelivery/', deliveryid, this.headerJson);
  }
  changeStatus(status: string, deliveryid: number) {
    return this.http.put<boolean>(this.url + 'api/DeliveryBike/ChangeStatus/'+deliveryid,  `"${status}"`,this.headerJson);
  }
  getDeliveryConfirmation(id:number)
  {
    console.log(id);

    return this.http.get<string>(this.url + 'api/DeliveryBike/IsConfirm/'+id);
  }
}
