
import { Component, OnInit, Input, Output, EventEmitter, SimpleChanges, AfterViewInit } from '@angular/core';
import { MouseEvent } from '@agm/core';
import { ViewChild, ElementRef, NgZone } from '@angular/core';
import { MapsAPILoader } from '@agm/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Hub } from './hub';
import { MapService } from './map.service';
import { ToastrService } from 'ngx-toastr';
import { IPosition } from '../deliverymap/deliveyboy/IPosition';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit,AfterViewInit {
  @Input() hubs: Array<Hub>;
  @Input() action: string;
  @Output() getClickHub = new EventEmitter<number>();
  @Output() getUserAddress = new EventEmitter<Hub>();
  @Output() getlatlng=new EventEmitter<IPosition>();
  @Output() getaddress=new EventEmitter<string>();

  private latitude: number;
  private longitude: number;
  private zoom: number;
  private geoCoder;
  private geocoderAddress = {};
  private title: string;
  private hubSelected: boolean;
  private latlng={}as IPosition;
  private address:string;


  @ViewChild('search', { static: true })
  public searchElementRef: ElementRef;

  constructor(
    private mapsAPILoader: MapsAPILoader,
    private ngZone: NgZone,
    private routes: Router,
    private mapService: MapService,
    private tostr: ToastrService,
    private router: Router,
    private route:ActivatedRoute
  ) {
    this.hubs = [];
    this.latitude = 0.0;
    this.longitude = 0.0;
    this.zoom = 0;
    // this.action = "createHub";
    this.hubSelected = false;
  }
  async ngOnInit() {
    //load Places Autocomplete
    this.title = "Add New Hub";
    // this.loadMap();
    // this.hubSelected = false;
  }

  ngAfterViewInit(){
    this.loadMap();
    this.hubSelected = false;
  }

  ngOnChanges(changes: SimpleChanges) {

  }
  loadMap() {
    this.mapsAPILoader.load().then(() => {
      this.setCurrentLocation();
      this.geoCoder = new google.maps.Geocoder;
      let autocomplete = new google.maps.places.Autocomplete(this.searchElementRef.nativeElement, {
        types: ["address"]
      });
      autocomplete.addListener("place_changed", () => {
        this.ngZone.run(() => {
          //get the place result
          let place: google.maps.places.PlaceResult = autocomplete.getPlace();
          //verify result
          if (place.geometry === undefined || place.geometry === null) {
            return;
          }
          //set latitude, longitude and zoom
          this.latitude = place.geometry.location.lat();
          this.longitude = place.geometry.location.lng();
          this.zoom = 15;
        });
      });

      this.isActionNull();
      console.log(this.hubs);
    });
  }


  isActionNull() {
    console.log("nullaction");
    console.log(this.action);

    if (this.action == null) {
      this.action=this.route.snapshot.paramMap.get('action');
      console.log(this.action);

      if(this.action==null)
        this.action='visit';
      // this.action = "createHub";
    }
     if (this.action.toString() === "createHub") {
          console.log("hubs");
          this.getAllHubDetails();
    }
  }

  getAllHubDetails() {
    this.mapService.getHubPoint().subscribe(
      result => {
        this.hubs = result;

    console.log(this.hubs);
      },
      error => {
        console.log(error);
      }
    );
  }

  // Get Current Location Coordinates
  private setCurrentLocation() {
    if ('geolocation' in navigator) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.latitude = position.coords.latitude;
        this.longitude = position.coords.longitude;
        this.zoom = 15;
        this.getAddress(this.latitude, this.longitude);
      });
    }
  }

  markerDragEnd($event: MouseEvent) {
    // console.log($event);
    this.latitude = $event.coords.lat;
    this.longitude = $event.coords.lng;
  }

  getAddress(latitude, longitude) {
    this.geoCoder.geocode({ 'location': { lat: latitude, lng: longitude } }, (results, status) => {
      // console.log(results);
      this.geocoderAddress = results;
      // console.log(status);
      if (status === 'OK') {
        if (results[0]) {
          this.zoom = 15;

          this.address=results[0].formatted_address;
        } else {
          window.alert('No results found');
        }
      } else {
        window.alert('Geocoder failed due to: ' + status);
      }
    });
  }

  clickedMarker(hubId: number) {
    this.getClickHub.emit(hubId);
    this.latlng.Latitude=this.latitude;
    this.latlng.Longitude=this.longitude;
    console.log(this.latlng);

    this.getlatlng.emit(this.latlng);
    console.log(this.address);

    this.getaddress.emit(this.address);
    // this.router.navigate(['/booking']);
    // console.log(i);
  }

  filterAddress() {
    let positonDetail={} as Hub;
    const temporary_address_components_length = this.geocoderAddress[0].address_components.length;
    let temporaryAddressComponent;
    for (let i = temporary_address_components_length - 1; i >= 0; i--) {
      temporaryAddressComponent = this.geocoderAddress[0].address_components[i];
      if (temporaryAddressComponent.types.indexOf('postal_code') > -1) {
        positonDetail.postal = temporaryAddressComponent.long_name;
        // console.log(temporary_address_component.long_name);
      } else if (temporaryAddressComponent.types.indexOf('country') > -1) {
        positonDetail.country = temporaryAddressComponent.long_name;
        positonDetail.short_country = temporaryAddressComponent.short_name;
      } else if (temporaryAddressComponent.types.indexOf('locality') > -1) {
        positonDetail.locality = temporaryAddressComponent.long_name;
        positonDetail.short_locality = temporaryAddressComponent.short_name;
      } else if (temporaryAddressComponent.types.indexOf('sublocality_level_1') > -1) {
        positonDetail.sub_locality_l1 = temporaryAddressComponent.long_name;
        positonDetail.short_sub_locality_l1 = temporaryAddressComponent.short_name;
      } else if (temporaryAddressComponent.types.indexOf('sublocality_level_2') > -1) {
        positonDetail.sub_locality_l2 = temporaryAddressComponent.long_name;
        positonDetail.short_sub_locality_l2 = temporaryAddressComponent.short_name;
        break;
      }
    }
    positonDetail.latitude = this.latitude;
    positonDetail.longitude = this.longitude;
    return positonDetail;
  }

  createHub() {
    if(this.hubSelected==true){
      let hub={} as Hub;
      this.getAddress(this.latitude,this.longitude);
      hub=this.filterAddress();
      this.mapService.postHubPoint(hub).subscribe(
        () => {
          this.tostr.success('new hub created', 'Hub Added');
        },
        error => {
          this.tostr.error(error, 'Hub Additon failed');
        }
      );
      console.log(hub);
      this.hubs.push(hub);

    }
    this.hubSelected=false;
  }

  AddHub(){
    this.hubSelected = true;
  }
}
