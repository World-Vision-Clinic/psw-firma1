import { Equipment } from './equipment';

export interface Room {
  id?: number;
  name: string;
  purpose: string;
  equipments?: Equipment[];
  doctorId: number;
  x: number;
  y: number;
  height: number;
  width: number;
  doorX: number;
  doorY: number;
  vertical?: boolean;
  css?: string;
  doorExist?: boolean;
}

export function emptyRoom(): Room {
  return {
    id: -1,
    name: '',
    purpose: '',
    doctorId: -1,
    x: -1,
    y: -1,
    height: -1,
    width: -1,
    doorX: -1,
    doorY: -1,
    vertical: false,
    css: '',
    doorExist: false
  };
}
