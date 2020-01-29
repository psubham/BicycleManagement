import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { IBicycleType } from '../../../shared/model/IBicycleType';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';
import { BicycleTypeService } from '../../services/bicycletype.service';

@Component({
  selector: 'app-bicycle-create',
  templateUrl: './bicycle-create.component.html',
  styleUrls: ['./bicycle-create.component.css']
})
export class BicycleCreateComponent implements OnInit {

  form: FormGroup;
  bicycle: IBicycleType;
  imageUrl: string = "/assets/Images/bikeicon.jpg";
  fileToUpload: File = null;
  flag: boolean;
  constructor(
    private fb: FormBuilder,
    private bicycleService: BicycleTypeService,
    private toastr: ToastrService,
    private router: Router,
    private spinnerService: Ng4LoadingSpinnerService
  ) {
    this.flag = false;
  }

  ngOnInit() {
    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(20)]],
      description: ['', [Validators.required, Validators.minLength(10)]],
      imageUrl: ['', [Validators.required, Validators.pattern(/([a-z\-_0-9\/\:\.]*\.(jpg|jpeg|png|gif))/i)]],
      type: ['', [Validators.required]]
    });
  }

  handleFileInput(file: FileList) {
    this.fileToUpload = file.item(0);
    console.log(this.fileToUpload);

    //Show image preview
    var reader = new FileReader();
    reader.onload = (event: any) => {
      this.imageUrl = event.target.result;
    }
    reader.readAsDataURL(this.fileToUpload);
  }


  createBicycle() {
    this.flag = true;
    console.log(this.form.value);
    this.bicycle = {
      name: this.form.value.name,
      description: this.form.value.description,
      imageUrl: '',
      type: this.form.value.type,
    }

    console.log(this.bicycle);
    this.spinnerService.show();
    this.bicycleService.uploadImage(this.fileToUpload).subscribe(
      (res: any) => {

        this.bicycle.imageUrl = res.toString();
        console.log(this.bicycle);
        this.bicycleService.postBicycleType(this.bicycle).subscribe(
          event => {
            this.toastr.success('new Bicycle type created', 'Bicycle Added');

          },err=>{
            this.toastr.error(err.message,'Bicycle cycle cannot be added')
          }
        )
        this.form.reset();
        this.flag=false;

      }, error => {
        console.log(error);

        this.toastr.error(error.message, 'Bicycle image cannot be added')
      }, () => {
        this.spinnerService.hide();
      }
    )

  }

  get f() { return this.form.controls; }

}
