import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientFeedbackViewComponent } from './patient-feedback-view.component';

describe('PatientFeedbackViewComponent', () => {
  let component: PatientFeedbackViewComponent;
  let fixture: ComponentFixture<PatientFeedbackViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PatientFeedbackViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientFeedbackViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
