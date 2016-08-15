import {
  beforeEachProviders,
  describe,
  expect,
  it,
  inject
} from '@angular/core/testing';
import { ShariahStandardsAppComponent } from '../app/shariah-standards.component';

beforeEachProviders(() => [ShariahStandardsAppComponent]);

describe('App: ShariahStandardsAppComponent', () => {
  it('should create the app',
      inject([ShariahStandardsAppComponent], (app: ShariahStandardsAppComponent) => {
    expect(app).toBeTruthy();
  }));

  it('should have as title \'shariah-standards works!\'',
      inject([ShariahStandardsAppComponent], (app: ShariahStandardsAppComponent) => {
    expect(app.title).toEqual('shariah-standards works!');
  }));
});
