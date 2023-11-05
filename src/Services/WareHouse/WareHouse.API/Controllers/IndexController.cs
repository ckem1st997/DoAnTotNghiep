using Microsoft.AspNetCore.Mvc;
using Share.Base.Core.Extensions;
using Share.Base.Service;
using Share.Base.Service.Controller;
using Share.Base.Service.Security;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WareHouse.API.Controllers
{
    public class IndexController : ApiController
    {
        private readonly IAuthorizeExtension _authorizeExtension;
        private readonly IRepositoryEF<Domain.Entity.WareHouse> _repository;

        public IndexController(IAuthorizeExtension authorizeExtension)
        {
            _authorizeExtension = authorizeExtension;
            _repository = EngineContext.Current.Resolve<IRepositoryEF<Domain.Entity.WareHouse>>(DataConnectionHelper.ConnectionStringNames.Warehouse);

        }
        [Route("da")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Test()
        {
            var get = await _repository.QueryAsync<object>("SELECT CREATED_BY FROM  ERP_ITEM ");
            return Ok(new MessageResponse()
            {
                success = true,
               
                data= get.Take(10)
            });
        }    
        
        
        [Route("role")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Role()
        {
            return base.Ok(new MessageResponse()
            {
                success = true,
                message = "This is API to check Authorize !"
            });
        }

        [HttpGet]
        [Route("get-list-key")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetList(bool getText)
        {
            var strings = GetKeyRoleHelper.GetKeyItems(getText);
            return Ok(new MessageResponse()
            {
                data = strings,
                totalCount = strings.Count
            });
        }
    }
}
