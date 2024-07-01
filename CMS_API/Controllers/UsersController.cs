using CMS.Business.ClientModels;
using CMS.Data;
using CMS.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CMSDbContext _cmsDbContext;

        public UsersController(CMSDbContext cmsDbContext)
        {
            this._cmsDbContext = cmsDbContext;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUserAsync(AddUserPayload userPayload)
        {
            try
            {
                UserComponent userComponent = new UserComponent();
                string res = await userComponent.AddUserAsync(_cmsDbContext, userPayload);

                if(res == "Email already registered")
                {
                    return Conflict(res);
                }

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
