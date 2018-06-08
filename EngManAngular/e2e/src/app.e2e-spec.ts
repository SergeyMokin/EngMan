import { AppPage } from './app.po';

describe('App component', () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
  });

  it('Route should display LOGIN', () => {
    page.navigateTo("/login");
    expect(page.getParagraphText()).toEqual('LOGIN');
  });

  it('Route should display RULES after login', () => 
  {
    page.login();
    expect(page.getParagraphText()).toEqual('RULES');
  })

  it('Route shold display PRACTICE-RULES', () =>
  {
    page.navigateTo("/practice-rules");
    expect(page.getParagraphText()).toEqual('PRACTICE-RULES');
  })

  it('Route shold display PROFILE-INFO', () =>
  {
    page.navigateTo("/profile-info");
    expect(page.getParagraphText()).toEqual('PROFILE-INFO');
  })

  it('Route shold display USER-DICTIONARY', () =>
  {
    page.navigateTo("/user-dictionary");
    expect(page.getParagraphText()).toEqual('USER-DICTIONARY');
  })

  it('Route shold display WORD-TRANSLATE', () =>
  {
    page.navigateTo("/word-translate");
    expect(page.getParagraphText()).toEqual('WORD-TRANSLATE');
  })
  
  it('Route shold display TRANSLATE-WORD', () =>
  {
    page.navigateTo("/translate-word");
    expect(page.getParagraphText()).toEqual('TRANSLATE-WORD');
  })

  it('Route shold display WORD-CARD', () =>
  {
    page.navigateTo("/word-card");
    expect(page.getParagraphText()).toEqual('WORD-CARD');
  })

  it('Route shold display GUESSES-THE-IMAGE', () =>
  {
    page.navigateTo("/guesses-the-image");
    expect(page.getParagraphText()).toEqual('GUESSES-THE-IMAGE');
  })
});
