import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlockPatientsComponent } from './block-patients.component';

describe('BlockPatientsComponent', () => {
  let component: BlockPatientsComponent;
  let fixture: ComponentFixture<BlockPatientsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BlockPatientsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BlockPatientsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
