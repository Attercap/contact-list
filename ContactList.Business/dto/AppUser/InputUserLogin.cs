using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ContactList.Business
{
    /// <summary>
    /// Data transfer object for user login
    /// </summary>
    public class InputUserLogin
    {
        [Required, NotNull]
        public string UserName { get; set; }
        [Required, NotNull]
        public string Password { get; set; }
    }
}