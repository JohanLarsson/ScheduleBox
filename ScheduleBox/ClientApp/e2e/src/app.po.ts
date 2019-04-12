import { browser, by, element } from 'protractor';

export class AppPage {
  navigateTo() {
    return browser.get(browser.baseUrl) as Promise<any>;
  }

  getDate() {
    return element(by.name('date')).getText() as Promise<string>;
  }
}
