import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TypeSelectorService {
  
  private bicycleTypeData: Subject<number> = new Subject<number>();

    constructor() { }

    publish(newData: number) {
        this.bicycleTypeData.next(newData);
    }

    getData(): Observable<number> {
        return this.bicycleTypeData.asObservable();
    }
}
