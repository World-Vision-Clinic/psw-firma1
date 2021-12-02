import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetSpecificationComponent } from './get-specification.component';

describe('GetSpecificationComponent', () => {
  let component: GetSpecificationComponent;
  let fixture: ComponentFixture<GetSpecificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GetSpecificationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GetSpecificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
