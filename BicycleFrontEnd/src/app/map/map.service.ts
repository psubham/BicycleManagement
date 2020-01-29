import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { Hub } from './hub';
import { UserService } from '../services/user.service';

@Injectable({
  providedIn: 'root'
})
export class MapService implements OnInit {
  url: string;
  private hubsdata: Subject<Array<Hub>> = new Subject<Array<Hub>>();
  constructor(private httpClient: HttpClient, private service: UserService) {
    this.url = "http://localhost:63730/api/hub";
  }
  
  ngOnInit() {
  
  }
  getHubPoint(): Observable<Array<Hub>> {
    return this.httpClient.get<Array<Hub>>(this.url);
  }
  postHubPoint(hub: Hub) {
    return this.httpClient.post<Hub>(this.url, hub);
  }

 

  publish(newData: Array<Hub>) {
    this.hubsdata.next(newData);
  }

  getData(): Observable<Array<Hub>> {
    return this.hubsdata.asObservable();
  }
}


