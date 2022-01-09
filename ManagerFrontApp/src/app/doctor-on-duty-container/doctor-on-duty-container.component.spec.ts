import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorOnDutyContainerComponent } from './doctor-on-duty-container.component';

describe('DoctorOnDutyContainerComponent', () => {
  let component: DoctorOnDutyContainerComponent;
  let fixture: ComponentFixture<DoctorOnDutyContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DoctorOnDutyContainerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DoctorOnDutyContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
