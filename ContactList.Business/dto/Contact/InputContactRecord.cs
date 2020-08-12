using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ContactList.Business
{
    /// <summary>
    /// Data transfer object add/edit contact rows
    /// </summary>
    public class InputContactRecord
    {

        public Guid? ContactId { get; set; }
        [Required, NotNull]
        public Guid UserId { get; set; }
        [Required, NotNull]
        public string FirstName { get; set; }
        [Required, NotNull]
        public string LastName { get; set; }
        [Required, NotNull]
        public string EmailAddress { get; set; }
        [Required, NotNull]
        public string StreetAddress1 { get; set; }

        public string StreetAddress2 { get; set; }
        [Required, NotNull]
        public string City { get; set; }
        [Required, NotNull]
        public string StateProvince { get; set; }
        [Required, NotNull]
        public string PostalCode { get; set; }
        [Required, NotNull]
        public string Country { get; set; }
    }
}
