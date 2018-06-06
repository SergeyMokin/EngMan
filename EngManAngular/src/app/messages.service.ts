import { Injectable } from "@angular/core";
import { SignalR, BroadcastEventListener, ISignalRConnection } from "ng2-signalr";
import { UserViewModel } from "./app.models";

@Injectable()
export class MessagesService
{
    public UpdateMessagesSubscription: BroadcastEventListener<void>
    public Connection: ISignalRConnection
    public Disconnect: any;

    constructor(private signalR: SignalR)
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
              this.UpdateMessagesSubscription.subscribe(() => document.getElementById("message-icon").className += " animation-new-mess-blink")
            })
            .catch(error => console.log(error))
          })
        .catch(() => console.log("Can not connect."))
    }
}