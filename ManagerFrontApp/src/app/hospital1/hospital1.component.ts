import { HttpClient } from '@angular/common/http';
import { ThisReceiver } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Building } from '../data/building';
//import { Building } from '../data/building';
import { Equipment } from '../data/equipment';
import { Floor } from '../data/floor';
import { iEquipmentRoom } from '../data/iEquipmentRoom';
//import { BUILDINGS } from '../data/mock-buildings';
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
  interval: FormGroup;
  //destinationRoom: iEquipmentRoom | null = null;
  movingAmount: number | null = null;
  loadingFloors = true;
  loadingHospital = true;
  floors: Floor[] = [];
  building: Building[] = [];
  rooms = ROOMS;
  selectedRoom_ = null;
  selectedBuilding;
  selectedFloor;
  hospitalEquipment;
  hospitalRooms;
  searchResultList;
  startRoom: iEquipmentRoom | null = null;
  selectedEquipment: Equipment | null = null;
  roomsList: iEquipmentRoom[] = [];
  destinationRooms: Room[] | null = [];

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private hospitalService: HospitalService

  ) {
    const today = new Date();
    const month = today.getMonth();
    const year = today.getFullYear();

    this.interval = new FormGroup({
      start: new FormControl(),
      end: new FormControl(),
    });
  }

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
  allEquipment: any[] = [];
  buildingBox : boolean = false;

  currentState = {
    index: 0,
  };


  enableEdit() {
    this.formDisabled = false;
  }

  selectStartRoom(room) {
    this.startRoom = room;
    this.currentState.index = 2;
    const allRooms = this.floors
      .map((e) => e.rooms)
      .reduce((accumulator, value) => accumulator.concat(value), []);
    console.log('All rooms: ', allRooms);

    this.destinationRooms = allRooms.filter(
      (e) => !!e.doorExist && e.name != room.roomName && e.name != 'TOILET'
    );
    console.log('Destination rooms: ', this.destinationRooms);
  }

  goBack() {
    if (this.currentState.index > 0) this.currentState.index--;
  }
  estimateHours: number | null = null;
  goNext() {
    if (this.currentState.index <= 5) this.currentState.index++;
    if (this.currentState.index == 5) this.getSuggestion(this.estimateHours);
    if (this.currentState.index == 6) this.orderMovingEquipment();
  }

  orderMovingEquipment() {
    const data = {
      TargetRoomId: this.destinationRoom?.id,
      TargetEqupmentId: this.selectedEquipment?.id,
      startDate: this.suggestion?.startDate,
      endDate: this.suggestion?.endDate,
      Amount: this.movingAmount,
    };
    this.hospitalService.orderMoving(data).subscribe(
      (d) => {
        this.closeMovingContainer();
        console.log('Hura iznenilo seee!!!');
      },
      (e) => {}
    );
  }

  destinationRoom: Room | null = null;
  selectDestinationRoom(room) {
    this.destinationRoom = room;
    this.currentState.index++;
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

  closeInfo = () => {
    this.formDisabled = true;
    this.buildingBox = false;
  };

  closeSearchBox = () => {
    this.searchBoxDisabled = true;
    this.searchEquipmentResultBox = false;
    this.selectedFloor.highlightedRoomId = -1;
  };

  closeRoomTable = () => {
    this.searchBoxDisabled = true;
    this.searchRooms = true;
    this.searchBegin = false;
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
    this.loadAllEquipment();
    this.listBoxEquipment = true;
    this.moveBoxEquipment = true;
  }
  loadAllEquipment() {
    const hospitalId = this.router.url.split('/')[2];
    this.hospitalService.getEquipments(hospitalId, '').subscribe(
      (data) => {
        this.allEquipment = data.sort((a, b) => (a.name < b.name ? -1 : 1));
      },
      (error) => console.log(error)
    );
  }
  suggestion: any = null;

  getSuggestion(time) {
    const startDate = this.interval.controls['start'].value;
    const endDate = this.interval.controls['end'].value;
    console.log('Dates: ', startDate, endDate);

    const hospitalId = this.router.url.split('/')[2];
    this.hospitalService
      .getSuggestionForPeriod(hospitalId, startDate, endDate, time)
      .subscribe(
        (data) => {
          this.suggestion = data;
          console.log(data);
        },
        (e) => console.log(e)
      );
  }

  closeMovingContainer() {
    this.startRoom = null;
    this.selectedEquipment = null;
    this.destinationRoom = null;
    this.movingAmount = null;
    this.interval = new FormGroup({
      start: new FormControl(),
      end: new FormControl(),
    });
    this.estimateHours = null;
    this.listBoxEquipment = false;
    this.currentState.index = 0;
    this.suggestion = null;
  }
  disableNextButton() {
    if (this.currentState.index == 0 && !this.selectedEquipment) return true;
    else if (this.currentState.index == 1 && !this.startRoom) return true;
    else if (this.currentState.index == 2 && !this.movingAmount) return true;
    else if (this.currentState.index == 3 && !this.destinationRoom) return true;
    else if (this.currentState.index == 4 && !this.estimateHours) return true;
    return false;
  }

  selectEquipment(eq: Equipment) {
    this.selectedEquipment = eq;
    this.startRoom = null;
    const hospitalId = this.router.url.split('/')[2];
    this.hospitalService.getEquipmentRooms(hospitalId, eq.name).subscribe(
      (data) => {
        console.log(data);
        this.currentState.index++;
        this.roomsList = data;
      },
      (err) => console.log(err)
    );
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

  getRoomName(id){
    var name;
    this.hospitalRooms.forEach(room => {
      if(room.id == id){
        name = room.name;
      }
    });
    return name;
    
  }

  selecteddEquipment(roomId){
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
