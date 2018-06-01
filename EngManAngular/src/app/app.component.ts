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

  public ngOnInit(): void
  {
    if(this.cookieService.get('user.login.token.engmanangular').length > 0)
    {
      this.apiService.BearerToken = this.cookieService.get('user.login.token.engmanangular');
      this.GetUserData();
      return;
    }
    this.router.navigate(["login"]);
    
  }

  private Logout(): void
  {
    this.apiService.Logout().subscribe(obj => 
    {
      this.cookieService.delete(`user.login.token.engmanangular`);
      this.apiService.BearerToken = ``;
      this.apiService.RoleOfUser = ``;
      this.router.navigate([`login`]);
    })
  }

  private GetUserData(): void
  {
    this.apiService.GetUserData().subscribe(
      obj => 
      {
        this.apiService.RoleOfUser = obj.Role;
      },
      error => console.log(error)
    )
  }

  private GetCurrentPage(): string
  {
    let urlToReturn: string = ''
    if(this.router.url.indexOf('?') > 0)
    {
      urlToReturn = this.router.url.substring(1, this.router.url.indexOf('?')).toUpperCase();
    }
    else
    {
      urlToReturn = this.router.url.substring(1).toUpperCase();
    }
    return urlToReturn;
  }
}
