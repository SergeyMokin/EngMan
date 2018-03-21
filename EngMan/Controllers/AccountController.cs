using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using EngMan.Service;
using EngMan.Models;
using System.Linq;
using System.Dynamic;

namespace EngMan.Controllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        private readonly IUserService service;
        private readonly IUserDictionaryService serviceDictionary;

        public AccountController(IUserService _service, IUserDictionaryService _serviceDictionary)
        {
            service = _service;
            serviceDictionary = _serviceDictionary;
        }

        //POST api/account/login
        [AllowAnonymous]
        [HttpPost]
        public async Task<ExpandoObject> Login(UserLogin user)
        {
            //fields of ExpandoObject: access_token, token_type, expires_in,
            //userName, .issued, .expires
            return await service.Login(user);
        }

        //POST api/account/registration
        [AllowAnonymous]
        [HttpPost]
        public async Task<ExpandoObject> Registration(User user)
        {
            //fields of ExpandoObject: access_token, token_type, expires_in,
            //userName, .issued, .expires
            return await service.Registration(user);
        }
        
        //GET api/account/GetAllUsers
        [HttpGet]
        public IQueryable<UserView> GetAllUsers()
        {
            return service.GetAll();
        }

        //GET api/account/GetAllCategoriesOfDictionary
        [HttpGet]
        public IQueryable<string> GetAllCategoriesOfDictionary()
        {
            return serviceDictionary.GetAllCategories(GetCurrentUserId());
        }

        //GET api/account/GetByCategoryDictionary
        [HttpGet]
        public UserDictionary GetByCategoryDictionary(string category)
        {
            return serviceDictionary.GetByCategory(GetCurrentUserId(), category);
        }

        //GET api/account/GetUserDictionary
        [HttpGet]
        public UserDictionary GetUserDictionary()
        {
            return serviceDictionary.Get(GetCurrentUserId());
        }

        //POST api/account/AddWordToDictionary
        [HttpPost]
        public bool AddWordToDictionary(UserWord word)
        {
            return serviceDictionary.Add(GetCurrentUserId(), word);
        }

        //DELETE api/account/DeleteWordFromDictionary
        [HttpDelete]
        public string DeleteWordFromDictionary(int id)
        {
            return serviceDictionary.Delete(GetCurrentUserId(), id);
        }

        //GET api/account/GetUserData
        [HttpGet]
        public UserView GetUserData()
        {
            return service.Get(GetCurrentUserId());
        }

        //GET api/account/GetUserById
        [HttpGet]
        public UserView GetUserById(int id)
        {
            return service.Get(id);
        }

        //PUT api/account/EditUser
        [HttpPut]
        public bool EditUser(UserView user)
        {
            return service.Edit(new User(user));
        }

        //DELETE api/account/DeleteUser
        //todo: edit delete to int?
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public string DeleteUser(int id)
        {
            return service.Delete(id);
        }
        
        //POST api/account/LogOut
        [HttpPost]
        public string LogOut()
        {
            //sign out
            HttpContext.Current.GetOwinContext().Authentication.SignOut();
            return "Successfully completed";
        }

        //PUT api/account/ChangeRole
        [HttpPut]
        [Authorize(Roles = "admin")]
        public bool ChangeRole(UserView user)
        {
            return service.ChangeRole(user);
        }

        //PUT api/account/ChangePassword
        [HttpPut]
        public bool ChangePassword(string oldpassword, string newpassword)
        {
            return service.ChangePassword(GetCurrentUserId(), oldpassword, newpassword);
        }

        //get id of current user
        private int GetCurrentUserId()
        {
            return int.Parse(HttpContext.Current.GetOwinContext().Authentication.User.Claims.Select(x => x).ElementAt(0).Value);
        }
    }
}
