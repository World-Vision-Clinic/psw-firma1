import { TestBed } from '@angular/core/testing';

import { BlockPatientsService } from './block-patients.service';

describe('BlockPatientsService', () => {
  let service: BlockPatientsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BlockPatientsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
