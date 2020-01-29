import { Component, OnInit, OnDestroy } from '@angular/core';
import { NgForm } from '@angular/forms';

import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../services/user.service';
import { IsloginService } from '../../services/Islogin.service';
import { IuserCredential } from '../../shared/IuserCredential';
import { userCredentialService } from '../../services/userCredential.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-login-component',
  templateUrl: './login-component.component.html',
  styleUrls: ['./login-component.component.css']
})
export class LoginComponentComponent implements OnInit, OnDestroy {

  formModel = {
    UserName: '',
    Password: ''
  };
  loginSubscription: Subscription;
  passwordShown = false;
  Islogin: boolean;
  userCredential = {} as IuserCredential;
  constructor(private service: UserService,
              private routes: Router,
              private toastr: ToastrService,
              private isLogin: IsloginService,
              private usercredentialService: userCredentialService
  ) {
    this.Islogin = false;
  }

  ngOnInit() {
    if (this.service.getUser() != null) {
      this.routes.navigateByUrl('/home');
    }

  }
  btnClick() {
    this.routes.navigateByUrl('/register');
  }
  onSubmit(form: NgForm) {
    console.log(this.formModel);
    console.log(this.formModel.Password);
    this.loginSubscription = this.service.login(this.formModel).subscribe(
      (res: any) => {
        console.log('done');
        console.log(res);
        this.service.setUser(res);
        this.isLogin.publish(res);
        this.routes.navigate(['/home']);
      },
      err => {
        
          this.toastr.error(err.error, 'Authentication failed');
        
      }
    );

  }
  ngOnDestroy() {
    if (this.loginSubscription) {
      this.loginSubscription.unsubscribe();
    }
  }

}
