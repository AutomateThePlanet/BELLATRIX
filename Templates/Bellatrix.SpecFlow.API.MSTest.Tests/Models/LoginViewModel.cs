using System.ComponentModel.DataAnnotations;

namespace Bellatrix.SpecFlow.API.MSTest.Tests
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
