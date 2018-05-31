import { Injectable } from "@angular/core"

@Injectable()
export class RegistrationUserModel
{
    public Id: number;

    public FirstName: string;

    public LastName: string;
    
    public Email: string;

    public Password: string;

    public Role: string = `user`;
}

@Injectable()
export class UserModel
{
    public Email: string;
    public Password: string;
}

@Injectable()
export class UserViewModel
{
    public Id: number;

    public FirstName: string;

    public LastName: string;

    public Email: string;

    public Role: string;
}

@Injectable()
export class UserHubConnectModel
{
    public Id: number;

    public ConnectionId: string;

    public FirstName: string;

    public LastName: string;

    public Email: string;

    public Role: string;
}

@Injectable()
export class RuleModel
{
    public RuleId: number
    public Title: string
    public Text: string
    public Category: string
}

@Injectable()
export class SentenceTaskModel
{
    public SentenceTaskId: number;
    
    public Sentence: string;
    
    public Category: string;
    
    public Translate: string;
}

@Injectable()
export class UserDictionaryModel
{
    public User: UserViewModel;
    public Words: WordModel[];
}

@Injectable()
export class WordModel
{
    public WordId: number;
    public Original: string;
    public Translate: string;
    public Category: string;
    public Transcription: string;
}