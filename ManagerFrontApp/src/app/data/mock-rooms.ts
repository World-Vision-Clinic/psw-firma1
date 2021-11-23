import { Room } from "./room";

export let ROOMS: Room[] = [
    {
        roomId: 1,
        name: "DOCTOR'S OFFICE 1",
        doctor: "",
        purpose: "",
        x: 100,
        y:150,
        height: 100,
        width: 150,
        doorX:200,
        doorY:248,
        vertical: false,
        class:"room",
        doorExists: true
    },
    
    {
        roomId: 2,
        name: "DOCTOR'S OFFICE 2",
        doctor: "",
        purpose: "",
        x: 260,
        y:150,
        height: 100,
        width: 150,
        doorX:360,
        doorY:248,
        vertical: false,
        class:"room",
        doorExists: true
    },
    {
        roomId: 3,
        name: "DOCTOR'S OFFICE 3",
        doctor: "",
        purpose: "",
        x: 420,
        y:150,
        width: 150,
        height: 100,
        doorX:520,
        doorY:248,
        vertical: false,
        class:"room",
        doorExists: true
    },
    {
        roomId: 4,
        name: "DOCTOR'S OFFICE 4",
        doctor: "",
        purpose: "",
        x: 580,
        y:150,
        width: 170,
        height: 100,
        doorX:695,
        doorY:248,
        vertical: false,
        class:"room",
        doorExists: true
    },
    {
        roomId: 5,
        name: "ROOM 1",
        doctor: "",
        purpose: "",
        x: 760,
        y: 150,
        width: 180,
        height: 100,
        doorX:780,
        doorY:248,
        vertical: false,
        class:"room",
        doorExists: true
    },
    {
        roomId: 6,
        name: "STAIRS",
        doctor: "",
        purpose: "",
        x: 870,
        y: 258,
        width: 70,
        height: 104,
        doorX:828,
        doorY:290,
        vertical: true,
        class:"staircase",
        doorExists: false
    },
    {
        roomId: 7,
        name: "LIFT",
        doctor: "",
        purpose: "",
        x: 790,
        y: 370,
        width: 150,
        height: 90,
        doorX:828,
        doorY:290,
        vertical: true,
        class:"staircase",
        doorExists: false
    },
    {
        roomId: 8,
        name: "TOILET",
        doctor: "",
        purpose: "",
        x: 830,
        y: 470,
        width: 110,
        height: 60,
        doorX:828,
        doorY:485,
        vertical: true,
        class:"room",
        doorExists: true
    },
    {
        roomId: 9,
        name: "TOILET",
        doctor: "",
        purpose: "",
        x: 830,
        y: 540,
        width: 110,
        height: 60,
        doorX:828,
        doorY:555,
        vertical: true,
        class:"room",
        doorExists: true
    },
    {
        roomId: 10,
        name: "OPERATING ROOM 2",
        doctor: "",
        purpose: "",
        x: 100,
        y: 410,
        width: 150,
        height: 190,
        doorX:248,
        doorY:419,
        vertical: true,
        class:"room",
        doorExists: true
    },
    {
        roomId: 11,
        name: "ROOM 2",
        doctor: "",
        purpose: "",
        x: 260,
        y: 500,
        width: 100,
        height: 100,
        doorX:300,
        doorY:498,
        vertical: false,
        class:"room",
        doorExists: true
    },
    {
        roomId: 12,
        name: "OPERATING ROOM 3",
        doctor: "",
        purpose: "",
        x: 370,
        y: 500,
        width: 300,
        height: 100,
        doorX:450,
        doorY:498,
        vertical: false,
        class:"room",
        doorExists: true
    },
    {
        roomId: 13,
        name: "OPERATING ROOM 1",
        doctor: "",
        purpose: "",
        x: 100,
        y: 300,
        height: 100,
        width: 580,
        doorX:500,
        doorY:398,
        vertical: false,
        class:"room",
        doorExists: true
    },
    {
        roomId: 14,
        name: "",
        doctor: "",
        purpose: "",
        x: 100,
        y: 252,
        width: 4,
        height: 46,
        doorX:828,
        doorY:290,
        vertical: true,
        class:"staircase",
        doorExists: false
    },
    {
        roomId: 15,
        name: "",
        doctor: "",
        purpose: "",
        x: 673,
        y: 595,
        width: 155,
        height: 4,
        doorX:828,
        doorY:290,
        vertical: true,
        class:"staircase",
        doorExists: false
    }
    
]