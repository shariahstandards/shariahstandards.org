import { ShariahstandardsPage } from './app.po';

describe('shariahstandards App', function() {
  let page: ShariahstandardsPage;

  beforeEach(() => {
    page = new ShariahstandardsPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
