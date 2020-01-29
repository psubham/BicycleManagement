import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { Iuser } from '../shared/model/login';

@Injectable({
    providedIn: 'root'
})
export class IsloginService {

    private data: Subject<Iuser> = new Subject<Iuser>();

    constructor() { }

    publish(newData: Iuser) {
        this.data.next(newData);
    }

    getData(): Observable<Iuser> {
        return this.data.asObservable();
    }
}
