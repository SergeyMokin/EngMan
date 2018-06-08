import { browser, by, element, ExpectedConditions } from 'protractor';

export class AppPage {
  navigateTo(str: string) {
    browser.waitForAngularEnabled(false)
    browser.get(str, 2000)
  }

  getParagraphText() {
    return element(by.className("brand-logo center")).getText();
  }

  login()
  {
    element(by.id("email")).sendKeys("s.a.mokin@list.ru");
    element(by.id("password")).sendKeys("DefaultAdmin9856");
    let btn = element(by.className("btn waves-effect waves-light"));
    browser.waitForAngularEnabled(true)
    btn.click();
  }
}
