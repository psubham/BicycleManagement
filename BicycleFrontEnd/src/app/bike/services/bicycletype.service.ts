import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IBicycleType } from 'src/app/shared/model/IBicycleType';
import { IBicycle } from 'src/app/shared/model/IBicycle';

@Injectable({
  providedIn: 'root'
})
export class BicycleTypeService {
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
    this.url = 'http://localhost:57207';
  }
  postBicycleType(bike: IBicycleType) {
    console.log(bike);

    return this.httpClient.post<IBicycleType>(this.url + '/api/BicycleType', bike);
  }
  getAllBicycleType() {
    return this.httpClient.get<IBicycleType[]>(this.url + '/api/BicycleType');
  }
  getAllBicycle() {
    return this.httpClient.get<IBicycle[]>(this.url + '/api/Bicycle/GetAllBicycle');
  }
  getBicycleType(hubid: number) {
    return this.httpClient.get<IBicycleType[]>(this.url + '/api/BicycleType/' + hubid);
  }
  postBicycle(bicycle: IBicycle) {
    console.log(bicycle);

    return this.httpClient.post<IBicycle>(this.url + '/api/Bicycle/AddBicycle', bicycle);
  }
  deleteBicycle(bicycle: IBicycle) {
    console.log(bicycle);
    return this.httpClient.post<boolean>(this.url + '/api/Bicycle/DeleteBicycle', bicycle);
  }
  GetBicycleId(id: number) {
    return this.httpClient.post<IBicycle>(this.url + '/api/Bicycle/GetBicycleId/', id, this.headerJson);
  }
  UpdateBicycle(bicycle: IBicycle): Observable<IBicycle> {
    console.log(bicycle);

    return this.httpClient.put<IBicycle>(this.url + '/api/Bicycle/UpdateBicycle/', bicycle);
  }
  GetType(id: number) {
    console.log(id);

    return this.httpClient.get<IBicycleType>(this.url + '/api/Bicycle/GetType/' + id);
  }

  uploadImage(files: File) {

    const formData = new FormData();
    formData.append('files', files, files.name);
    return this.httpClient.post<File>(this.url + '/api/BicycleType/upload',
      formData,
      {
        headers: new HttpHeaders().set('Content-Disposition', 'multipart/form-data'),

      }
    )
  }

}

