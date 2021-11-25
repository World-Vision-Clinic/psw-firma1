import { Component, DoCheck, OnInit } from '@angular/core';
import { User } from 'src/user';
import { Router } from '@angular/router';
import { RegisterService } from '../register.service';
import { Doctor } from 'src/doctor';
import { Allergen } from 'src/allergen';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  firstName: string = "";
  lastName: string = "";
  username: string = "";
  gender: string = "";
  jmbg: string = "";
  email: string = "";
  dateOfBirth: Date = new Date();
  country: string = "";
  address: string = "";
  city: string = "";
  phoneNumber: string = "";
  height: number = -1;
  weight: number = -1;
  allergens: number[] = [];
  preferedDoctor: string = "";
  bloodType: string = "";
  password: string = "";
  confirmPassword: string = "";

  feedbackSent: boolean = false;
  errorMsg: string = "";
  
  public doctors = [] as any;
  public allAllergens = [] as any;

  constructor(private router: Router, private _registerService: RegisterService) { }


  ngOnInit(): void {
    this._registerService.getDoctors().subscribe(data => {this.doctors = data},
                                                error => this.errorMsg = "Couldn't load doctors");

    this._registerService.getAllergens().subscribe(data => this.allAllergens = data,
                                                error => this.errorMsg = "Couldn't load allergens");
  }

  register() {
    if(this.contentIsValid()){
      let user = new User();
      user.UserName = this.username;
      user.FirstName = this.firstName;
      user.LastName = this.lastName;
      user.Gender = this.gender;
      user.Jmbg = this.jmbg;
      user.EMail = this.email;
      user.DateOfBirth = this.dateOfBirth;
      user.Country = this.country;
      user.Address = this.address;
      user.City = this.city;
      user.Phone = this.phoneNumber;
      user.Height = this.height;
      user.Weight = this.weight;
      user.Allergens = this.allergens.map(Number);
      user.PreferedDoctor = parseInt(this.preferedDoctor);
      user.BloodType = this.bloodType;
      user.Password = this.password;

      console.log(user);

      this._registerService.register(user).subscribe(
        success => setTimeout(() => {
          this.router.navigate(['login']);
        }, 800));
    }
  }

  contentIsValid() {
    if(this.password != this.confirmPassword)
      return false;
    return true;
  }

}