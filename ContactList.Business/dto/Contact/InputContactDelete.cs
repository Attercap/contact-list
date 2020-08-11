using System;
using System.ComponentModel.DataAnnotations;

namespace ContactList.Business
{ 
    public class InputContactDelete
    {
        [Required]
        public Guid ContactId { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
