import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Md5 } from 'ts-md5/dist/md5';
import { IuserCredential } from '../shared/IuserCredential';
import { Iuser } from '../shared/model/login';
@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private fb: FormBuilder, private http: HttpClient) { }
  readonly BaseUri = 'http://localhost:58079/api';
  signupUser(body) {
    console.log(body);
    body.Password = this.encrypt(body.Password);
    console.log(body.Password);

    return this.http.post(this.BaseUri + '/ApplicationUser/Register', body);

  }
  encrypt(text: string) {
    const md5 = new Md5();
    return md5.appendStr(text).end().toString();
    // btoa(text).toString();
  }
  login(formData) {
    formData.Password = this.encrypt(formData.Password);
    console.log(formData);
    const something = this.http.post(this.BaseUri + '/ApplicationUser/Login', formData);
    return something;
  }
  isAuthenticate() {
    return this.http.get<boolean>(this.BaseUri + '/ApplicationUser/IsAuthenticate');
  }
 


  setUser(user: Iuser) {
    localStorage.setItem('user', JSON.stringify(user));
  }
  getUser() {
    return localStorage.getItem('user');
  }

  setToken(token: string) {
    localStorage.setItem('token', token);
  }
  setUserId(userId: string) {
    localStorage.setItem('userId', userId);
  }
  getToken() {
    return localStorage.getItem('token');
  }
  getUserId() {
    return localStorage.getItem('userId');
  }

}
