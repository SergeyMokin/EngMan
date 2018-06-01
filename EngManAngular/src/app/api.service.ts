import { Injectable } from "@angular/core";
import { RuleModel, UserModel, UserViewModel, RegistrationUserModel, SentenceTaskModel, UserDictionaryModel } from './app.models'
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
    public SetToken(token:string): void
    {
        this.BearerToken = token;
    }

    public DeleteToken(): void
    {
        this.BearerToken = ``;
    }
    
    public Login(model: UserModel): Observable<any>
    {
        return this.http.post(this.Url + `/account/login`, model);
    }

    public Registration(model: RegistrationUserModel): Observable<any>
    {
        return this.http.post(this.Url + `/account/registration`, model);
    }

    public Logout(): Observable<any>
    {
        return this.http.post(this.Url + `/account/logout`, {});
    }

    public GetUserData(): Observable<UserViewModel>
    {
        return this.http.get<UserViewModel>(this.Url + `/account/getuserdata`);
    }

    public ChangePassword(id: number, oldpassword: string, newpassword: string)
    {
        return this.http.put(this.Url + `/account/ChangePassword?id=${id}&oldpassword=${oldpassword}&newpassword=${newpassword}`, {});
    }

    public EditCurrentProfile(user: UserViewModel)
    {
        return this.http.put(this.Url + '/account/edituser', user);
    }

    public GetUserDictionaryCategories(): Observable<string[]>
    {
        return this.http.get<string[]>(this.Url + '/account/GetAllCategoriesOfDictionary');
    }

    public GetUserDictionaryByCategory(category: string): Observable<UserDictionaryModel>
    {
        return this.http.get<UserDictionaryModel>(this.Url + `/account/GetByCategoryDictionary?category=${category}`)
    }

    public DeleteUserDictionaryWord(id: number): Observable<string>
    {
        return this.http.delete<string>(this.Url + `/account/deletewordfromdictionary?id=${id}`);
    }

    //Rule api.
    public GetRules(): Observable<RuleModel[]> 
    {
        return this.http.get<RuleModel[]>(this.Url + `/rule/getallrules`);
    }

    //SentenceTask api.
    public GetSentenceTaskCategories(): Observable<string[]>
    {
        return this.http.get<string[]>(this.Url + `/sentencetask/GetAllCategories`);
    }

    public GetSenteceTask(category:string, indexes:string): Observable<SentenceTaskModel>
    {
        return this.http.get<SentenceTaskModel>(this.Url + `/sentencetask/gettask?category=${category}&indexes=${indexes}`)
    }

    public CheckTheAnswerOfSentenceTask(sentence: SentenceTaskModel): Observable<boolean>
    {
        return this.http.post<boolean>(this.Url + `/sentencetask/verificationcorrectness`, sentence)
    }

    //Word api.
    public GetWordTaskCategories(): Observable<string[]>
    {
        return this.http.get<string[]>(this.Url + `/word/GetAllCategories`);
    }
    
    // url = http://*host*/api
    // /account/getuserdictionary get
    // /account/addwordtodictionary/ post
    // /account/getallusers get
    // /account/deleteuser/ delete
    // /account/changerole put
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
