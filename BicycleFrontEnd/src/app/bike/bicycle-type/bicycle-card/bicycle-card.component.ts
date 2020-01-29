import { Component, OnInit, Input } from '@angular/core';
import{ IBicycleType} from '../../../shared/model/IBicycleType';

@Component({
  selector: 'app-bicycle-card',
  templateUrl: './bicycle-card.component.html',
  styleUrls: ['./bicycle-card.component.css']
})
export class BicycleCardComponent  {

  @Input() bicycle:IBicycleType;

}
