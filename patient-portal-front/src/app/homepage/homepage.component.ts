import { Component, OnInit } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomePageComponent implements OnInit {
  menuIsOpen: boolean=true;
  constructor(private router: Router){}

  

  ngOnInit(): void {
  }

}
