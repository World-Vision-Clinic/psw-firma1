import { Component, OnInit } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';

@Component({
  selector: 'app-buildings-map',
  templateUrl: './buildings-map.component.html',
  styleUrls: ['./buildings-map.component.css']
})
export class BuildingsMapComponent implements OnInit {

  constructor(private router: Router){}

  enterHospital(){
    this.router.navigate(['/hospital1'])
  }

  ngOnInit(): void {
  }

}
