import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  public devices: Device[] = [];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.loadData();
  }

  loadData() {
    this.http.get<Device[]>(this.baseUrl + 'api/ElectricMeter').subscribe(result => {
      this.devices = this.devices.concat(result.map(x => { return { type: "Electric Meter", ...x } }));
    }, error => console.error(error));

    this.http.get<Device[]>(this.baseUrl + 'api/WaterMeter').subscribe(result => {
      this.devices = this.devices.concat(result.map(x => { return { type: "Water Meter", ...x } }));
    }, error => console.error(error));

    this.http.get<Device[]>(this.baseUrl + 'api/Gateway').subscribe(result => {
      this.devices = this.devices.concat(result.map(x => { return { type: "Gateway", ...x } }));
    }, error => console.error(error));
  }
}

interface Device {
  type: string;
  serialNumber: string;
  firmwareVersion: string;
  state: number;
  ip: string;
  port: number;
}
