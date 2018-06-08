import { Injectable } from "@angular/core";
import { RuleModel, UserModel, UserViewModel, RegistrationUserModel, SentenceTaskModel, UserDictionaryModel, WordTaskModel, WordModel, GuessesTheImageTaskModel, ReturnMessageModel, MessageModel } from './app.models'
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class ApiService
{
    public readonly Url:string = 'http://ecsc00a01a18/api';

    public RoleOfUser: string = ``;

    public UserId: number = -1;

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

    public GetAllUsers(): Observable<UserViewModel[]>
    {
        return this.http.get<UserViewModel[]>(this.Url + `/account/getallusers`);
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

    public AddWordToDictionaryOfCurrentUser(id: number): Observable<boolean>
    {
        let toAdd: any = 
        {
            UserId: this.UserId,
            WordId: id
        }
        return this.http.post<boolean>(this.Url + `/account/addwordtodictionary`, toAdd);
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

    public GetWordTask(category: string, indexes: string, translate: boolean): Observable<WordTaskModel>
    {
        return this.http.get<WordTaskModel>(this.Url + `/word/getwordmap?category=${category}&indexes=${indexes}&translate=${translate}`);
    }

    public CheckTheAnswerOfWordTask(word: WordModel, translate: boolean): Observable<boolean>
    {
        return this.http.post<boolean>(this.Url + `/word/VerificationCorrectness?translate=${translate}`, word);
    }

    public GetWordsByCategory(category: string): Observable<WordModel[]>
    {
        return this.http.get<WordModel[]>(this.Url + `/word/GetByCategory?category=${category}`);
    }

    public GetWordCategories(): Observable<string[]>
    {
        return this.http.get<string[]>(this.Url + `/word/GetAllCategories`);
    }

    //GuessesTheImages api.
    public GetGuessesTheImageTaskCategories(): Observable<string[]>
    {
        return this.http.get<string[]>(this.Url + `/guessestheimage/GetAllCategories`);
    }

    public GetGuessesTheImageTask(category:string, indexes:string): Observable<GuessesTheImageTaskModel>
    {
        return this.http.get<GuessesTheImageTaskModel>(this.Url + `/guessestheimage/gettask?category=${category}&indexes=${indexes}`)
    }

    public CheckTheAnswerOfGuessesTheImageTask(task: GuessesTheImageTaskModel): Observable<boolean>
    {
        return this.http.post<boolean>(this.Url + `/guessestheimage/verificationcorrectness`, task)
    }

    //Messages api.
    public ReadMessages(userId: number): Observable<boolean>
    {
        return this.http.post<boolean>(this.Url + `/message/readmessages?senderId=${userId}`, {});
    }

    public GetNewMessages(): Observable<ReturnMessageModel[]>
    {
        return this.http.get<ReturnMessageModel[]>(this.Url + `/message/getnewmessages/`);
    }

    public GetMessagesByUserId(userId: number, lastReceivedMessageId: number): Observable<ReturnMessageModel[]>
    {
        return this.http.get<ReturnMessageModel[]>(this.Url + `/message/getmessagesbyuserid?otherUserId=${userId}&lastReceivedMessageId=${lastReceivedMessageId}`);
    }

    public DeleteMessage(messageId: number): Observable<string>
    {
        return this.http.delete<string>(this.Url + `/message/deletemessage?id=${messageId}`);
    }

    public SendMessage(message: MessageModel): Observable<boolean>
    {
        return this.http.post<boolean>(this.Url + `/message/SendMessage`, message);
    }
    
    // url = http://*host*/api
    // /account/getuserdictionary get
    // /account/addwordtodictionary/ post
    // /account/deleteuser/ delete
    // /account/changerole put
    // /rule/getrule/ get
    // /rule/addrule post
    // /rule/editrule put
    // /rule/deleterule/ delete
    // /rule/GetAllCategories get
    // /rule/GetByCategory?category= get
    // /rule/addimages post
    // /sentencetask/GetAllTasks get
    // /sentencetask/GetTaskById/ get
    // /sentencetask/AddTask post
    // /sentencetask/EditTask put
    // /sentencetask/DeleteTask/ delete
    // /sentencetask/GetByCategory?category= get
    // /word/GetAllWords get
    // /word/GetWordById/ get
    // /word/AddWord post
    // /word/EditWord put
    // /word/DeleteWord/ delete
    // /message/getallmessages/ get
    // /guessestheimage/getalltasks/ get
    // /guessestheimage/gettaskbyid/ get
    // /guessestheimage/edittask/ put
    // /guessestheimage/addtask/ post
    // /guessestheimage/deletetask/ delete
    // /guessestheimage/GetByCategory?category= get
}
