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
        public DtoReturnBase Post([FromBody] InputContactCountGet getter)
        {
            return ContactManager.GetContactCount(getter);
        }

        // POST: api/Contact/List
        [HttpPost]
        [Route("List")]
        public DtoReturnObject<List<OutputContactRecord>> Post([FromBody] InputContactListSelect getter)
        {
            return ContactManager.GetContacts(getter);
        }

        // POST: api/Contact/AddEdit
        [HttpPost]
        [Route("AddEdit")]
        public DtoReturnBase Post([FromBody] InputContactRecord contact)
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
        public DtoReturnBase Post([FromBody] OutputContactRecord contact)
        {
            return ContactManager.DeleteContact(contact);
        }
    }
}
