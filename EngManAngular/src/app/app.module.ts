import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { LoginFormPageComponent } from './login-form-page/login-form-page.component'; 
import { CookieService } from 'ngx-cookie-service';
import { RulesListFormComponent } from './rules-list-form/rules-list-form.component';
import { AppInterceptor } from './app.interceptor'
import { ApiService } from './api.service';
import { PracticeRulesPageComponent } from './practice-rules-page/practice-rules-page.component';
import { ProfileInfoPageComponent } from './profile-info-page/profile-info-page.component';
import { UserDictionaryPageComponent } from './user-dictionary-page/user-dictionary-page.component';
import { SearchUserDictionaryPipe } from './user-dictionary-page/user-dictionary-page.pipe';
import { HoveredRuleDirective } from './rules-list-form/rules-list-form.directive';
import { WordTranslatePageComponent } from './word-translate-page/word-translate-page.component';
import { TranslateWordPageComponent } from './translate-word-page/translate-word-page.component';
import { WordCardPageComponent } from './word-card-page/word-card-page.component';
import { GuessesTheImagePageComponent } from './guesses-the-image-page/guesses-the-image-page.component';


const routes = [
  {
    path: 'login',
    component: LoginFormPageComponent
  },
  {
    path: 'profile-info',
    component: ProfileInfoPageComponent
  },
  {
    path: 'user-dictionary',
    component: UserDictionaryPageComponent
  },
  {
    path: 'rules',
    component: RulesListFormComponent
  },
  {
    path: 'practice-rules',
    component: PracticeRulesPageComponent
  },
  {
    path: 'word-translate',
    component: WordTranslatePageComponent
  },
  {
    path: 'translate-word',
    component: TranslateWordPageComponent
  },
  {
    path: 'word-card',
    component: WordCardPageComponent
  },
  {
    path: 'guesses-the-image',
    component: GuessesTheImagePageComponent
  },
  {
    path: '**', 
    redirectTo: '/rules'
  }
]


@NgModule({
  declarations: [
    AppComponent,
    LoginFormPageComponent,
    RulesListFormComponent,
    PracticeRulesPageComponent,
    ProfileInfoPageComponent,
    UserDictionaryPageComponent,
    SearchUserDictionaryPipe,
    HoveredRuleDirective,
    WordTranslatePageComponent,
    TranslateWordPageComponent,
    WordCardPageComponent,
    GuessesTheImagePageComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(routes)
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: AppInterceptor,
    multi: true
  },
  CookieService,
  ApiService],
  bootstrap: [AppComponent]

})
export class AppModule { }
