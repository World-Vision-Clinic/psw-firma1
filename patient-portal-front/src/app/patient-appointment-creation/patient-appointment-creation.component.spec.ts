import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientAppointmentCreationComponent } from './patient-appointment-creation.component';

describe('PatientAppointmentCreationComponent', () => {
  let component: PatientAppointmentCreationComponent;
  let fixture: ComponentFixture<PatientAppointmentCreationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PatientAppointmentCreationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientAppointmentCreationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
