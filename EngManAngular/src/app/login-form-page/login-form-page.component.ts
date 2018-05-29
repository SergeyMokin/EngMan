import { Component, OnInit } from '@angular/core';
import { UserModel, RegistrationUserModel} from '../app.models'
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-login-form-page',
  templateUrl: './login-form-page.component.html'
})

export class LoginFormPageComponent implements OnInit {

  public User: UserModel;
  public RegistrationUser: RegistrationUserModel;
  public RegistrationFormEnable:boolean = false;
  private ErrorMessage = ``;
  private InProgress = false;

  constructor(private router: Router
    , private cookieService: CookieService
    , private apiService: ApiService) 
  {
    this.User = new UserModel();
    this.RegistrationUser = new RegistrationUserModel();
  }

  ngOnInit(): void
  {
    if(this.cookieService.get('user.login.token.engmanangular').length > 0)
    {
      this.apiService.BearerToken = this.cookieService.get('user.login.token.engmanangular');
      this.router.navigate(["rules"]);
      return;
    }
  }

  ChangeForm(): void
  {
    this.ErrorMessage = ``;
    this.RegistrationFormEnable = !this.RegistrationFormEnable;
  }

  SignUp(): void
  {
    if(this.InProgress) return;
    if(this.cookieService.get('user.login.token.engmanangular') != undefined)
    {
      this.cookieService.delete('user.login.token.engmanangular');
    }
    this.InProgress = true;
    this.apiService.Registration(this.RegistrationUser).subscribe(obj => 
      {
        if(obj.access_token != undefined)
        {
          this.apiService.BearerToken = `bearer ` + obj.access_token
          this.cookieService.set('user.login.token.engmanangular', `bearer ` + obj.access_token);
          this.router.navigate(['rules']);
        }
        else
        {
          this.ErrorMessage = `Check the email address and password (must contain uppercase / lowercase letter, digit, longer than 8 characters)`;
        }
        this.InProgress = false;
      },
      error => 
      {
        this.ErrorMessage = `Check the email address and password (must contain uppercase / lowercase letter, digit, longer than 8 characters)`;
        this.InProgress = false;
      })
  }

  SignIn(): void
  {
    if(this.InProgress) return;
    if(this.cookieService.get('user.login.token.engmanangular') != undefined)
    {
      this.cookieService.delete('user.login.token.engmanangular');
    }
    this.InProgress = true;
    this.apiService.Login(this.User).subscribe(obj => 
      {
        if(obj.access_token != undefined)
        {
          this.apiService.BearerToken = `bearer ` + obj.access_token
          this.cookieService.set('user.login.token.engmanangular', `bearer ` + obj.access_token);
          this.router.navigate(['rules']);
        }
        else
        {
          this.ErrorMessage = `Incorrect email or password.`;
        }
        this.InProgress = false;
      },
      error => 
      {
        this.ErrorMessage = `Incorrect email or password.`;
        this.InProgress = false;
      })
    this.apiService.SetToken('');
  }

  ClearForm(): void
  { 
    location.reload();
  }

}
