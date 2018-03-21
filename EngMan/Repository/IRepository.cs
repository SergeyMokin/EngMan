using System.Collections.Generic;
using System.Linq;
using EngMan.Models;

namespace EngMan.Repository
{
    public interface IRepository<ReturnType, ParameterType>
    {
        IQueryable<ReturnType> GetAll();

        ReturnType Get(int id);

        bool Add(ParameterType value);

        bool Edit(ParameterType value);

        int Delete(int id);
    }

    public interface IRepositoryClassifier<ReturnType>
    {
        IQueryable<string> GetAllCategories();

        IQueryable<ReturnType> GetByCategory(string category);
    }

    public interface IRepositoryTask<ReturnType>
    {
        IQueryable<ReturnType> GetTasks(string category, IEnumerable<int> indexes = default(int[]));
    }

    public interface ISentenceTaskRepository :
        IRepository<SentenceTask, SentenceTask>,
        IRepositoryClassifier<SentenceTask>,
        IRepositoryTask<SentenceTask>
    {

    }

    public interface IWordRepository :
        IRepository<Word, Word>,
        IRepositoryClassifier<Word>,
        IRepositoryTask<Word>
    {

    }

    public interface IGuessesTheImageRepository :
        IRepository<GuessesTheImageToReturn, GuessesTheImageToAdd>,
        IRepositoryClassifier<GuessesTheImageToReturn>,
        IRepositoryTask<GuessesTheImageToReturn>
    {

    }

    public interface IRuleRepository : 
        IRepository<RuleModel, RuleModel>, 
        IRepositoryClassifier<RuleModel>
    {

    }

    public interface IUserRepository : 
        IRepository<User, User>
    {
        bool ChangePassword(int id, string oldpassword, string newpassword);

        bool ChangeRole(UserView user);
    }

    public interface IUserDictionaryRepository
    {
        UserDictionary GetUserDictionary(int id);

        bool Add(int id, UserWord word);

        int Delete(int userId, int wordId);

        IQueryable<string> GetAllCategories(int id);

        UserDictionary GetByCategory(int id, string category);
    }

    public interface IMessageRepository
    {
        bool SendMessage(Message mes, int userId);

        IQueryable<ReturnMessage> GetMessages(int userId);

        int DeleteMessage(int mesId, int userId);

        int ReadMessages(int senderId, int beneficiaryId);

        IQueryable<ReturnMessage> GetMessagesByUserId(int currentUserId, int otherUserId, int lastReceivedMessageId);

        IQueryable<ReturnMessage> GetNewMessages(int userId);
    }
}