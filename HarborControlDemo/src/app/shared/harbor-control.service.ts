import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators'

@Injectable({
  providedIn: 'root'
})
export class HarborControlService {
 private readonly _apiKey: string  = "00448fd92b992dd8aed304eaadf5aa53";
 private readonly _cityName: string  = "durban";
 private readonly _baseApi = "http://localhost:62557/harborcontrol/";
  constructor(private http: HttpClient) { }

  // get current weather wind speed of durban  city
  getCurrentWeatherWindSpeed(){
    return this.http.get(
    "http://api.openweathermap.org/data/2.5/weather?q="+this._cityName+ "&appid=" +this._apiKey
    )
  }

  //use to get random boat related all informations
  getboatInformationDetails(boatCount:number){
    return this.http.get(this._baseApi + 'get-boat-information-details/' + boatCount)
    .pipe(
      retry(1),
      catchError(this.handleError)
  );
  }
  // handle common error and display error in console log
  handleError(error) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
        // client-side error
        errorMessage = `Error: ${error.error.message}`;
    } else {
        // server-side error
        errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
}
}
