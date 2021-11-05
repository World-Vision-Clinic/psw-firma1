import { Floor } from "./floor";

export interface Building{
    name: string,
    info?: string,
    floors: Floor[],
    area?: Area
}


export interface Area{
    x: number,
    y: number,
    width: number,
    height: number
}