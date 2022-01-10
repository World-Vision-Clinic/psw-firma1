import { DateTime } from '@syncfusion/ej2-charts';
import { Doctor } from './doctor';

export interface OnCallShift {
  id: number;
  doctor: Doctor;
  date: Date;
  new?: boolean;
}
