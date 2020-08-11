using System;
using System.Collections.Generic;
using System.Text;

namespace ContactList.Business
{
    /// <summary>
    /// Data transfer object add/edit contact rows
    /// </summary>
    public class ContactAddEdit
    {
        public Guid ContactId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
