import { Component, OnInit, DoCheck } from '@angular/core';
import { ApiService } from '../api.service';
import { UserViewModel } from '../app.models';

@Component({
  selector: 'app-profile-info-page',
  templateUrl: './profile-info-page.component.html'
})
export class ProfileInfoPageComponent implements OnInit, DoCheck {

  private CurrentUser: UserViewModel = new UserViewModel();
  private OpenedChangePassword:boolean = false;
  private OldPassword: string = ``;
  private NewPassword: string = ``;
  private ErrorMessageEditProfile: string = ``;
  private ErrorMessageChangePassword: string = ``;
  private SuccessfullyMessage: string = ``;

  constructor(private apiService: ApiService) 
  { 
  }

  ngOnInit(): void
  {
    this.ErrorMessageEditProfile = ``;
    this.ErrorMessageChangePassword = ``;
    this.apiService.GetUserData().subscribe(
      obj =>
      {
        this.CurrentUser = obj;
      },
      error => console.log(error)
    );
  }

  ngDoCheck()
  {
    document.getElementById("first_name_label").className = "active";
    document.getElementById("last_name_label").className = "active";
    document.getElementById("email_label").className = "active";
    document.getElementById("role_label").className = "active";
  }

  SaveChanges(): void
  {
    this.SuccessfullyMessage = ``;
    this.ErrorMessageEditProfile = ``;
    this.ErrorMessageChangePassword = ``;
    this.apiService
    .EditCurrentProfile(this.CurrentUser)
    .subscribe(
      obj =>
      {
        if(obj)
        {
          this.SuccessfullyMessage = `Successfully`;
        }
        else
        {
          this.ErrorMessageEditProfile = `All fields are required. Check your email for correctness.`;
        }
      },
      error =>
      {
        this.ErrorMessageEditProfile = `All fields are required. Check your email for correctness.`;
      }
    )
  }

  ChangeOpenedChangePassword(): void
  {
    if(this.OpenedChangePassword)
    {
      document.getElementById("change_pass_btn").innerText = "Change Password";
    }
    else
    {
      document.getElementById("change_pass_btn").innerText = "Close";
    }
    this.OpenedChangePassword = !this.OpenedChangePassword;
    this.OldPassword = ``;
    this.NewPassword = ``;    
    this.ErrorMessageChangePassword = ``;
  }

  ChangePassword(): void
  {
    this.SuccessfullyMessage = ``;
    this.ErrorMessageEditProfile = ``;
    this.ErrorMessageChangePassword = ``;
    this.apiService
    .ChangePassword(this.CurrentUser.Id, this.OldPassword, this.NewPassword)
    .subscribe(
      obj => 
      {
        if(obj)
        {
          this.SuccessfullyMessage = `Successfully`;
          this.ChangeOpenedChangePassword();
        }
        else
        {
          this.ErrorMessageChangePassword = `Incorrect old password or new password does not pass validation(must contain uppercase / lowercase letter, digit, longer than 8 characters)`;
        }
      },
      error => 
      {
        this.ErrorMessageChangePassword = `Incorrect old password or new password does not pass validation(must contain uppercase / lowercase letter, digit, longer than 8 characters)`;
      }
    )
  }

}
