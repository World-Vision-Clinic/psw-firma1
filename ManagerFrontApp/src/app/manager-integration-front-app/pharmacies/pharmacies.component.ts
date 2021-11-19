import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';

@Component({
  selector: 'app-pharmacies',
  templateUrl: './pharmacies.component.html',
  styleUrls: ['./pharmacies.component.css']
})
export class PharmaciesComponent implements OnInit {

  PharmaciesList:any =[];
  PharmaciesWithMedicalList:any =[];
  searchFilter: string = '';
  medicineName: string = '';
  medicineGrams: string = '';
  numOfBoxes: string = '';
  buttonClicked: boolean = false;

  constructor(private http:HttpClient) { }

  ngOnInit(): void {
    this.getPharmacies();
  }

  searchPharmaciesForMedicals(){ 
    this.buttonClicked = true;
    this.searchPharmacies();
    return this.http.get<any>('http://localhost:43818/medicines/check?name=' + this.medicineName
    + "&dosage=" + this.medicineGrams + "&quantity=" + this.numOfBoxes).subscribe(data=>{
      this.PharmaciesWithMedicalList=data;
      this.PharmaciesList = this.PharmaciesList.filter(o => data.some(({Localhost}) => o.Localhost === Localhost));
      this.buttonClicked = true;
    });
  }
  disableButtons(){
    this.buttonClicked = false;
  }
  searchPharmacies(){
    this.buttonClicked = false;
    return this.http.get<any>("http://localhost:43818/Pharmacies/Filtered?searchFilter=" + this.searchFilter).subscribe(data=>{
      this.PharmaciesList=data;
    });
  }

  getPharmacies(){
    return this.http.get<any>("http://localhost:43818/Pharmacies").subscribe(data=>{
      this.PharmaciesList=data;
    });
  }
  orderMedicines(selectedPharmacy){
    const body = { Localhost: selectedPharmacy.Localhost, MedicineName: this.medicineName, MedicineGrams: this.medicineGrams, NumOfBoxes: this.numOfBoxes};
    this.http.put<any>('http://localhost:43818/medicines/OrderMedicine', body)
        .subscribe();
    alert("Medicine succesfully ordered");
  }
}
