import { Injectable } from "@angular/core";
import { SignalR, BroadcastEventListener, ISignalRConnection } from "ng2-signalr";
import { UserViewModel, ReturnMessageModel, MessageModel } from "./app.models";
import { ApiService } from "./api.service";

@Injectable()
export class MessagesService
{
    public UpdateMessagesSubscription: BroadcastEventListener<void>
    public Connection: ISignalRConnection
    public Disconnect: any;

    public Messages: ReturnMessageModel[] = [];
    public NewMessages: ReturnMessageModel[] = [];

    constructor(private signalR: SignalR,
      private apiService: ApiService)
    {
        
    }

    public Connect(user: UserViewModel): void
    {
        this.signalR
        .connect()
        .then(connection => 
          {
            this.Connection = connection;
            this.Connection.invoke("Connect", user)
            .then(() => 
            {
              this.UpdateMessagesSubscription = connection.listenFor("onUpdateMessages");
              this.UpdateMessagesSubscription.subscribe(() => {
                document.getElementById("message-icon").className = "medium material-icons animation-new-mess-blink";
                this.GetNewMessages();
              })
            })
            .catch(error => console.log(error))
          })
        .catch(() => console.log("Can not connect."))
    }

    public GetNewMessages(): void
    {
      this.apiService
      .GetNewMessages()
      .subscribe(
        obj => 
        {
          this.NewMessages = obj
          if(this.NewMessages.length > 0)
          {
            document.getElementById("message-icon").className = "medium material-icons animation-new-mess-blink";
          }
          else
          {
            document.getElementById("message-icon").className = "medium material-icons";
          }
        },
        error => console.log(error)
      )
    }

    public GetMessages(id: number, idOfLastMessage: number): void
    {
      this.ReadMessages(id);
      this.apiService
      .GetMessagesByUserId(id, idOfLastMessage)
      .subscribe(
        obj => 
        {
          for(let i = 0; i < obj.length; i++)
          {
            this.Messages.push(obj[i]);
          }
        },
        error => console.log(error)
      )
    }

    private ReadMessages(id: number): void
    {
      this.apiService
      .ReadMessages(id)
      .subscribe(
        obj => this.GetNewMessages(),
        error => console.log(error)
      )
    }

    public DeleteMessages(mes: ReturnMessageModel): void
    {
      this.apiService
      .DeleteMessage(mes.MessageId)
      .subscribe(
        obj => 
        {
          if(obj === `Delete completed successful`)
          {
            let index = this.Messages.indexOf(mes);
            if(index > -1)
            {
              this.Messages.splice(index, 1);
            }
          }
        },
        error => console.log(error)
      )
    }

    public SendMessage(mes: MessageModel)
    {
      this.apiService
      .SendMessage(mes)
      .subscribe(
        obj => 
        {
          if(obj)
          {
            this.Messages = [];
            this.GetMessages(mes.BeneficiaryId, 0);
          }
        },
        error => console.log(error)
      )
    }
}