using System.ComponentModel.DataAnnotations;

namespace EngMan.Models
{
    public class AdminAccount
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}