using System.ComponentModel.DataAnnotations;

namespace ContactList.Business
{
    /// <summary>
    /// Data transfer object for user login
    /// </summary>
    public class InputUserLogin
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}