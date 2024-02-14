using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Zhiyun.Web.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NetworkController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Update([FromBody]string network)
        {
            return Ok(network);
        }
    }
}
