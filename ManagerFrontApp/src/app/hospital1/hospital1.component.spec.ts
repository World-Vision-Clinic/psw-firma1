import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Hospital1Component } from './hospital1.component';

describe('Hospital1Component', () => {
  let component: Hospital1Component;
  let fixture: ComponentFixture<Hospital1Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ Hospital1Component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(Hospital1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
