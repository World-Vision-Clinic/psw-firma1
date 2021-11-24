import { HttpClient } from '@angular/common/http';
import { ThisReceiver } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Building } from '../data/building';
import { Floor } from '../data/floor';
import { BUILDINGS } from '../data/mock-buildings';
import { ROOMS } from '../data/mock-rooms';
import { emptyRoom, Room } from '../data/room';
import { HospitalService } from './hospital.service';
@Component({
  selector: 'app-hospital1',
  templateUrl: './hospital1.component.html',
  styleUrls: ['./hospital1.component.css'],
})
export class Hospital1Component implements OnInit {
  hospitalId;
  loadingFloors = true;
  loadingHospital = true;
  floors;
  building = BUILDINGS;
  rooms = ROOMS;
  selectedRoom_ = null;
  selectedBuilding;
  selectedFloor;
  hospitalEquipment;
  hospitalRooms;
  searchResultList;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private hospitalService: HospitalService
  ) {}

  btntext = 'Edit';
  buildingFormDisabled = true;
  roomName: string = '';
  doctorUsing: string = '';
  purpose: string = '';
  selectedRoom: Room = emptyRoom();
  roomIsSelected = false;
  formDisabled: boolean = true;
  equipmentBox: boolean = false;
  listBoxEquipment: boolean = false;
  moveBoxEquipment: boolean = false;
  searchEquipmentResultBox: boolean = false;
  searchRooms: boolean = true;
  searchBegin: boolean = false;
  searchBoxDisabled: boolean = true;

  enableEdit() {
    this.formDisabled = false;
  }

  cancel() {
    this.formDisabled = true;
  }
  selectFloor = (floor: Floor) => {
    this.selectedFloor = floor;
  };

  takeOut = () => {
    this.router.navigate(['/buildings']);
  };

  save() {
    const index = this.selectedFloor.rooms.findIndex(
      (e) => e.id === this.selectedRoom?.id
    );
    const room = this.selectedRoom;
    this.selectedFloor.rooms[index] = this.selectedRoom;
    this.formDisabled = true;
    this.roomIsSelected = false;
    this.selectedRoom = emptyRoom();
    this.hospitalService.updateRoom('' + room.id, room).subscribe(
      (e) => console.log(e),
      (e) => console.log(e)
    );
  }

  calculateTextX(room: Room) {
    const textWidth = room.name.length * 7.5;
    const middleX = room.x + room.width / 2 - textWidth / 2;
    return middleX;
  }
  calculateTextY(room: Room) {
    const lineHeight = 10;
    const middleX = room.y + room.height / 2 + lineHeight / 2;
    return middleX;
  }

  close = () => {
    this.formDisabled = true;
    this.roomIsSelected = false;
    this.selectedRoom = emptyRoom();
  };

  closeSearchBox = () => {
    this.searchBoxDisabled = true;
    this.searchEquipmentResultBox = false;
    this.selectedFloor.highlightedRoomId = -1;
  };

  closeRoomTable = () => {
    this.searchBoxDisabled = true;
    this.searchRooms = false;
    this.selectedFloor.highlightedRoomId = -1;
  };

  showInfo(roomName: string) {
    this.roomName = roomName;
  }

  selectRoom(room: Room) {
    this.selectedRoom = { ...room };
    this.roomIsSelected = true;
    this.formDisabled = true;
  }

  equipment() {
    this.equipmentBox = true;
  }

  closeEquip() {
    this.equipmentBox = false;
  }

  moveEquipment() {
    this.listBoxEquipment = true;
    this.moveBoxEquipment = true;
  }

  searchEquipment() {
    this.searchEquipmentResultBox = true;
  }

  searchrooms() {
    this.searchRooms = true;
  }

  setSearchCriteria() {
    this.searchRooms = !this.searchRooms;
    this.searchBegin = true;
  }

  searchForEquipment(event: any) {
    var inputValue = event.target.value.toLowerCase();
    this.searchResultList = [];

    this.hospitalEquipment.forEach((element) => {
      if (element.name.toLowerCase().includes(inputValue)) {
        this.searchResultList.push(element);
      }
    });
    this.searchEquipment();
  }

  searchForRooms(event: any) {
    this.searchBegin = true;
    var inputValue = event.target.value.toLowerCase();
    this.searchResultList = [];

    this.hospitalRooms.forEach((element) => {
      if (element.name.toLowerCase().includes(inputValue)) {
        this.searchResultList.push(element);
      }
    });
    console.log(this.searchResultList);
    this.searchrooms();
  }

  selectedEquipment(roomId){
    this.hospitalRooms.forEach(room => {
      if(room.id == roomId){
        if(room.floorId == 1){
          this.selectFloor(this.floors[0]);
          this.selectedFloor.highlightedRoomId = room.id;
        }
        else{
          this.selectFloor(this.floors[1]);
          this.selectedFloor.highlightedRoomId = room.id;
        }
      }
    });
  }

  ngOnInit(): void {
    this.loadHospital();
    this.loadHospitalFloors();
  }

  async saveHospital() {
    const hospitalId = this.router.url.split('/')[2];
    this.hospitalService
      .updateHospital(hospitalId, this.selectedBuilding)
      .subscribe(
        (data) => {
          this.buildingFormDisabled = true;
        },
        (error) => console.log(error)
      );
  }

  async loadHospitalFloors() {
    const hospitalId = this.router.url.split('/')[2];
    this.hospitalService.getFloors(hospitalId).subscribe(
      (data) => {
        console.log(data);

        this.floors = data;
        this.selectedFloor = this.floors[0];
        this.loadingFloors = false;
      },
      (error) => console.log(error)
    );
  }

  async loadHospital() {
    const hospitalId = this.router.url.split('/')[2];
    this.hospitalService.getHospital(hospitalId).subscribe(
      (data) => {
        this.selectedBuilding = data;
        this.loadingHospital = false;
        this.hospitalEquipment = data.equipment;
        this.hospitalRooms = data.rooms;
        console.log(this.hospitalRooms);
      },
      (error) => console.log(error)
    );
  }

  async loadEquipment(id: number) {
    this.hospitalService.getEquipment(id).subscribe(
      (data) => {
        this.selectedRoom.equipments = data.equipments;
      },
      (error) => console.log(error)
    );
  }
}
