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


const routes = [
  {
    path: 'login',
    component: LoginFormPageComponent
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
    path: '**', 
    redirectTo: '/rules'
  }
]


@NgModule({
  declarations: [
    AppComponent,
    LoginFormPageComponent,
    RulesListFormComponent,
    PracticeRulesPageComponent
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
