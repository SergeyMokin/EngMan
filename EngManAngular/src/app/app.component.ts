import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { ApiService } from './api.service';
import * as M from 'materialize-css';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'

})
export class AppComponent implements OnInit{
  title = 'app';

  constructor(private cookieService:CookieService,
    private router: Router,
    private apiService: ApiService)
  {
    document.addEventListener('DOMContentLoaded', function() {
      M.Dropdown.init(document.querySelectorAll('.dropdown-trigger'), {hover:true});
    });
  }

  ngOnInit(): void
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
