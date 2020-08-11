using System;
using System.Collections.Generic;
using System.Text;

namespace ContactList.Business
{
    /// <summary>
    /// Data Transfer object following most user API calls
    /// </summary>
    public class OutputUserBase
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}
