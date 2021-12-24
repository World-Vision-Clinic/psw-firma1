import { Component, OnInit, ViewChild } from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http'
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { AccumulationChartComponent, AccumulationChart, IAccLoadedEventArgs, AccumulationTheme } from "@syncfusion/ej2-angular-charts";


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
  profileNotSelected: boolean = true;
  selectedProfile: any;
  isEditing: boolean = false;
  isViewing: boolean = true;
  oldSelectedProfile: any;
  PhotoFilePath: string = '';
  PhotoFileName: string = '';
  formData: any='';
  isPictureRemoved: boolean = true;
  timeStamp: any;
  error: any;
  isPictureRemovedCopy: boolean = false;
  lost: number = 30;
  won: number = 8;
  percent: string = '';

  constructor(private http:HttpClient) { }

  public data: Object[] = [
    { x: "LOST", y: this.lost },
    { x: "WON", y: this.won }
  ];

  @ViewChild("pie")
  public pie!: AccumulationChartComponent | AccumulationChart;
  palette = [
    "rgba(255,41,41)",
    "rgba(102,255,102)"
  ];
  public animation: Object = {
    enable: false
  };
  //Initializing Legend
  public legendSettings: Object = {
    visible: false
  };
  //Initializing Datalabel
  public dataLabel: Object = {
    visible: true,
    position: "Inside",
    font: { size: "10px", color: "white" },
    template: "<div>${point.x}: ${point.y}</div>"
  };
  // custom code start
  public load(args: IAccLoadedEventArgs): void {
    let selectedTheme: string = location.hash.split("/")[1];
    selectedTheme = selectedTheme ? selectedTheme : "Material";
    args.accumulation.theme = <AccumulationTheme>(
      (selectedTheme.charAt(0).toUpperCase() + selectedTheme.slice(1)).replace(
        /-dark/i,
        "Dark"
      )
    );
    //dark mode
    this.pie.background = "#FFFFFF";
  }
  openTendersStatistic(){
    this.percent = (Math.round(this.won/(this.won + this.lost) * 100 * 100) / 100).toFixed(2);
  }
  // custom code end
  public center: Object = { x: "50%", y: "50%" };
  public startAngle: number = 0;
  public endAngle: number = 360;
  public explode: boolean = true;
  public enableAnimation: boolean = false;
  public tooltip: Object = {
    enable: true,
    format: "${point.x} : <b>${point.y}</b>"
  };
  public title: string = "Success statistics";

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
    },
    error =>{alert("Please enter proper values")});
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
  checkIfThereArePharmacies(){
    return this.PharmaciesList.length == 0;
  }
  orderMedicines(selectedPharmacy){
    const body = { Localhost: selectedPharmacy.Localhost, MedicineName: this.medicineName, MedicineGrams: this.medicineGrams, NumOfBoxes: this.numOfBoxes};
    this.http.put<any>('http://localhost:43818/medicines/OrderMedicine', body)
        .subscribe(res => alert("Medicine succesfully ordered"));
    this.buttonClicked = false;
  }

  showProfile(selectedProfile){
    this.profileNotSelected = false;
    this.selectedProfile= selectedProfile;
    
    let httpHeaders = new HttpHeaders()
    .set('Accept', "image/webp,*/*");
    this.http.get('http://localhost:43818/Photos/'+this.selectedProfile.Name+'.png', { headers: httpHeaders, responseType: 'blob' as 'json' }).pipe(
      catchError(this.errorHandler)
  ).subscribe(data=>{
    this.isPictureRemoved = false;
    this.setLinkPicture('http://localhost:43818/Photos/'+this.selectedProfile.Name+'.png');
    this.PhotoFileName = this.selectedProfile.Name+'.png';
  })   
  }

  goBack(){
    this.profileNotSelected = true;
    this.getPharmacies();
  }

  editProfile(){
    this.oldSelectedProfile = JSON.parse(JSON.stringify(this.selectedProfile))
    this.isViewing = false;
    this.isEditing = true;
    this.isPictureRemovedCopy= this.isPictureRemoved;
    this.formData = null;
  }

  saveChanges(){

    this.http.put<any>('http://localhost:43818/Pharmacies', this.selectedProfile)
        .subscribe(data => {this.selectedProfile = data

          if((this.formData==null || this.formData.length == 0) && this.isPictureRemoved==true){
            this.http.delete('http://localhost:43818/api/Photos/deletePhoto/'+this.selectedProfile.Name).subscribe((data:any)=>{
              this.isViewing = true;
              this.isEditing = false;
              this.isPictureRemoved=true;
              alert("Your changes have been saved.")
             
            })
         
          } else if(this.formData!=null && this.formData.length != 0){
              this.http.post('http://localhost:43818/api/Photos/addPhoto/'+this.selectedProfile.Name, this.formData).subscribe((data:any)=>{
                 this.PhotoFileName = data.toString();
                 this.setLinkPicture('http://localhost:43818/Photos/'+this.PhotoFileName);
                 this.isViewing = true;
                 this.isEditing = false;
                 this.isPictureRemoved = false;
                 alert("Your changes have been saved.")
               })
              }
              else{
                this.isViewing = true;
                this.isEditing = false;
                alert("Your changes have been saved.")
              }

        });
    
  }

  discardChanges(){
    this.selectedProfile = JSON.parse(JSON.stringify(this.oldSelectedProfile))
    this.setLinkPicture('http://localhost:43818/Photos/'+this.PhotoFileName);
    this.isViewing = true;
    this.isEditing = false;
    this.isPictureRemoved=this.isPictureRemovedCopy;
  }

  uploadPhoto(event){
    var file = event.target.files[0];
   
    this.formData = new FormData();
    this.formData.append('uploadedFile',file,file.name);

  }

  removePicture(){
    this.isPictureRemovedCopy= this.isPictureRemoved
    this.isPictureRemoved = true;
  
    this.setLinkPicture('');
  }

  public getLinkPicture() {
    if (this.timeStamp) {
       return this.PhotoFilePath + '?' + this.timeStamp;
    }
    return this.PhotoFilePath;
 }

 public setLinkPicture(url: string) {
  this.PhotoFilePath = url;
  this.timeStamp = (new Date()).getTime();
}

errorHandler(error: HttpErrorResponse) {
  return throwError(error.message || "server error.");
}

}
