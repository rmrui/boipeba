#pragma warning disable 1591
using System.ComponentModel.DataAnnotations;

namespace Boipeba.Web.Models
{
    /// <summary>
    /// View model para tela de login.
    /// </summary>
    public class LoginVM
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}