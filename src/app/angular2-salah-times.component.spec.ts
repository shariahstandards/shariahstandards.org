import {
  beforeEachProviders,
  describe,
  expect,
  it,
  inject
} from '@angular/core/testing';
import { Angular2SalahTimesAppComponent } from '../app/angular2-salah-times.component';

beforeEachProviders(() => [Angular2SalahTimesAppComponent]);

describe('App: Angular2SalahTimes', () => {
  it('should create the app',
      inject([Angular2SalahTimesAppComponent], (app: Angular2SalahTimesAppComponent) => {
    expect(app).toBeTruthy();
  }));

  it('should have as title \'angular2-salah-times works!\'',
      inject([Angular2SalahTimesAppComponent], (app: Angular2SalahTimesAppComponent) => {
    expect(app.title).toEqual('angular2-salah-times works!');
  }));
});
