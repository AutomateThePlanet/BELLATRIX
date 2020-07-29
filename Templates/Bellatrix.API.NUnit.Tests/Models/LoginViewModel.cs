using System.ComponentModel.DataAnnotations;

namespace Bellatrix.API.NUnit.Tests.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
