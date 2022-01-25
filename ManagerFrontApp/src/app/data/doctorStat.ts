import { Doctor } from './doctor';

export interface DoctorStat {
  doctor: Doctor;
  numberOfAppointments: number;
  numberOfOnCallShifts: number;
  numberOfPatients: number;
  numberOfVacationDays: number;
}
