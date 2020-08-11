using System;
using System.ComponentModel.DataAnnotations;

namespace ContactList.Business
{
    /// <summary>
    /// Data transfer object add/edit contact rows
    /// </summary>
    public class InputContactRecord
    {
        public Guid? ContactId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string StreetAddress1 { get; set; }
        [Required]
        public string StreetAddress2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string StateProvince { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
