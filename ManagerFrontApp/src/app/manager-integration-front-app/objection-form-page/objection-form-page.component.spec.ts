import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ObjectionFormPageComponent } from './objection-form-page.component';

describe('ObjectionFormPageComponent', () => {
  let component: ObjectionFormPageComponent;
  let fixture: ComponentFixture<ObjectionFormPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ObjectionFormPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ObjectionFormPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
