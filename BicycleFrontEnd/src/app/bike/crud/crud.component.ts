import { Component, OnInit, OnDestroy } from '@angular/core';
import { IBicycle } from '../../shared/model/IBicycle';
import { Route, Router } from '@angular/router';
import { BicycleTypeService } from '../services/bicycletype.service';

@Component({
  selector: 'app-crud',
  templateUrl: './crud.component.html',
  styleUrls: ['./crud.component.css']
})
export class CrudComponent implements OnInit {
  

  page: number;
  pagesize: number;
  bicycles: Array<IBicycle>
  constructor(
    private bicycleService: BicycleTypeService, private route: Router
  ) {
    this.bicycles = []
    this.page = 1;
    this.pagesize = 4;
  }
  
  ngOnInit() {
    this.bicycleService.getAllBicycle().subscribe(
      res => {
        this.bicycles = res;
        console.log(this.bicycles);
      }
    );
  }
  
  deleteBicycle(bicycle: IBicycle) {
   var del= this.bicycleService.deleteBicycle(bicycle).subscribe(
      res => {
        this.bicycles = this.bicycles.filter(x => x !== bicycle);
        console.log(this.bicycles);
      },
      err => {
        console.log('error occured', err);
      }
    );

  }
  addBicycle() {
    this.route.navigate(['Add Bike']);
  }
  editBicycle(bicycle: any): void {
    this.route.navigate(['editbicycle/', { 'id': bicycle.bicycleId }]);
  };
  
}
