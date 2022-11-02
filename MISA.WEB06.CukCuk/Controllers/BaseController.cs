using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB06.CukCuk.Interfaces;

namespace MISA.WEB06.CukCuk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase,IBaseController
    {

    }
}
