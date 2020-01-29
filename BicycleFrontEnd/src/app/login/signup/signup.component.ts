import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  tokenFromUI = '0123456789012345';

  constructor(public userservice: UserService,
              private tostr: ToastrService,
              private routes: Router,
              private fb: FormBuilder,
              private http: HttpClient) { }
  formModel = this.fb.group({
    UserName: ['', Validators.required],
    // tslint:disable-next-line: max-line-length
    Email: ['', [Validators.required, Validators.email, Validators.pattern('^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$')]],
    FirstName: ['', Validators.required],
    LastName: [''],
    Password: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(6), Validators.pattern('((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,30})')]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.comparePasswords })

  });
  comparePasswords(fb: FormGroup) {
    const cnfrmpassword = fb.get('ConfirmPassword');
    

    if (cnfrmpassword.errors == null || 'psswdmismatch' in cnfrmpassword.errors) {
   
      if (fb.get('Password').value != cnfrmpassword.value) {
        cnfrmpassword.setErrors({ psswdmismatch: true });
      } else {
        cnfrmpassword.setErrors(null);
      }
    }
  }

  ngOnInit() {
    this.formModel.reset();
  }
  onSubmits() {
    console.log(this.formModel);
    const body = {
      UserName: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      FirstName: this.formModel.value.FirstName,
      LastName: this.formModel.value.LastName,
      Password: this.formModel.value.Password.Password
    };
    this.userservice.signupUser(body).subscribe(
      (res: any) => {
        console.log(res);

        if (res.succeeded) {
          this.formModel.reset();
          this.tostr.success('New User created', 'Registration Success');
          this.routes.navigateByUrl('/login');
        } else {
          res.errors.forEach(element => {

            // console.log(element.code);

            switch (element.code) {
              case 'DuplicateUserName':
                this.tostr.error(element.description, 'Registrations failed');
                break;

              default:
                this.tostr.error('server error', 'Registration failed');
                break;
            }

          });
        }
      },
      err => {
        this.tostr.error('server error', err.Message);
      }
    );
  }
}
