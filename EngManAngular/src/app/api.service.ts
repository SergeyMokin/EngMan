import { Injectable } from "@angular/core";
import { RuleModel, UserModel, UserViewModel, RegistrationUserModel, SentenceTaskModel } from './app.models'
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class ApiService
{
    private readonly Url:string = 'http://ecsc00a01a18/api';

    public RoleOfUser: string = ``;

    public BearerToken:string = ``;

    constructor(private http: HttpClient) 
    {

    }

    //Account api.
    SetToken(token:string): void
    {
        this.BearerToken = token;
    }

    DeleteToken(): void
    {
        this.BearerToken = ``;
    }
    
    Login(model: UserModel): Observable<any>
    {
        return this.http.post(this.Url + `/account/login`, model);
    }

    Registration(model: RegistrationUserModel): Observable<any>
    {
        return this.http.post(this.Url + `/account/registration`, model);
    }

    Logout(): Observable<any>
    {
        return this.http.post(this.Url + `/account/logout`, {});
    }

    GetUserData(): Observable<UserViewModel>
    {
        return this.http.get<UserViewModel>(this.Url + `/account/getuserdata`);
    }

    //Rule api.
    GetRules(): Observable<RuleModel[]> 
    {
        return this.http.get<RuleModel[]>(this.Url + `/rule/getallrules`);
    }

    //SentenceTask api.
    GetSentenceTaskCategories(): Observable<string[]>
    {
        return this.http.get<string[]>(this.Url + `/sentencetask/GetAllCategories`);
    }

    GetSenteceTask(category:string, indexes:string): Observable<SentenceTaskModel>
    {
        return this.http.get<SentenceTaskModel>(this.Url + `/sentencetask/gettask?category=${category}&indexes=${indexes}`)
    }

    CheckTheAnswerOfSentenceTask(sentence: SentenceTaskModel): Observable<boolean>
    {
        return this.http.post<boolean>(this.Url + `/sentencetask/verificationcorrectness`, sentence)
    }
    
    // url = http://*host*/api
    // /account/getuserdictionary get
    // /account/deletewordfromdictionary/ delete
    // /account/addwordtodictionary/ post
    // /account/getallusers get
    // /account/deleteuser/ delete
    // /account/changerole put
    // /account/ChangePassword?id= put
    // /account/edituser put
    // /account/GetAllCategoriesOfDictionary get
    // /account/GetByCategoryDictionary?category= get
    // /rule/getrule/ get
    // /rule/addrule post
    // /rule/editrule put
    // /rule/deleterule/ delete
    // /rule/GetAllCategories get
    // /rule/GetByCategory?category= get
    // /rule/addimages post
    // /sentencetask/gettask?category=' + category + '&indexes= get
    // /sentencetask/verificationcorrectness post
    // /word/getwordmap?category=' + category + '&indexes=' + indexes + '&translate= get
    // /word/VerificationCorrectness?translate=' + translate post
    // /sentencetask/GetAllTasks get
    // /sentencetask/GetTaskById/ get
    // /sentencetask/AddTask post
    // /sentencetask/EditTask put
    // /sentencetask/DeleteTask/ delete
    // /sentencetask/GetByCategory?category= get
    // /word/GetAllCategories get
    // /word/GetAllWords get
    // /word/GetWordById/ get
    // /word/AddWord post
    // /word/EditWord put
    // /word/DeleteWord/ delete
    // /word/GetByCategory?category= get
    // /message/readmessages?senderId= post
    // /message/getallmessages/ get
    // /message/getnewmessages/ get
    // /message/getmessagesbyuserid?otherUserId=' + otherUserId + '&lastReceivedMessageId=' + lastReceivedMessageId get
    // /message/deletemessage/ delete
    // /message/SendMessage post
    // /guessestheimage/gettask?category=' + category + "&indexes= get
    // /guessestheimage/verificationcorrectness/ post
    // /guessestheimage/getalltasks/ get
    // /guessestheimage/gettaskbyid/ get
    // /guessestheimage/edittask/ put
    // /guessestheimage/addtask/ post
    // /guessestheimage/deletetask/ delete
    // /guessestheimage/GetAllCategories get
    // /guessestheimage/GetByCategory?category= get
    // hubConnection('http://ecsc00a01a18')
    // get new messages in real time: onUpdateMessages
    // connect to server: Connect + model UserView
}
