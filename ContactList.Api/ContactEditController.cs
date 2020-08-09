using ContactList.Business;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ContactList.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactEditController
    {
        // POST: api/ContactEdit
        [HttpPost]
        public DtoBase Post([FromBody] ContactRow contact)
        {
            if(contact.ContactId == null || contact.ContactId == Guid.Empty)
            {
                return ContactManager.AddContact(contact);
            }

            return ContactManager.EditContact(contact);
        }
    }
}
