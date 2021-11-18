import { Equipment } from "./equipment"

export interface Room {
    roomId?: number,
    name: string,
    purpose: string,
    equipment?: Equipment[],
    doctor: string,
    x: number,
    y: number,
    height: number,
    width: number,
    doorX: number,
    doorY: number,
    vertical?:boolean,
    class?: string,
    doorExists?:boolean
}

export function emptyRoom():Room{
    return {
        roomId: -1,
        name: '',
        purpose: '',
        doctor: '',
        x: -1,
        y: -1,
        height: -1,
        width: -1,
        doorX: -1,
        doorY: -1,
        vertical:false,
        class: '',
        doorExists: false
    }
}