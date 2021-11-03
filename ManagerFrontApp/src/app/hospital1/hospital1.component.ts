import { ThisReceiver } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-hospital1',
  templateUrl: './hospital1.component.html',
  styleUrls: ['./hospital1.component.css']
})
export class Hospital1Component implements OnInit {

  constructor() { }
  btntext="Edit"
  roomName:string ='';
  doctorUsing:string='';
  purpose:string='';
  
  enableEdit(){
    
    if(this.btntext==='Edit'){
      this.btntext="Quit edit"
      let a = document.getElementsByTagName('input');
      for(let i=0; i<a.length; i++){
        a[i].disabled=false;
     }
    } else {
      this.btntext="Edit"
      let a = document.getElementsByTagName('input');
      for(let i=0; i<a.length; i++){
        a[i].disabled=true;
      }
    }
    
  }

  showInfo(roomName:string){
    this.roomName=roomName;

  }

  ngOnInit(): void {
  }

}
