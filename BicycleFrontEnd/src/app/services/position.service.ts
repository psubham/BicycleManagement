import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { IPosition } from '../deliverymap/deliveyboy/IPosition';

@Injectable({
    providedIn: 'root'
})
export class PositionService {

    private data: Subject<IPosition> = new Subject<IPosition>();

    constructor() { }

    publish(newData: IPosition) {
        this.data.next(newData);
    }

    getData(): Observable<IPosition> {
        return this.data.asObservable();
    }
}
