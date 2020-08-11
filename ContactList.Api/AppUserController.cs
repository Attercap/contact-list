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
        public DtoReturnObject<OutputUserBase> Post([FromBody] InputUserRegister user)
        {
            return AppUserManager.CreateUser(user);
        }

        // POST: api/AppUser/Login
        [HttpPost]
        [Route("Login")]
        public DtoReturnObject<OutputUserBase> Post([FromBody] InputUserLogin appUser)
        {
            return AppUserManager.LoginUser(appUser);
        }

        // POST: api/AppUser/Update
        [HttpPost]
        [Route("Update")]
        public DtoReturnBase Post([FromBody] InputUserUpdate appUser)
        {
            return AppUserManager.UpdateUser(appUser);
        }
    }
}
