using System;
using System.Collections.Generic;
using System.Text;

namespace ContactList.Business
{
    /// <summary>
    /// Base properties for dto objects (used primarily for success/error messaging)
    /// </summary>
    public class DtoBase
    {
        public bool HasErrors { get; set; }
        public string DtoMessage { get; set; }
    }
}
