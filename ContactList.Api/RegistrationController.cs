using ContactList.Business;
using Microsoft.AspNetCore.Mvc;

namespace ContactList.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        // POST: api/Registration
        [HttpPost]
        public AppUserReturn Post([FromBody] AppUser user)
        {
            return AppUserManager.CreateUser(user);
        }
    }
}
