using Microsoft.AspNetCore.Mvc;

namespace BaseDoAnTotNghiep.BaseController
{
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class BaseAPIController : ControllerBase
    {
    }
}