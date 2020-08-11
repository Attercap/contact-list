using System;
using System.Collections.Generic;
using System.Text;

namespace ContactList.Business
{
    public class DtoReturnObject<T> : DtoReturnBase
    {
        public T ReturnObject { get; set; }

        public DtoReturnObject(bool hasErrors, string dtoMessage, T returnObject)
            : base(hasErrors, dtoMessage)
        {
            ReturnObject = returnObject;
        }
    }
}
