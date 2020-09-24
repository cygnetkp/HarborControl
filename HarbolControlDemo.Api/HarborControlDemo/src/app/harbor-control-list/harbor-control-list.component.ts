import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { BoatInformation } from '../models/boatInformation.model';
import { BoatStatusType } from '../models/boatStatusType';
import { BoatType } from '../models/boatType';
import { HarborControlService } from '../shared/harbor-control.service';

@Component({
  selector: 'app-harbor-control-list',
  templateUrl: './harbor-control-list.component.html',
  styleUrls: ['./harbor-control-list.component.css']
})
export class HarborControlListComponent implements OnInit {

  _currentWindSpeed: number = 0;
  _windSpeedTemp: number = 16;
  boatInformationList: BoatInformation[] = null;
  BoatType = BoatType;
  BoatStatus = BoatStatusType;
  _timeIntervalParam = 0;
  formCriteria: FormGroup = new FormGroup({});
  myInterval: any;
  constructor(private harborService: HarborControlService,
    private fb: FormBuilder,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.formCriteria = this.fb.group({
      boatCount: ['', [Validators.required, Validators.pattern("^[0-9]*$")]]
    });

    this.harborService.getCurrentWeatherWindSpeed().subscribe((data: any) => {
      this._currentWindSpeed = data.wind.speed.toString();
    });
  }

  // Submit boat count as input parameter and return radom boat information list
  submitData(): void {
    if (this.formCriteria.valid) {
      let _boatCount = this.formCriteria.get("boatCount").value;
      this.harborService.getboatInformationDetails(_boatCount).subscribe((data: any) => {
        if (data != null) {
          this.boatInformationList = data;
          let currentBoatInformation = this.boatInformationList.find(x => x.boatStatus == 2);
          if (this.boatInformationList && this.boatInformationList.find(x => x.boatStatus <= 2)) {
            this.myInterval = setInterval(() => {
              this.getUpdateBoatStatusDetail();
            }, currentBoatInformation.boatActualTimeDuration * 1000);
          }
        }
      });
    }
  }
  // Use for display boat current status information in the front-end
  getUpdateBoatStatusDetail() {
    clearInterval(this.myInterval);
    if (this.boatInformationList && this.boatInformationList.find(x => x.boatStatus <= 2)) {
      let inProgressBoatInformation = this.boatInformationList.find(x => x.boatStatus == 2);
      inProgressBoatInformation.boatStatus = 3;
      let queueBoatInformation = this.boatInformationList.find(x => x.boatStatus == 1);
      if (queueBoatInformation) {
        queueBoatInformation.boatStatus = 2;
        this.myInterval = setInterval(() => {
          this.getUpdateBoatStatusDetail();
        }, queueBoatInformation.boatActualTimeDuration * 1000);
      }
      else {
        if (this.boatInformationList.find(x => x.boatStatus == 0)) {
          if (this._windSpeedTemp > 10 || this._windSpeedTemp < 30) {
            let spendBoatInformation = this.boatInformationList.find(x => x.boatStatus == 0);
            if (spendBoatInformation) {
              spendBoatInformation.boatStatus = 2;
              spendBoatInformation.boatReachTimeDuration = spendBoatInformation.boatActualTimeDuration;
              this.myInterval = setInterval(() => {
                this.getUpdateBoatStatusDetail();
              }, spendBoatInformation.boatActualTimeDuration * 1000);
            }
          }
        }
        else {
          this.toastr.success("All boat reach destination successfully", "Notification");
        }
      }

    }
  }
}
