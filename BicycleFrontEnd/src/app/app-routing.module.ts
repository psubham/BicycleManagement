import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponentComponent } from './login/login-component/login-component.component';
import { MapComponent } from './map/map.component';
import { AuthGuard } from './core/Authentication/auth.guard';
import { NavbarComponent } from './core/navbar/navbar.component';
import { DeliveyboyComponent } from './deliverymap/deliveyboy/deliveyboy.component';
import { SignupComponent } from './login/signup/signup.component';
import { HomeComponent } from './home/home.component';
import { BookingComponent } from './booking/booking.component';
import { UserComponent } from './home/user/user.component';
import { DeliverymapComponent } from './deliverymap/deliverymap.component';
import { BicycleDetailComponent } from './bike/bicycle-type/bicycle-detail/bicycle-detail.component';
import { AddBicycleComponent } from './bike/bicycle/add-bicycle/add-bicycle.component';
import { BicycleCreateComponent } from './bike/bicycle-type/bicycle-create/bicycle-create.component';
import { CrudComponent } from './bike/crud/crud.component';
import { BicycleeditComponent } from './bike/crud/bicycleedit/bicycleedit.component';
import { BookinghistoryComponent } from './home/user/bookinghistory/bookinghistory.component';
import { DeliveryeditComponent } from './bike/crud/deliveryedit/deliveryedit.component';
import { DeliverydetailComponent } from './bike/crud/deliverydetail/deliverydetail.component';



const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'booking', component:BookingComponent },
  { path: 'register', component: SignupComponent },
  { path: 'map', component: MapComponent, canActivate: [AuthGuard] },
  { path: 'showBikeType', component: BicycleDetailComponent },
  { path: 'login', component: LoginComponentComponent },
  { path: 'navbar', component: NavbarComponent },
  // { path: 'delivery', component: DeliveyboyComponent },
  // { path: 'map', component: MapComponent, canActivate: [AuthGuard] },
  { path: 'home', component: HomeComponent },
  // { path: 'showBikeType', component: BicycleDetailComponent },
  { path: 'Add Bike', component: AddBicycleComponent },
  { path: 'Add Bike Type', component: BicycleCreateComponent },
  { path: 'deliveryBoy', component: DeliveyboyComponent },

  { path: 'bookingHistory', component: BookinghistoryComponent },
  { path: 'user', component: UserComponent },
  { path: 'crud', component: CrudComponent },
  { path: 'deliverymap', component: DeliverymapComponent },
  { path: 'editbicycle', component: BicycleeditComponent },
  { path: 'editdelivery', component: DeliveryeditComponent },
  { path: 'deliverydetail', component: DeliverydetailComponent }
  // { path: 'editbicycle', component: EditBicycleComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
