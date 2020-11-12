export class Device {
  type: string;
  serialNumber: string;
  firmwareVersion: string;
  state: number;
  ip: string;
  port: number;
}

export class BaseResponse {
  valid: boolean;
  errorMessages: string[];
}
