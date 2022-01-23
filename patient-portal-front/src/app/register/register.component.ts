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
  dateOfBirth: string = "";
  country: string = "";
  address: string = "";
  city: string = "";
  phoneNumber: string = "";
  height: string = "";
  weight: string = "";
  allergens: number[] = [];
  preferedDoctor: string = "";
  bloodType: string = "";
  password: string = "";
  confirmPassword: string = "";
  ProfileImage:string = "";
  
  maxDate: string = ""

  feedbackSent: boolean = false;
  errorMsg: string = "";
  image:any;
  
  public doctors = [] as any;
  public allAllergens = [] as any;

  constructor(private router: Router, private _registerService: RegisterService) { }


  ngOnInit(): void {
    this._registerService.getDoctors().subscribe(data => {this.doctors = data},
                                                error => this.errorMsg = "Couldn't load doctors");

    this._registerService.getAllergens().subscribe(data => this.allAllergens = data,
                                                error => this.errorMsg = "Couldn't load allergens");

    let today = new Date();
    console.log(today)
    let mm = today.getMonth()
    let monthMap = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    this.maxDate = today.getFullYear() + '-' + (mm + 1) + '-' + today.getDate()
    console.log(this.maxDate)
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
      user.DateOfBirth = new Date(this.dateOfBirth);
      user.Country = this.country;
      user.Address = this.address;
      user.City = this.city;
      user.Phone = this.phoneNumber;
      user.Height = parseInt(this.height);
      user.Weight = parseInt(this.weight);
      user.Allergens = this.allergens.map(Number);
      user.PreferedDoctor = parseInt(this.preferedDoctor);
      user.BloodType = this.bloodType;
      user.Password = this.password;
      user.ProfileImage = this.image['profileImage']

      console.log(user);

      this._registerService.register(user).subscribe(
        success => setTimeout(() => {
          this.router.navigate(['login']);
        }, 800));
    }
  }

  verifyPassword() {
    if(this.password == "")
      return false;
    if(this.password != this.confirmPassword)
      return false;
    if(this.password.length <= 0 || this.password.length > 100)
      return false;
    console.log("Passed pass");
    return true;
  }

  verifyUsername() {
    if(this.username == "")
      return false;
    if(this.username == null)
      return false;
    var regexp = new RegExp(/^[a-zA-Z0-9]{1,30}$/);
    if (!regexp.test(this.username))
        return false;
    console.log("Passed username");
    return true;
  }

  verifyFirstName() {
    if(this.firstName == "")
      return false;
    if(this.firstName == null)
      return false;
    var regexp = new RegExp(/^[a-zA-ZčćžšđČĆŽŠĐ]{1,20}$/);
    if (!regexp.test(this.firstName))
        return false;
    console.log("Passed firstName");
    return true;
  }

  verifyLastName() {
    if(this.lastName == "")
      return false;
    if(this.lastName == null)
      return false;
    var regexp = new RegExp(/^[a-zA-ZčćžšđČĆŽŠĐ]{1,20}$/);
    if (!regexp.test(this.lastName))
        return false;
    console.log("Passed lastname");
    return true;
  }

  verifyEmail() {
    let maxLength = 30;
    if(this.email == null)
      return false;
    if(this.email.length > maxLength)
      return false;
    var regexp = new RegExp(/^[a-z0-9\\.]+[@][a-z0-9\\.]+$/);
    if (!regexp.test(this.email))
        return false;
    console.log("Passed email");
    return true;
  }

  verifyGender() {
    if(this.gender == null)
      return false;
    if(this.gender != 'Male' && this.gender != 'Female')
      return false;
    console.log("Passed gender");
    return true;
  }

  verifyJmbg() {
    if(this.jmbg == null)
      return false;
    var regexp = new RegExp(/^[0-9]{13}$/);
    if (!regexp.test(this.jmbg))
        return false;
    console.log("Passed jmbg");
    return true;
  }

  verifyDateOfBirth() {
    if(this.dateOfBirth == null)
      return false;
    if(this.dateOfBirth == "")
      return false;
    console.log("Passed date of birth");
    return true;
  }

  verifyCountry() {
    if(this.country == null)
      return false;
    var regexp = new RegExp(/^[a-zA-ZčćžšđČĆŽŠĐ ]{1,20}$/);
    if (!regexp.test(this.country))
        return false;
    console.log("Passed country");
    return true;
  }

  verifyAddress() {
    if(this.address == null)
      return false;
    var regexp = new RegExp(/^[a-zA-Z0-9čćžšđČĆŽŠĐ ]{1,30}$/);
    if (!regexp.test(this.address))
        return false;
    console.log("Passed address");
    return true;
  }

  verifyCity() {
    if(this.city == null)
      return false;
    var regexp = new RegExp(/^[a-zA-ZčćžšđČĆŽŠĐ ]{1,20}$/);
    if (!regexp.test(this.city))
        return false;
    console.log("Passed city");
    return true;
  }

  verifyPhone() {
    if(this.phoneNumber == null)
      return false;
    var regexp = new RegExp(/^[0-9]{1,30}$/);
    if (!regexp.test(this.phoneNumber))
        return false;
    console.log("Passed phone");
    return true;
  }

  verifyWeight() {
    if(this.weight == null)
      return false;
    var regexp = new RegExp(/^[1-9][0-9]{1,3}$/);
    if (!regexp.test(this.weight))
        return false;
    console.log("Passed weight");
    return true;
  }

  verifyHeight() {
    if(this.height == null)
      return false;
    var regexp = new RegExp(/^[1-9][0-9]{1,2}$/);
    if (!regexp.test(this.height))
        return false;
    console.log("Passed height");
    return true;
  }

  verifyPreferedDoctor() {
    if(this.preferedDoctor == null)
      return false;
    var regexp = new RegExp(/^[0-9]+$/);
    if (!regexp.test(this.preferedDoctor))
        return false;
    console.log("Passed prefered doctor");
    return true;
  }

  verifyBloodType() {
    if(this.bloodType == null)
      return false;
    if(this.bloodType != 'A' && this.bloodType != 'B' && this.bloodType != 'O' && this.bloodType != 'AB')
      return false;
    console.log("Passed blood type");
    return true;
  }

  contentIsValid() {
    if(!this.verifyPassword())
      return false;
    if(!this.verifyUsername())
      return false;
    if(!this.verifyFirstName())
      return false;
    if(!this.verifyLastName())
      return false;
    if (!this.verifyEmail())
      return false;
    if (!this.verifyGender())
      return false;
    if (!this.verifyJmbg())
      return false;
    if (!this.verifyDateOfBirth())
      return false;
    if (!this.verifyCountry())
      return false;
    if (!this.verifyAddress())
      return false;
    if (!this.verifyCity())
      return false;
    if (!this.verifyPhone())
      return false;
    if (!this.verifyWeight())
      return false;
    if (!this.verifyHeight())
      return false;
    if (!this.verifyBloodType())
      return false;
    if(!this.verifyPreferedDoctor())
      return false;
    return true;
  }

  addAllergen(id: number) {
    this.allergens.push(id)
  }

  removeAllergen(id: number) {
    let index = this.allergens.findIndex(a => a === id)
    if(index >= 0) {
      this.allergens.splice(index,1)
    }
  }

  allergenIsPicked(id: number) {
    for (let a of this.allergens) {
        if (a == id) {
          return true;
        }
    } 
    return false
  }

  handleProfileImage(event: any) {

    if(!event || !event.target || !event.target.files) {
      return;
    }

    this.getBase64(event, 'profileImage');
  }

  getBase64(event:any, name: any) {
    let me = this;
    let file = event.target.files[0];
    let reader = new FileReader();
    reader.readAsDataURL(file);
    var self = this;

    if(!this.image) {
      this.image = {};
    }

    reader.onload = function () {
      self.image[name] = reader.result;
    }
    reader.onerror = function (error) {
    };
 }
}