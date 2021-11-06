import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientCreateFeedbackComponent } from './patient-create-feedback.component';

describe('PatientCreateFeedbackComponent', () => {
  let component: PatientCreateFeedbackComponent;
  let fixture: ComponentFixture<PatientCreateFeedbackComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PatientCreateFeedbackComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientCreateFeedbackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
