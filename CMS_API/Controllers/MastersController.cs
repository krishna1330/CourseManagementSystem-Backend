using CMS.Business.ClientResponseModels;
using CMS.Business.Models;
using CMS.Data;
using CMS.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MastersController : ControllerBase
    {
        private readonly CMSDbContext _cmsDbContext;

        public MastersController(CMSDbContext cmsDbContext)
        {
            this._cmsDbContext = cmsDbContext;
        }

        [HttpGet("GetMasters")]
        public ActionResult GetMasters()
        {
            try
            {
                MastersComponent mastersComponent = new MastersComponent();
                List<GetMastersResponse> mastersList = new List<GetMastersResponse>();
                mastersList = mastersComponent.GetMasters(_cmsDbContext);
                return Ok(mastersList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
