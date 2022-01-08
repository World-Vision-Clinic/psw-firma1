import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TenderSelectionComponent } from './tender-selection.component';

describe('TenderSelectionComponent', () => {
  let component: TenderSelectionComponent;
  let fixture: ComponentFixture<TenderSelectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TenderSelectionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TenderSelectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
