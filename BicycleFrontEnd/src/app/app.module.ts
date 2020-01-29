import { BrowserModule } from '@angular/platform-browser';
import { NgModule, NO_ERRORS_SCHEMA} from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponentComponent } from './login/login-component/login-component.component';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MapComponent } from './map/map.component';
import { AgmCoreModule } from '@agm/core';
import { NavbarComponent } from './core/navbar/navbar.component';
import { DeliveyboyComponent } from './deliverymap/deliveyboy/deliveyboy.component';
import { MockService } from './services/mock.service';
import { UserService } from './services/user.service';
import { DeliverymapComponent } from './deliverymap/deliverymap.component';
import { ContentComponent } from './shared/content/content.component';
import { FooterComponent } from './shared/footer/footer.component';
import { HomeComponent } from './home/home.component';
import { SignupComponent } from './login/signup/signup.component';
import { BookingComponent } from './booking/booking.component';
import { TemoDirective } from './temo.directive';
import { UserComponent } from './home/user/user.component';
import { AuthInterceptor } from './core/Authentication/auth.interceptor';
import { LoaderService } from './loader.service';
import { Ng4LoadingSpinnerModule } from 'ng4-loading-spinner';
import { AddBicycleComponent } from './bike/bicycle/add-bicycle/add-bicycle.component';
import { BicycleCreateComponent } from './bike/bicycle-type/bicycle-create/bicycle-create.component';
import { BicycleCardComponent } from './bike/bicycle-type/bicycle-card/bicycle-card.component';
import { BicycleDetailComponent } from './bike/bicycle-type/bicycle-detail/bicycle-detail.component';
import { CrudComponent } from './bike/crud/crud.component';
import { BicycleeditComponent } from './bike/crud/bicycleedit/bicycleedit.component';
import { BookinghistoryComponent } from './home/user/bookinghistory/bookinghistory.component';
import { UserdetailComponent } from './userdetail/userdetail.component';
import { DeliverydetailComponent } from './bike/crud/deliverydetail/deliverydetail.component';
import { DeliveryeditComponent } from './bike/crud/deliveryedit/deliveryedit.component';
import { ShowHidePasswordModule } from 'ngx-show-hide-password'


@NgModule({
  declarations: [
    AppComponent,
    LoginComponentComponent,
    MapComponent,
    NavbarComponent,
    DeliveyboyComponent,
    DeliverymapComponent,
    AddBicycleComponent,
    BicycleCreateComponent,
    BicycleCardComponent,
    BicycleDetailComponent,
    ContentComponent,
    FooterComponent,
    HomeComponent,
    SignupComponent,
    BookingComponent,
    TemoDirective,
    CrudComponent,
    UserComponent,
    BicycleeditComponent,
    BookinghistoryComponent,
    UserdetailComponent,
    DeliverydetailComponent,
    DeliveryeditComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    MatToolbarModule,
    NgbModule,
    ShowHidePasswordModule,
    Ng4LoadingSpinnerModule.forRoot() ,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyBEVydAXGqM2Y9Azo_5IlaLbSk_qGg9fgo',
      libraries: ['places']
    }),
    ToastrModule.forRoot()
    // IonicModule.forRoot()
  ],
  providers: [UserService, MockService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }],
  bootstrap: [AppComponent],
  schemas: [NO_ERRORS_SCHEMA]
})
export class AppModule { }
