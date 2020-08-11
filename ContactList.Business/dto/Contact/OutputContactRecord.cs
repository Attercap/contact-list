using System;

namespace ContactList.Business
{
    /// <summary>
    /// Data transfer object for contact list rows
    /// </summary>
    public class OutputContactRecord
    {
        public Guid ContactId { get; set; }
        public string UserName { get; set; }
        public int UtcOffset { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string LastModifiedFormatted { get; set; }
    }
}
