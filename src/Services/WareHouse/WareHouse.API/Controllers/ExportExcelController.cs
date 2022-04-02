using Aspose.Cells;
using Aspose.Words;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WareHouse.API.Application.Cache.CacheName;
using WareHouse.API.Application.Extensions;
using WareHouse.API.Application.Message;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.GetFisrt;
using WareHouse.API.Application.Queries.Paginated.Unit;
using WareHouse.API.Controllers.BaseController;

namespace WareHouse.API.Controllers
{
    public class ExportExcelController : BaseControllerWareHouse
    {
        private readonly IMediator _mediat;
        private readonly ICacheExtension _cacheExtension;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ExportExcelController(IWebHostEnvironment hostEnvironment, IMediator mediat, ICacheExtension cacheExtension)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
            _cacheExtension = cacheExtension ?? throw new ArgumentNullException(nameof(cacheExtension));
            _hostingEnvironment = hostEnvironment;
        }
        #region R
        #endregion





        [Route("export-out-ward")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> ExportOutWard(string id)
        {
            #region Validation

            if (string.IsNullOrEmpty(id))
            {
                return Ok(new ResultMessageResponse()
                {
                    data = null,
                    success = true,
                    code = new StatusCodeResult((int)HttpStatusCode.BadRequest).ToString(),
                    message = "Bạn chưa nhập mã phiếu xuất !"
                });
            }
            // var res= await _mediat.Send(paginatedList); OutwardGetFirstExcelCommand
            var res = await _mediat.Send(new OutwardGetFirstExcelCommand() { Id = id });
            var entity = res;
            var result = res.OutwardDetails;

            if (entity is null || result is null)
            {
                return Ok(new ResultMessageResponse()
                {
                    data = null,
                    success = true,
                    code = new StatusCodeResult((int)HttpStatusCode.NotFound).ToString(),
                    message = "Không tồn tại mã phiếu xuất !"
                });
            }

            #endregion
            var exportDate = DateTime.Now;
            var inDay = exportDate.Day;
            var inMouth = exportDate.Month;
            var inYear = exportDate.Year;
            var vDay = entity.CreatedDate.Day;
            var vMouth = entity.CreatedDate.Month;
            var vYear = entity.CreatedDate.Year - 2000 < 10 ? "0+" + (entity.CreatedDate.Year - 2000) + "" : "" + (entity.CreatedDate.Year - 2000) + "";
            var voucherCode = entity.VoucherCode;
            var voucherCodeReality = string.IsNullOrEmpty(entity.Voucher) ? entity.VoucherCode : entity.Voucher;
            var description = entity.Description;
            var wareHouse = entity.WareHouse is null ? "" : entity.WareHouse.Name;
            var address = entity.WareHouse is null ? "" : entity.WareHouse.Address;
            var receiver = entity.Receiver;
            var sumNone = 0;
            var priceString = "0";
            var sumUIPrice = 0m;
            var sumAmount = 0m;
            var sumUIQuantity = 0m;

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);


            DataTable dt = new DataTable { TableName = "Outward" };
            dt.Columns.Add("S", typeof(string));
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("Unit", typeof(string));
            dt.Columns.Add("QNone", typeof(string));
            dt.Columns.Add("UIQ", typeof(string));
            dt.Columns.Add("UIPrice", typeof(string));
            dt.Columns.Add("Amount", typeof(string));

            int i = 0;
            foreach (var item in result)
            {
                i++;

                DataRow dr = dt.NewRow();
                dr["S"] = i;
                dr["ItemName"] = item.ItemName;
                dr["Code"] = item.Code;
                dr["Unit"] = item.UnitName;
                dr["QNone"] = 0;
                dr["UIQ"] = item.Uiquantity;
                dr["UIPrice"] = FormatNumbe(item.Uiprice);
                dr["Amount"] = FormatNumbe(item.Amount);

                sumAmount = sumAmount + item.Amount;
                sumUIPrice = sumUIPrice + item.Uiprice;
                sumUIQuantity = sumUIQuantity + item.Uiquantity;
                dt.Rows.Add(dr);
                if (i > 20)
                    break;
            }


            if (sumAmount > 0)
                priceString = NumberToText((double)sumAmount);

            var bcStream = new MemoryStream();
            // 
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "Word", "xuatphieukhosaigon.doc");
            var doc = new Document(Path.GetFullPath(path).Replace("~\\", ""));

            doc.MailMerge.Execute(new[]
            {
                "ExportDate","inDay" ,"inMouth","inYear" ,"vDay" ,"vMouth" ,"vYear" ,"voucherCode","receiver","description","voucherCodeReality" ,"wareHouse","address" , "sumNone","sumUIPrice" ,"sumAmount","sumUIQuantity","PriceString"
            },
            new object[]
            {
                    exportDate,
                    inDay,
                    inMouth,
                    inYear,
                    vDay,
                    vMouth,
                    vYear,
                    voucherCode,
                    receiver,
                    description,
                    voucherCodeReality,
                    wareHouse,
                    address,
                    sumNone,
                    sumUIPrice,
                    sumAmount,
                    sumUIQuantity,
                    priceString
            });

            doc.MailMerge.ExecuteWithRegions(dt);
            doc.MailMerge.DeleteFields();

            var dstStream = new MemoryStream();
            var docSave = doc.Save(dstStream, Aspose.Words.SaveFormat.Pdf);
            dstStream.Position = 0;

            return File(dstStream, docSave.ContentType, "phieu-xuat-" + entity.VoucherCode + ".pdf");


        }




        [Route("export-in-ward")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> ExportInWard(string id)
        {
            #region Validation

            if (string.IsNullOrEmpty(id))
            {
                return Ok(new ResultMessageResponse()
                {
                    data = null,
                    success = true,
                    code = new StatusCodeResult((int)HttpStatusCode.BadRequest).ToString(),
                    message = "Bạn chưa nhập mã phiếu nhập !"
                });
            }
            // var res= await _mediat.Send(paginatedList); OutwardGetFirstExcelCommand
            var res = await _mediat.Send(new OutwardGetFirstExcelCommand() { Id = id });
            var entity = res;
            var result = res.OutwardDetails;

            if (entity is null || result is null)
            {
                return Ok(new ResultMessageResponse()
                {
                    data = null,
                    success = true,
                    code = new StatusCodeResult((int)HttpStatusCode.NotFound).ToString(),
                    message = "Không tồn tại mã phiếu xuất !"
                });
            }

            #endregion
            var exportDate = DateTime.Now;
            var inDay = exportDate.Day;
            var inMouth = exportDate.Month;
            var inYear = exportDate.Year;
            var vDay = entity.CreatedDate.Day;
            var vMouth = entity.CreatedDate.Month;
            var vYear = entity.CreatedDate.Year - 2000 < 10 ? "0+" + (entity.CreatedDate.Year - 2000) + "" : "" + (entity.CreatedDate.Year - 2000) + "";
            var voucherCode = entity.VoucherCode;
            var description = entity.Description;
            var voucher = string.IsNullOrEmpty(entity.Voucher) ? entity.VoucherCode : entity.Voucher;
            var wareHouse = entity.WareHouse is null ? "" : entity.WareHouse.Name;
            var address = entity.WareHouse is null ? "" : entity.WareHouse.Address;
            var sumNone = 0;
            var priceString = "0";
            var sumUIPrice = 0m;
            var sumAmount = 0m;
            var sumUIQuantity = 0m;
            var Deliver = entity.Deliver;

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            #region Invoice details

            DataTable dt = new DataTable { TableName = "Inward" };
            dt.Columns.Add("S", typeof(string));
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("Unit", typeof(string));
            dt.Columns.Add("QNone", typeof(string));
            dt.Columns.Add("UIQ", typeof(string));
            dt.Columns.Add("UIP", typeof(string));
            dt.Columns.Add("Am", typeof(string));

            int i = 0;
            foreach (var item in result)
            {
                i++;

                DataRow dr = dt.NewRow();
                dr["S"] = i;
                dr["ItemName"] = item.ItemName;
                dr["Code"] = item.Code;
                dr["Unit"] = item.UnitName;
                dr["QNone"] = 0;
                dr["UIQ"] = item.Uiquantity;
                dr["UIP"] = FormatNumbe(item.Uiprice);
                dr["Am"] = FormatNumbe(item.Amount);

                sumAmount = sumAmount + item.Amount;
                sumUIPrice = sumUIPrice + item.Uiprice;
                sumUIQuantity = sumUIQuantity + item.Uiquantity;
                dt.Rows.Add(dr);
                if (i > 20)
                    break;
            }
            if (sumAmount > 0)
                priceString = NumberToText((double)sumAmount);
            #endregion

            #region Export word

            var bcStream = new MemoryStream();
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "Word", "inphieukhosaigon.doc");
            var doc = new Aspose.Words.Document(Path.GetFullPath(path).Replace("~\\", ""));

            doc.MailMerge.Execute(new[]
            {
                "ExportDate","inDay" ,"inMouth","inYear" ,"vDay" ,"vMouth" ,"vYear" ,"voucherCode","description","voucher" ,"wareHouse","address" , "sumN","sumUIP" ,"sumAm","sumUIQ","PriceString","Deliver"
            },
            new object[]
            {
                    exportDate,
                    inDay,
                    inMouth,
                    inYear,
                    vDay,
                    vMouth,
                    vYear,
                    voucherCode,
                    description,
                    voucher,
                    wareHouse,
                    address,
                    sumNone,
                    sumUIPrice,
                    sumAmount,
                    sumUIQuantity,
                    priceString,
                    Deliver
            });

            doc.MailMerge.ExecuteWithRegions(dt);
            doc.MailMerge.DeleteFields();

            var dstStream = new MemoryStream();
            var docSave = doc.Save(dstStream, Aspose.Words.SaveFormat.Pdf);
            dstStream.Position = 0;

            return File(dstStream, docSave.ContentType, "phieu-nhap-" + entity.VoucherCode + ".pdf");

            #endregion


        }

        private static string FormatNumbe(decimal item)
        {
            return item.ToString("n4", CultureInfo.CurrentCulture) ?? 0.ToString("n4", CultureInfo.CurrentCulture);
        }
        public static string NumberToText(double inputNumber, bool suffix = true)
        {
            string[] unitNumbers = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] placeValues = new string[] { "", "nghìn", "triệu", "tỷ" };
            bool isNegative = false;

            // -12345678.3445435 => "-12345678"
            string sNumber = inputNumber.ToString("#");
            double number = Convert.ToDouble(sNumber);
            if (number < 0)
            {
                number = -number;
                sNumber = number.ToString();
                isNegative = true;
            }


            int ones, tens, hundreds;

            int positionDigit = sNumber.Length;   // last -> first

            string result = " ";


            if (positionDigit == 0)
                result = unitNumbers[0] + result;
            else
            {
                // 0:       ###
                // 1: nghìn ###,###
                // 2: triệu ###,###,###
                // 3: tỷ    ###,###,###,###
                int placeValue = 0;

                while (positionDigit > 0)
                {
                    // Check last 3 digits remain ### (hundreds tens ones)
                    tens = hundreds = -1;
                    ones = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                    positionDigit--;
                    if (positionDigit > 0)
                    {
                        tens = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                        positionDigit--;
                        if (positionDigit > 0)
                        {
                            hundreds = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                            positionDigit--;
                        }
                    }

                    if ((ones > 0) || (tens > 0) || (hundreds > 0) || (placeValue == 3))
                        result = placeValues[placeValue] + result;

                    placeValue++;
                    if (placeValue > 3) placeValue = 1;

                    if ((ones == 1) && (tens > 1))
                        result = "một " + result;
                    else
                    {
                        if ((ones == 5) && (tens > 0))
                            result = "lăm " + result;
                        else if (ones > 0)
                            result = unitNumbers[ones] + " " + result;
                    }
                    if (tens < 0)
                        break;
                    else
                    {
                        if ((tens == 0) && (ones > 0)) result = "lẻ " + result;
                        if (tens == 1) result = "mười " + result;
                        if (tens > 1) result = unitNumbers[tens] + " mươi " + result;
                    }
                    if (hundreds < 0) break;
                    else
                    {
                        if ((hundreds > 0) || (tens > 0) || (ones > 0))
                            result = unitNumbers[hundreds] + " trăm " + result;
                    }
                    result = " " + result;
                }
            }
            result = result.Trim();
            if (isNegative) result = "Âm " + result;
            return result + (suffix ? " đồng chẵn" : "");
        }

    }
}