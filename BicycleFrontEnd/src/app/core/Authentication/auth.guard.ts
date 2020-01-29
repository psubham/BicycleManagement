import { Injectable, OnInit } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { userCredentialService } from '../../services/userCredential.service';
import { IuserCredential } from '../../shared/IuserCredential';
import { Iuser } from '../../shared/model/login';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate, OnInit {
  usercredential = {} as IuserCredential;

  user = {} as Iuser;
  constructor(private router: Router,
    private userCredentialService: userCredentialService,
    private userService: UserService
  ) { }
  ngOnInit() {

  }
  canActivate(

    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {

    console.log("activate");
    let status = false;
    this.user = JSON.parse(this.userService.getUser());
    if (this.user != null)
      return true;
    else {
      this.router.navigate(['/login']);
      return false;
    }
  }
}
