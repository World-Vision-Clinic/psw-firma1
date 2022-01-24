export interface RenovationDto{
    
    Room1Id:number;
    Room2Id:number;
    NewRoomName1:string;
    NewRoomName2:string;

    NewRoomPurpose1:string;
    NewRoomPurpose2:string;
    StartDateTimestamp:number;
    EndDateTimeStamp:number;
    isMerge:boolean;
}