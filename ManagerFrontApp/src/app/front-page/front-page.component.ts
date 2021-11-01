import { Component, OnInit } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';

@Component({
  selector: 'app-front-page',
  templateUrl: './front-page.component.html',
  styleUrls: ['./front-page.component.css']
})
export class FrontPageComponent implements OnInit {

  constructor(private router: Router){}

  switchPage(){
    this.router.navigate(['/buildings'])
  }

  ngOnInit(): void {
  }

}
