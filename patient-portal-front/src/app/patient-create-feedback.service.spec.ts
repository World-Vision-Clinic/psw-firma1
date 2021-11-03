import { TestBed } from '@angular/core/testing';

import { PatientCreateFeedbackService } from './patient-create-feedback.service';

describe('PatientCreateFeedbackService', () => {
  let service: PatientCreateFeedbackService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PatientCreateFeedbackService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
