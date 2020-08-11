using ContactList.Business;
using Microsoft.AspNetCore.Mvc;

namespace ContactList.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController
    {
        // POST: api/AppUser/Register
        [HttpPost]
        [Route("Register")]
        public AppUserReturn Post([FromBody] Register user)
        {
            return AppUserManager.CreateUser(user);
        }

        // POST: api/AppUser/Login
        [HttpPost]
        [Route("Login")]
        public AppUserReturn Post([FromBody] AppUserLogin appUser)
        {
            return AppUserManager.LoginUser(appUser);
        }

        // POST: api/AppUser/Update
        [HttpPost]
        [Route("Update")]
        public DtoBase Post([FromBody] Update appUser)
        {
            return AppUserManager.UpdateUser(appUser);
        }
    }
}
