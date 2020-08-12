using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ContactList.Business
{
    public class InputContactListSelect
    {
        [Required, NotNull]
        public Guid UserId { get; set; }
        [Required, NotNull]
        public int UtcOffset { get; set; }
        [Required, NotNull]
        public int PageNumber { get; set; }
        [Required, NotNull]
        public int RowsPerPage { get; set; }
    }
}
