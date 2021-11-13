import { Component, OnInit } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';
import { Building } from '../data/building';
import { BUILDINGS } from '../data/mock-buildings';
import { Parking } from '../data/parking';

@Component({
  selector: 'app-buildings-map',
  templateUrl: './buildings-map.component.html',
  styleUrls: ['./buildings-map.component.css']
})
export class BuildingsMapComponent implements OnInit {
  buildings = BUILDINGS;
  parkings: Parking[] = []
  constructor(private router: Router){}

  enterHospital(){
    this.router.navigate(['/hospital1'])
  }

  ngOnInit(): void {
    const y0 = 80;
    const dy = 70;
    for(let row = 0; row < 7; row++){
      for(let col = 0; col < 2; col++){
        const x0 = col == 0 ? 670 : 800;
        const parking: Parking = {
            x: x0, 
            y:  y0 + row * dy,
            width: 110,
            height: 50
        }
        this.parkings.push(parking)
      }
    }
  }

  calculateTextX(building:Building){
	  const textWidth = building.name.length * 24
	  const middleX = building.mapPosition.x + building.mapPosition.width / 2 - textWidth / 2;
	  return middleX;
  }
  calculateTextY(building:Building){
	  const lineHeight = 28
	  const middleY =  building.mapPosition.y + building.mapPosition.height / 2 + lineHeight / 2;
	  return middleY;
  }

}
