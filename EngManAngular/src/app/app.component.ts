import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { ApiService } from './api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'

})
export class AppComponent implements OnInit{
  title = 'app';

  private EnableProfileSetting: boolean = false;

  constructor(private cookieService:CookieService,
    private router: Router,
    private apiService: ApiService)
  {

  }

  ngOnInit() 
  {
    if(this.cookieService.get('user.login.token.engmanangular').length > 0)
    {
      this.apiService.BearerToken = this.cookieService.get('user.login.token.engmanangular');
      if(this.router.url == '/')
      {
        this.router.navigate(["rules"]);
      }
      return;
    }
    this.router.navigate(["login"]);
  }

  ShowProfileSettings(): void
  {
    this.EnableProfileSetting = true;
  }

  CloseProfileSettings(): void
  {
    this.EnableProfileSetting = false;
  }

  Logout(): void
  {
    this.apiService.Logout().subscribe(obj => 
    {
      this.cookieService.delete(`user.login.token.engmanangular`);
      this.apiService.BearerToken = ``;
      this.router.navigate([`login`]);
    })
  }

}
