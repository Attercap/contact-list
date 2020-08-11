using System;
using System.Collections.Generic;
using System.Text;

namespace ContactList.Business
{
    /// <summary>
    /// Data Transfer object for user registration/updates
    /// </summary>
    public class Update
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}
