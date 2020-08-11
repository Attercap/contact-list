using System;
using System.ComponentModel.DataAnnotations;

namespace ContactList.Business
{
    public class InputContactListSelect
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int UtcOffset { get; set; }
        [Required]
        public int PageNumber { get; set; }
        [Required]
        public int RowsPerPage { get; set; }
    }
}
