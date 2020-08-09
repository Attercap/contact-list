using ContactList.Business;
using Microsoft.AspNetCore.Mvc;

namespace ContactList.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateUserController
    {
        // POST: api/UpdateUser
        [HttpPost]
        public DtoBase Post([FromBody] AppUser appUser)
        {
            return AppUserManager.UpdateUser(appUser);
        }
    }
}
