import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagerObjectionsComponent } from './manager-objections.component';

describe('ManagerObjectionsComponent', () => {
  let component: ManagerObjectionsComponent;
  let fixture: ComponentFixture<ManagerObjectionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManagerObjectionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ManagerObjectionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
