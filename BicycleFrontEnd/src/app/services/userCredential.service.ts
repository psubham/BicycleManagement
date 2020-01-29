import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class userCredentialService {

    private data: Subject<string> = new Subject<string>();

    constructor() { }

    publish(newData: string) {
        this.data.next(newData);
    }

    getData(): Observable<string> {
        return this.data.asObservable();
    }
}
