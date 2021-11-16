import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagerIntegrationFrontAppComponent } from './manager-integration-front-app.component';

describe('ManagerIntegrationFrontAppComponent', () => {
  let component: ManagerIntegrationFrontAppComponent;
  let fixture: ComponentFixture<ManagerIntegrationFrontAppComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManagerIntegrationFrontAppComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ManagerIntegrationFrontAppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
