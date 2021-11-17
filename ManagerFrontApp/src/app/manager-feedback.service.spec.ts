import { TestBed } from '@angular/core/testing';

import { ManagerFeedbackService } from './manager-feedback.service';

describe('ManagerFeedbackService', () => {
  let service: ManagerFeedbackService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ManagerFeedbackService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
