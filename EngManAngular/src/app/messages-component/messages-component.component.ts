import { Component, OnInit } from '@angular/core';
import * as M from 'materialize-css';
import { UserViewModel, MessageModel, ReturnMessageModel } from '../app.models';
import { ApiService } from '../api.service';
import { MessagesService } from '../messages.service';

@Component({
  selector: 'app-messages-component',
  templateUrl: './messages-component.component.html'
})
export class MessagesComponentComponent implements OnInit {

  private Users: UserViewModel[] = [];
  private ChoosenUser: string = `Main`;
  private Message = "";


  constructor(private apiService: ApiService
    , private messagesService: MessagesService)
  {

  }

  public ngOnInit(): void
  {
    document.addEventListener('DOMContentLoaded', function() {
      let elems = document.querySelectorAll('.modal');
      let instances = M.Modal.init(elems, {});
    });
    this.apiService
    .GetAllUsers()
    .subscribe(
      obj => 
      {
        for(let i = 0; i < obj.length; i++)
        {
          if(obj[i].Id != this.apiService.UserId)
          {
            this.Users.push(obj[i]);
          }
        }
      },
      error => console.log(error)
    )
  }

  private UpdateUser(): void
  {
    this.messagesService.Messages = [];
    let doc:any = document.getElementById("selectUser");
    if(doc.options[doc.selectedIndex].text == "Main")
    {
      this.ChoosenUser = "Main";
      return;
    }
    this.UpdateMessages(doc.options[doc.selectedIndex].text);
  }

  UpdateMessages(email: string): void
  {
    let doc:any = document.getElementById("selectUser");
    doc.value = email;
    this.ChoosenUser = email;
    this.messagesService.GetMessages(this.FindUserByEmail(email).Id, 0);
    this.Message = "";
  }

  private OpenMessages(): void
  {
    document.getElementById("message-icon").className = "medium material-icons";
    this.messagesService.GetNewMessages();
    let doc:any = document.getElementById("selectUser");
    doc.value = "Main";
    this.ChoosenUser = "Main";
    this.Message = "";
  }

  private Send(): void
  {
    let date = new Date(Date.now());
    let hours = date.getHours() < 10 ? "0"+date.getHours() : date.getHours();
    let minutes = date.getMinutes() < 10 ? "0"+date.getMinutes() : date.getMinutes();
    let seconds = date.getSeconds() < 10 ? "0"+date.getSeconds() : date.getSeconds();
    let month = (date.getMonth()+1) < 10 ? "0"+(date.getMonth()+1) : (date.getMonth()+1);
    let day = date.getDate() < 10 ? "0"+date.getDate() : date.getDate();

    let mes = new MessageModel();
    mes.MessageId = 0;
    mes.SenderId = this.apiService.UserId;
    mes.BeneficiaryId = this.FindUserByEmail(this.ChoosenUser).Id;
    mes.Text = this.Message;
    mes.Time =  date.getFullYear() + "-" + month + "-" + day + "T" + hours + ":" + minutes + ":" + seconds + "+03:00";
    mes.CheckReadMes = false;
    this.messagesService.SendMessage(mes);
    this.Message = ``;
  }

  private Delete(mes: ReturnMessageModel): void
  {
    this.messagesService.DeleteMessages(mes);
  }

  private DownloadMore(): void
  {
    this.messagesService.GetMessages(this.FindUserByEmail(this.ChoosenUser).Id, this.messagesService.Messages[this.messagesService.Messages.length - 1].MessageId);
  }

  private FindUserByEmail(email: string): UserViewModel
  {
    return this.Users[this.Users.findIndex((user) => user.Email === email)];
  }

}
