export class RegistrationUserModel
{
    public Id: number;

    public FirstName: string;

    public LastName: string;
    
    public Email: string;

    public Password: string;

    public Role: string = `user`;
}

export class UserModel
{
    public Email: string;

    public Password: string;
}

export class UserViewModel
{
    public Id: number;

    public FirstName: string;

    public LastName: string;

    public Email: string;

    public Role: string;
}

export class UserHubConnectModel
{
    public Id: number;

    public ConnectionId: string;

    public FirstName: string;

    public LastName: string;

    public Email: string;

    public Role: string;
}

export class RuleModel
{
    public RuleId: number;

    public Title: string;

    public Text: string;

    public Category: string;
}

export class SentenceTaskModel
{
    public SentenceTaskId: number;
    
    public Sentence: string;
    
    public Category: string;
    
    public Translate: string;
}

export class UserDictionaryModel
{
    public User: UserViewModel;

    public Words: WordModel[];
}

export class WordModel
{
    public WordId: number;

    public Original: string;

    public Translate: string;

    public Category: string;

    public Transcription: string;
}

export class WordTaskModel
{
    public WordId: number;

    public Word: string;

    public Answers: string[];
    
    public Category: string;
}