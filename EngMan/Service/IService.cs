using System.Collections.Generic;
using EngMan.Models;
using System.Threading.Tasks;
using System.Dynamic;
using System.Linq;

namespace EngMan.Service
{
    public interface IService<ReturnType, ParameterType>
    {
        IQueryable<ReturnType> GetAll();

        ReturnType Get(int id);

        bool Add(ParameterType value);

        bool Edit(ParameterType value);

        string Delete(int id);
    }

    public interface IServiceClassifier<ReturnType>
    {
        IQueryable<string> GetAllCategories();

        IQueryable<ReturnType> GetByCategory(string category);
    }

    public interface IServiceTask<ReturnType>
    {
        ReturnType GetTask(string category, string indexes);

        bool VerificationCorrectness(ReturnType img);
    }

    public interface IGuessesTheImageService: 
        IService<GuessesTheImageToReturn, GuessesTheImageToAdd>,
        IServiceClassifier<GuessesTheImageToReturn>,
        IServiceTask<GuessesTheImageToReturn>
    {

    }

    public interface ISentenceTaskService :
        IService<SentenceTask, SentenceTask>,
        IServiceClassifier<SentenceTask>,
        IServiceTask<SentenceTask>
    {

    }

    public interface IRuleService:
        IService<RuleModel, RuleModel>,
        IServiceClassifier<RuleModel>
    {
        //add images to server
        List<string> AddImages(Image[] images);
    }

    public interface IWordService :
        IService<Word, Word>,
        IServiceClassifier<Word>
    {
        //translate - return original and them translations, !translate - return translate and them origins
        //get WordMap task
        MapWord GetTask(string category, string indexes, bool translate);

        //translate - verification correctness of translate, !translate - verification correctness of original
        //verifivation correctness of the WordMap taks
        bool VerificationCorrectness(Word task, bool translate);
    }

    public interface IUserService :
        IService<UserView, User>
    {
        //log in user
        Task<ExpandoObject> Login(UserLogin user);

        //validation of the incoming user
        User ValidateUser(string email, string password);

        //add user to db
        Task<ExpandoObject> Registration(User user);

        //change password of user
        bool ChangePassword(int id, string oldpassword, string newpassword);

        //change role of user
        bool ChangeRole(UserView user);
    }

    public interface IUserDictionaryService
    {
        //get user dictionary
        UserDictionary Get(int id);

        //add word to dictionary of user
        bool Add(int id, UserWord word);

        //delete word from dictionary of user
        string Delete(int userId, int wordId);

        //get all categories of user's words
        IQueryable<string> GetAllCategories(int id);

        //get words of user by category
        UserDictionary GetByCategory(int id, string category);
    }

    public interface IMessageService
    {
        //send message to another user
        bool SendMessage(Message mes, int userId);

        //get messages of user
        IQueryable<ReturnMessage> GetMessages(int userId);

        //delete message of user from db
        string DeleteMessage(int mesId, int userId);

        //read unread messages
        bool ReadMessages(int senderId, int beneficiaryId);

        //get messages with current user and other user
        IQueryable<ReturnMessage> GetMessagesByUserId(int currentUserId, int otherUserId, int lastReceivedMessageId);

        //get new messages
        IQueryable<ReturnMessage> GetNewMessages(int userId);
    }
}