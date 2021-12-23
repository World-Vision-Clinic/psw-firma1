import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TenderCreationComponent } from './tender-creation.component';

describe('TenderCreationComponent', () => {
  let component: TenderCreationComponent;
  let fixture: ComponentFixture<TenderCreationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TenderCreationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TenderCreationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
