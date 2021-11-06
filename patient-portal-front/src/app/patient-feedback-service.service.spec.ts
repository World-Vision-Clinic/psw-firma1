import { TestBed } from '@angular/core/testing';

import { PatientFeedbackServiceService } from './patient-feedback-service.service';

describe('PatientFeedbackServiceService', () => {
  let service: PatientFeedbackServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PatientFeedbackServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
