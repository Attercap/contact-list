using ContactList.Business;
using Microsoft.AspNetCore.Mvc;

namespace ContactList.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController
    {
        // POST: api/Login
        [HttpPost]
        public AppUserReturn Post([FromBody]AppUserLogin appUser)
        {
            return AppUserManager.LoginUser(appUser);
        }
    }
}
