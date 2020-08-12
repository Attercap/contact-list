using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ContactList.Business
{
    public class InputContactCountGet
    {
        [Required, NotNull]
        public Guid UserId { get; set; }
    }
}
