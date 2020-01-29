import { Component, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { IsloginService } from '../../services/Islogin.service';
import { Iuser } from 'src/app/shared/model/login';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  title: string;
 role:string;
  userName: string;
  isLoggedIn: string;
  constructor(private routes: Router,
              private userService: UserService,
              private islogin: IsloginService
  ) {
    this.role = 'default';
  }

  ngOnInit() {
    console.log(this.role);
    if (this.userService.getUser() == null) {
      this.role = 'default';
    } else {
      var user = JSON.parse(this.userService.getUser())
      this.role = user.role;
      this.userName = user.userName;
    }
    this.islogin.getData().subscribe(
      res=>{
        this.role=res.role;
        this.userName=res.userName;
      }
    );

    console.log(this.role);

  }

  ngOnChanges(changes: SimpleChanges) {
    if (this.userService.getUser() == null) {
      this.role = 'default';
    } else {
      let user = JSON.parse(this.userService.getUser())
      this.role = user.role;
      this.userName = user.userName;
    }
    //  this.isLoggedIn=true;
    console.log(this.isLoggedIn);
  }
  onLogout() {
    localStorage.removeItem('user');
    this.role = 'default';
    this.routes.navigate(['/home']);
  }
  isLogin() {
    if (this.role === 'default') {
      console.log(true);

      return false;
    }
    return true;
  }

}
