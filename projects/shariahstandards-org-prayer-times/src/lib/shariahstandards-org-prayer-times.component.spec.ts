import { async, ComponentFixture, TestBed } from '@angular/core/testing';
//import 'jasmine';
import { ShariahstandardsOrgPrayerTimesComponent } from './shariahstandards-org-prayer-times.component';

describe('ShariahstandardsOrgPrayerTimesComponent', () => {
  let component: ShariahstandardsOrgPrayerTimesComponent;
  let fixture: ComponentFixture<ShariahstandardsOrgPrayerTimesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShariahstandardsOrgPrayerTimesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShariahstandardsOrgPrayerTimesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
