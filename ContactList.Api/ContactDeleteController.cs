using ContactList.Business;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ContactList.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactDeleteController
    {
        // POST: api/ContactDelete
        [HttpPost]
        public DtoBase Post([FromBody] ContactRow contact)
        {
            return ContactManager.DeleteContact(contact);
        }
    }
}
