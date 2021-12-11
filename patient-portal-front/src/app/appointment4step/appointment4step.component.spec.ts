import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Appointment4stepComponent } from './appointment4step.component';

describe('Appointment4stepComponent', () => {
  let component: Appointment4stepComponent;
  let fixture: ComponentFixture<Appointment4stepComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ Appointment4stepComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(Appointment4stepComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
