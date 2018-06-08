import { TestBed, getTestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { ApiService } from './api.service';
import { UserModel, RegistrationUserModel, UserViewModel, UserDictionaryModel, WordModel, RuleModel, SentenceTaskModel, WordTaskModel, GuessesTheImageTaskModel, ReturnMessageModel, MessageModel } from './app.models';
import { HttpResponse } from '@angular/common/http';


describe("ApiServiceTests", function() {
    let injector: TestBed;
    let service: ApiService;
    let httpMock: HttpTestingController;

    beforeEach(() => {
        TestBed.configureTestingModule({
          imports: [HttpClientTestingModule],
          providers: [ApiService]
        });
        injector = getTestBed();
        service = injector.get(ApiService);
        httpMock = injector.get(HttpTestingController);
    });

      
    afterEach(() => {
        httpMock.verify();
    });

    it("Login should return bearer token", () => {
        service.Login(new UserModel()).subscribe(
            obj => 
            {
                expect(obj).toBe("bearer");
            });
      
        const req = httpMock.expectOne(`${service.Url}/account/login`);
        expect(req.request.method).toBe("POST");
        req.flush("bearer");
    });

    it("Registration should return bearer token", () => {
        service.Registration(new RegistrationUserModel()).subscribe(
            obj => 
            {
                expect(obj).toBe("bearer");
            });
      
        const req = httpMock.expectOne(`${service.Url}/account/registration`);
        expect(req.request.method).toBe("POST");
        req.flush("bearer");
    });

    it("Logout should return true", () => {
        service.Logout().subscribe(
            obj => 
            {
                expect(obj).toBe("true");
            });
      
        const req = httpMock.expectOne(`${service.Url}/account/logout`);
        expect(req.request.method).toBe("POST");
        req.flush("true");
    });

    it("GetAllUsers should return 2 users", () => {
        const users: UserViewModel[] = [ new UserViewModel(), new UserViewModel() ];

        service.GetAllUsers().subscribe(
            obj => 
            {
                expect(obj.length).toBe(2);
            });
      
        const req = httpMock.expectOne(`${service.Url}/account/getallusers`);
        expect(req.request.method).toBe("GET");
        req.flush(users);
    });

    it("GetUserData should return good model", () => {
        let user: UserViewModel = new UserViewModel();

        user.Email = "asdf@asdf.asdf";

        service.GetUserData().subscribe(
            obj => 
            {
                expect(obj.Email).toBe("asdf@asdf.asdf");
            });
      
        const req = httpMock.expectOne(`${service.Url}/account/getuserdata`);
        expect(req.request.method).toBe("GET");
        req.flush(user);
    });
    
    it("ChangePassword should return true", () => {

        service.ChangePassword(1, "_", "_").subscribe(
            obj => 
            {
                expect(obj).toBe("true");
            });
      
        const req = httpMock.expectOne(`${service.Url}/account/ChangePassword?id=1&oldpassword=_&newpassword=_`);
        expect(req.request.method).toBe("PUT");
        req.flush("true");
    });

    it("EditCurrentProfile should return true", () => {

        service.EditCurrentProfile(new UserViewModel()).subscribe(
            obj => 
            {
                expect(obj).toBe("true");
            });
      
        const req = httpMock.expectOne(`${service.Url}/account/edituser`);
        expect(req.request.method).toBe("PUT");
        req.flush("true");
    });

    it("GetUserDictionaryCategories should return 2 categories", () => {
        const categories: string[] = ["", ""];
        service.GetUserDictionaryCategories().subscribe(
            obj => 
            {
                expect(obj.length).toBe(2);
            });
      
        const req = httpMock.expectOne(`${service.Url}/account/GetAllCategoriesOfDictionary`);
        expect(req.request.method).toBe("GET");
        req.flush(categories);
    });

    it("GetUserDictionaryByCategory should return 2 words", () => {
        let dictionary: UserDictionaryModel = new UserDictionaryModel();
        dictionary.Words = [new WordModel(), new WordModel()];
        service.GetUserDictionaryByCategory("_").subscribe(
            obj => 
            {
                expect(obj.Words.length).toBe(2);
            });
      
        const req = httpMock.expectOne(`${service.Url}/account/GetByCategoryDictionary?category=_`);
        expect(req.request.method).toBe("GET");
        req.flush(dictionary);
    });

    it("DeleteUserDictionaryWord should return true", () => {
        service.DeleteUserDictionaryWord(1).subscribe(
            obj => 
            {
                expect(obj).toBe("true");
            });
      
        const req = httpMock.expectOne(`${service.Url}/account/deletewordfromdictionary?id=1`);
        expect(req.request.method).toBe("DELETE");
        req.flush("true");
    });

    it("AddWordToDictionaryOfCurrentUser should return true", () => {
        service.AddWordToDictionaryOfCurrentUser(1).subscribe(
            obj => 
            {
                let res: any = obj;
                expect(res.body).toBe(true);
            });
      
        const req = httpMock.expectOne(`${service.Url}/account/addwordtodictionary`);
        expect(req.request.method).toBe("POST");
        req.flush(new HttpResponse<boolean>({body: true}));
    });

    it("GetRules should return 2 rules", () => {
        const rules: RuleModel[] = [new RuleModel(), new RuleModel()];

        service.GetRules().subscribe(
            obj => 
            {
                expect(obj.length).toBe(2);
            });
      
        const req = httpMock.expectOne(`${service.Url}/rule/getallrules`);
        expect(req.request.method).toBe("GET");
        req.flush(rules);
    });

    it("GetSentenceTaskCategories should return 2 categories", () => {
        const categories: string[] = ["", ""];
        service.GetSentenceTaskCategories().subscribe(
            obj => 
            {
                expect(obj.length).toBe(2);
            });
      
        const req = httpMock.expectOne(`${service.Url}/sentencetask/GetAllCategories`);
        expect(req.request.method).toBe("GET");
        req.flush(categories);
    });

    it("GetSenteceTask should return valid model", () => {
        const sentencetask = new SentenceTaskModel();
        sentencetask.Category = "_";
        service.GetSenteceTask("_", "_").subscribe(
            obj => 
            {
                expect(obj.Category).toBe("_");
            });
      
        const req = httpMock.expectOne(`${service.Url}/sentencetask/gettask?category=_&indexes=_`);
        expect(req.request.method).toBe("GET");
        req.flush(sentencetask);
    });

    it("CheckTheAnswerOfSentenceTask should return true", () => {
        service.CheckTheAnswerOfSentenceTask(new SentenceTaskModel()).subscribe(
            obj => 
            {
                let res: any = obj;
                expect(res.body).toBe(true);
            });
      
        const req = httpMock.expectOne(`${service.Url}/sentencetask/verificationcorrectness`);
        expect(req.request.method).toBe("POST");
        req.flush(new HttpResponse<boolean>({body: true}));
    });

    it("GetWordTaskCategories should return 2 categories", () => {
        const categories: string[] = ["", ""];
        service.GetWordCategories().subscribe(
            obj => 
            {
                expect(obj.length).toBe(2);
            });
      
        const req = httpMock.expectOne(`${service.Url}/word/GetAllCategories`);
        expect(req.request.method).toBe("GET");
        req.flush(categories);
    });

    it("GetWordTask should return valid model", () => {
        const wordTask = new WordTaskModel();
        service.GetWordTask("_", "_", true).subscribe(
            obj => 
            {
                expect(obj).toBe(wordTask);
            });
      
        const req = httpMock.expectOne(`${service.Url}/word/getwordmap?category=_&indexes=_&translate=true`);
        expect(req.request.method).toBe("GET");
        req.flush(wordTask);
    });

    it("CheckTheAnswerOfWordTask should return true", () => {
        service.CheckTheAnswerOfWordTask(new WordModel(), true).subscribe(
            obj => 
            {
                let res: any = obj;
                expect(res.body).toBe(true);
            });
      
        const req = httpMock.expectOne(`${service.Url}/word/VerificationCorrectness?translate=true`);
        expect(req.request.method).toBe("POST");
        req.flush(new HttpResponse<boolean>({body: true}));
    });

    it("GetWordsByCategory should return 2 words", () => {
        let words: WordModel[] = [new WordModel(), new WordModel()];
        service.GetWordsByCategory("_").subscribe(
            obj => 
            {
                expect(obj.length).toBe(2);
            });
      
        const req = httpMock.expectOne(`${service.Url}/word/GetByCategory?category=_`);
        expect(req.request.method).toBe("GET");
        req.flush(words);
    });

    it("GetWordCategories should return 2 categories", () => {
        const categories: string[] = ["", ""];
        service.GetWordCategories().subscribe(
            obj => 
            {
                expect(obj.length).toBe(2);
            });
      
        const req = httpMock.expectOne(`${service.Url}/word/GetAllCategories`);
        expect(req.request.method).toBe("GET");
        req.flush(categories);
    });

    it("GetGuessesTheImageTaskCategories should return 2 categories", () => {
        const categories: string[] = ["", ""];
        service.GetGuessesTheImageTaskCategories().subscribe(
            obj => 
            {
                expect(obj.length).toBe(2);
            });
      
        const req = httpMock.expectOne(`${service.Url}/guessestheimage/GetAllCategories`);
        expect(req.request.method).toBe("GET");
        req.flush(categories);
    });

    it("GetGuessesTheImageTask should return valid model", () => {
        const task = new GuessesTheImageTaskModel();
        service.GetGuessesTheImageTask("_", "_").subscribe(
            obj => 
            {
                expect(obj).toBe(task);
            });
      
        const req = httpMock.expectOne(`${service.Url}/guessestheimage/gettask?category=_&indexes=_`);
        expect(req.request.method).toBe("GET");
        req.flush(task);
    });

    it("CheckTheAnswerOfGuessesTheImageTask should return true", () => {
        service.CheckTheAnswerOfGuessesTheImageTask(new GuessesTheImageTaskModel()).subscribe(
            obj => 
            {
                let res: any = obj;
                expect(res.body).toBe(true);
            });
      
        const req = httpMock.expectOne(`${service.Url}/guessestheimage/verificationcorrectness`);
        expect(req.request.method).toBe("POST");
        req.flush(new HttpResponse<boolean>({body: true}));
    });
    
    it("ReadMessages should return true", () => {
        service.ReadMessages(1).subscribe(
            obj => 
            {
                let res: any = obj;
                expect(res.body).toBe(true);
            });
      
        const req = httpMock.expectOne(`${service.Url}/message/readmessages?senderId=1`);
        expect(req.request.method).toBe("POST");
        req.flush(new HttpResponse<boolean>({body: true}));
    });

    it("GetNewMessages should return 2 messages", () => {
        let messages: ReturnMessageModel[] = [new ReturnMessageModel(), new ReturnMessageModel()];
        service.GetNewMessages().subscribe(
            obj => 
            {
                expect(obj.length).toBe(2);
            });
      
        const req = httpMock.expectOne(`${service.Url}/message/getnewmessages/`);
        expect(req.request.method).toBe("GET");
        req.flush(messages);
    });

    it("GetMessagesByUserId should return 2 messages", () => {
        let messages: ReturnMessageModel[] = [new ReturnMessageModel(), new ReturnMessageModel()];
        service.GetMessagesByUserId(1, 0).subscribe(
            obj => 
            {
                expect(obj.length).toBe(2);
            });
      
        const req = httpMock.expectOne(`${service.Url}/message/getmessagesbyuserid?otherUserId=1&lastReceivedMessageId=0`);
        expect(req.request.method).toBe("GET");
        req.flush(messages);
    });

    it("DeleteMessage should return true", () => {
        service.DeleteMessage(1).subscribe(
            obj => 
            {
                expect(obj).toBe("true");
            });
      
        const req = httpMock.expectOne(`${service.Url}/message/deletemessage?id=1`);
        expect(req.request.method).toBe("DELETE");
        req.flush("true");
    });

    it("SendMessage should return true", () => {
        service.SendMessage(new MessageModel()).subscribe(
            obj => 
            {
                let res: any = obj;
                expect(res.body).toBe(true);
            });
      
        const req = httpMock.expectOne(`${service.Url}/message/SendMessage`);
        expect(req.request.method).toBe("POST");
        req.flush(new HttpResponse<boolean>({body: true}));
    });

});