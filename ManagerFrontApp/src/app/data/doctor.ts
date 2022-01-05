import {Shift} from "../data/shift";

export interface Doctor{
    id:number,
    firstName:string,
    lastName:string,
    shiftId:number,
    roomId:number,
    type:string
}