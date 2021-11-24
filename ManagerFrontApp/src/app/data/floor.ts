import { Room } from "./room";

export interface Floor{
    level: string,
    info?: string,
    rooms: Room[],
    labels?: FloorLabel[],
    highlightedRoomId?: number
}


export interface FloorLabel{
    x: number,
    y: number,
    text: string
}