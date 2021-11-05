import { ThisReceiver } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Floor } from '../data/floor';
import { BUILDINGS } from '../data/mock-buildings';
import { ROOMS } from '../data/mock-rooms';
import { emptyRoom, Room } from '../data/room';
@Component({
  selector: 'app-hospital1',
  templateUrl: './hospital1.component.html',
  styleUrls: ['./hospital1.component.css']
})
export class Hospital1Component implements OnInit {
  building = BUILDINGS;
	rooms = ROOMS;
  selectedBuilding = this.building[0];
  selectedFloor = this.selectedBuilding.floors[0]
  constructor(private router: Router) { }
  btntext="Edit"
  roomName:string ='';
  doctorUsing:string='';
  purpose:string='';
  selectedRoom:Room = emptyRoom();
  roomIsSelected = false;
	formDisabled: boolean = true;
	
  enableEdit(){
	this.formDisabled = false;
  }

  cancel(){
	this.formDisabled = true;
  }
  selectFloor = (floor: Floor) => {
    console.log(floor);
    this.selectedFloor = floor;
  }

  takeOut = () =>{
    this.router.navigate(['/buildings'])
  }

  save(){
	  const index = this.rooms.findIndex(e => e.roomId === this.selectedRoom?.roomId);
	  this.rooms[index] = this.selectedRoom;
	  this.formDisabled = true;
	  this.roomIsSelected = false;
	  this.selectedRoom = emptyRoom()
	  console.log(this.selectedRoom);
  }

  calculateTextX(room:Room){
	  const textWidth = room.name.length * 7.5
	  const middleX = room.x + room.width / 2 - textWidth/2
	  return middleX - 100
  }
  calculateTextY(room:Room){
	  const lineHeight = 10
	  const middleX =  room.y + room.height / 2 + lineHeight / 2
	  return middleX
  }

  close = () => {
    this.formDisabled = true;
	  this.roomIsSelected = false;
	  this.selectedRoom = emptyRoom()
  }
  showInfo(roomName:string){
	this.roomName=roomName;

  }
  selectRoom(room:Room){
	this.selectedRoom = {...room};
	this.roomIsSelected = true;
	this.formDisabled = true;
  }


  ngOnInit(): void {
  }

}
