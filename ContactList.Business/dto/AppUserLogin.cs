using System;
using System.Collections.Generic;
using System.Text;

namespace ContactList.Business
{
    /// <summary>
    /// Data transfer object for user login
    /// </summary>
    public class AppUserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}