import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, Validators, FormControl } from '@angular/forms';
import { Device, BaseResponse } from '../../models/models';

@Component({
  selector: 'app-add-device',
  templateUrl: './add-device.component.html',
  styleUrls: ['./add-device.component.css']
})
export class AddDeviceComponent {

  device: Device = new Device();
  deviceForm;
  gatewayFieldsEnabled: boolean = false;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private formBuilder: FormBuilder) {
    this.deviceForm = this.formBuilder.group({
      type: new FormControl(this.device.type, [Validators.required]),
      serialNumber: new FormControl(this.device.serialNumber, [
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(50),
      ]),
      firmwareVersion: new FormControl(this.device.firmwareVersion, [
        Validators.pattern('(\[0-9]+)\.(\[0-9]+)'),
        Validators.maxLength(20)
      ]),
      state: new FormControl(this.device.state),
      ip: new FormControl(this.device.ip, [
        Validators.pattern('(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)')
      ]),
      port: new FormControl(this.device.port, [
        Validators.min(0),
        Validators.max(65535),
      ]),
    });
  }

  enableGatewayFields() {
    this.gatewayFieldsEnabled = true;
  }

  disableGatewayFields() {
    this.gatewayFieldsEnabled = false;
  }

  onChangeType(value) {
    value == "Gateway" ? this.enableGatewayFields() : this.disableGatewayFields();
  }

  onSubmit(deviceData) {
    this.http.post<BaseResponse>(this.baseUrl + 'api/' + deviceData.type, deviceData).subscribe(result => {
      debugger;
      alert('Device added!')
      this.deviceForm.reset();
    }, result => {
      debugger;
      alert('Something went wrong:\n' + result.error.errorMessages.join('\n'));
    });
  }
}
