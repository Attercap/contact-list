using System;
using System.Collections.Generic;
using System.Text;

namespace ContactList.Business
{
    public class ListSelect
    {
        public string UserName { get; set; }
        public int UtcOffset { get; set; }
        public int PageNumber { get; set; }
        public int RowsPerPage { get; set; }
    }
}
