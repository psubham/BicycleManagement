import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IUserDetails } from '../shared/model/IUserDetails';

@Injectable({
    providedIn: 'root'
})
export class DeliveryService {
    url: string;
    headers = {
        headers: new HttpHeaders({
            'Content-Type': 'application/octet-stream',
            'Cache-Control': 'no-cache'
        })
    };
    headerJson = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Cache-Control': 'no-cache',
        })
    };

    constructor(private httpClient: HttpClient) {
        this.url = 'http://localhost:58079';
    }
    getUserRole() {
        return this.httpClient.get<IUserDetails[]>(this.url + '/api/ApplicationUser/GetUserRoles');
    }
    setUserRole(username: string, role: string, newrole: string) {
        return this.httpClient.post<IUserDetails[]>(this.url + '/api/ApplicationUser/SetUserRole',
        { 'userName': username, 'role': role, 'newRole': newrole });
    }

}