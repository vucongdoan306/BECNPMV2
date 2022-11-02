using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CNPM.WEB06.CukCuk.Interfaces;

namespace CNPM.WEB06.CukCuk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase,IBaseController
    {

    }
}
