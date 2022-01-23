import { Component, OnInit } from '@angular/core';
import { BlockPatientsService } from '../block-patients.service';

@Component({
  selector: 'app-block-patients',
  templateUrl: './block-patients.component.html',
  styleUrls: ['./block-patients.component.css']
})
export class BlockPatientsComponent implements OnInit {

  public patients = [] as any
  public errorMsg = ""

  constructor(private managerService : BlockPatientsService) { }

  ngOnInit(): void {
    this.managerService.getMaliciousPatients().subscribe(data => this.patients = data,
                                                error => this.errorMsg = "Couldn't load malicious patients");
  }

  blockPatient(i) {
    console.log(i);
    let patient = this.patients[i];
    this.managerService.blockPatient(patient.userName).subscribe();
  }

}