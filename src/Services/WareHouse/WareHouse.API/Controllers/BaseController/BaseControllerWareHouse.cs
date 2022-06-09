using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WareHouse.API.Application.Authentication;
using WareHouse.API.Application.Extensions;

namespace WareHouse.API.Controllers.BaseController
{
  //  [Authorize(Roles = "User,Admin,Manager")]
   // [CheckRole(LevelCheck.READ)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class BaseControllerWareHouse : ControllerBase
    {
        public BaseControllerWareHouse()
        {
        }
        private static void SetLicense()
        {
            new Aspose.BarCode.License().SetLicense(AsposeHelper.BarCodeLicenseStream);
            new Aspose.Cells.License().SetLicense(AsposeHelper.CellsLicenseStream);
            new Aspose.Pdf.License().SetLicense(AsposeHelper.PdfLicenseStream);
            new Aspose.Words.License().SetLicense(AsposeHelper.WordsLicenseStream);

            // Fix tương thích trên .NET Core
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
    }

}