using System.ComponentModel.DataAnnotations;

namespace EngMan.Account
{
    public class AdminAccountModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}