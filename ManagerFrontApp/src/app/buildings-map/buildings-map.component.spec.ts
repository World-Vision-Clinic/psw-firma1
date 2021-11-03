import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BuildingsMapComponent } from './buildings-map.component';

describe('BuildingsMapComponent', () => {
  let component: BuildingsMapComponent;
  let fixture: ComponentFixture<BuildingsMapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BuildingsMapComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BuildingsMapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
