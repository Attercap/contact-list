using ContactList.Business;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ContactList.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactListController
    {
        // POST: api/ContactCount
        [HttpPost]
        public List<ContactRow> Post([FromBody] ContactGet getter)
        {
            return ContactManager.GetContacts(getter);
        }
    }
}
