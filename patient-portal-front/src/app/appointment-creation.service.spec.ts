import { TestBed } from '@angular/core/testing';

import { AppointmentCreationService } from './appointment-creation.service';

describe('AppointmentCreationService', () => {
  let service: AppointmentCreationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AppointmentCreationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
