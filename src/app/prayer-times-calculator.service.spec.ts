import {
  beforeEachProviders,
  it,
  describe,
  expect,
  inject
} from '@angular/core/testing';
import { PrayerTimesCalculatorService } from './prayer-times-calculator.service';

describe('PrayerTimesCalculator Service', () => {
  beforeEachProviders(() => [PrayerTimesCalculatorService]);

  it('should ...',
      inject([PrayerTimesCalculatorService], (service: PrayerTimesCalculatorService) => {
    expect(service).toBeTruthy();
  }));
});
