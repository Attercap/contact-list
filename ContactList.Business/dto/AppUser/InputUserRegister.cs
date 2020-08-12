using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ContactList.Business
{
    /// <summary>
    /// Data Transfer object for user registration
    /// </summary>
    public class InputUserRegister
    {
        [Required, NotNull]
        public string UserName { get; set; }
        [Required, NotNull]
        public string Password { get; set; }
        [Required, NotNull]
        public string FirstName { get; set; }
        [Required, NotNull]
        public string LastName { get; set; }
        [Required, NotNull]
        public string EmailAddress { get; set; }
    }
}
