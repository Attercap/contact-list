using ContactList.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ContactList.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController
    {

        [HttpPost]
        [Route("Count")]
        public DtoBase Post([FromBody] CountGet getter)
        {
            return ContactManager.GetContactCount(getter);
        }

        // POST: api/Contact/List
        [HttpPost]
        [Route("List")]
        public List<ContactRow> Post([FromBody] ListSelect getter)
        {
            return ContactManager.GetContacts(getter);
        }

        // POST: api/Contact/AddEdit
        [HttpPost]
        [Route("AddEdit")]
        public DtoBase Post([FromBody] ContactAddEdit contact)
        {
            if (contact.ContactId == null || contact.ContactId == Guid.Empty)
            {
                return ContactManager.AddContact(contact);
            }

            return ContactManager.EditContact(contact);
        }

        // POST: api/Contact/Delete
        [HttpPost]
        [Route("Delete")]
        public DtoBase Post([FromBody] ContactRow contact)
        {
            return ContactManager.DeleteContact(contact);
        }
    }
}
