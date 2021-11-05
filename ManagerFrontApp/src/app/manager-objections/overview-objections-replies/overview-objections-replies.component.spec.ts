import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OverviewObjectionsRepliesComponent } from './overview-objections-replies.component';

describe('OverviewObjectionsRepliesComponent', () => {
  let component: OverviewObjectionsRepliesComponent;
  let fixture: ComponentFixture<OverviewObjectionsRepliesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OverviewObjectionsRepliesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OverviewObjectionsRepliesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
