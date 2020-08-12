using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ContactList.Business
{ 
    public class InputContactDelete
    {
        [Required, NotNull]
        public Guid ContactId { get; set; }
        [Required, NotNull]
        public Guid UserId { get; set; }
    }
}
