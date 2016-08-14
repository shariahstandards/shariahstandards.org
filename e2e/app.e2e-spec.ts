import { Angular2SalahTimesPage } from './app.po';

describe('angular2-salah-times App', function() {
  let page: Angular2SalahTimesPage;

  beforeEach(() => {
    page = new Angular2SalahTimesPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
