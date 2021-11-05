import { Floor } from "./floor";

export interface Building{
    name: string,
    info?: string,
    floors: Floor[],
    area?: Area,
    mapPosition: MapPosition
}


export interface Area{
    x: number,
    y: number,
    width: number,
    height: number
}

export interface MapPosition{
    x: number,
    y: number,
    height: number,
    width: number,
    doors?: OutsideDoor[]
}

export interface OutsideDoor{
    x: number,
    y: number,
    isVertical: boolean
}