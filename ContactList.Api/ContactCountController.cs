using ContactList.Business;
using Microsoft.AspNetCore.Mvc;

namespace ContactList.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactCountController
    {
        // POST: api/ContactCount
        [HttpPost]
        public DtoBase Post([FromBody] ContactGet getter)
        {
            return ContactManager.GetContactCount(getter);
        }
    }
}
