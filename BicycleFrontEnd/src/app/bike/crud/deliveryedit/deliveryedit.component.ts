import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-deliveryedit',
  templateUrl: './deliveryedit.component.html',
  styleUrls: ['./deliveryedit.component.css']
})
export class DeliveryeditComponent implements OnInit {
 rolename:string;
 opt:boolean;
  constructor() {
    this.opt=false;
   }

  ngOnInit() {
  }
  changeRole(event:string){
    if(event!=this.rolename)
    this.opt=true;
    this.rolename=event;

  }

}
