using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ContactList.Business
{
    /// <summary>
    /// Data Transfer object for user updates
    /// </summary>
    public class InputUserUpdate
    {
        [Required]
        public Guid UserId { get; set; }
        [Required, NotNull]
        public string FirstName { get; set; }
        [Required, NotNull]
        public string LastName { get; set; }
        [Required, NotNull]
        public string EmailAddress { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
