using ImportExcelDapper.Model;
using ImportExcelDapper.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImportExcelDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QrCodeController : ControllerBase
    {
        public readonly IQrCodeServices _qrCodeServices;
        public QrCodeController(IQrCodeServices qrCodeServices)
        {
            _qrCodeServices = qrCodeServices;   
        }
        [HttpPost]
        [Route("genqrcode")]
        public async Task<IActionResult> GetQrCode(QrcodeDto qrCode)
        {
            try
            {
                return Ok(await _qrCodeServices.GenQrCode(qrCode));
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
            
        }
        //[HttpPost]
        //[Route("genqrcodesms")]
        //public async Task<IActionResult> GetQrCodeForSms(QrcodeDto qrCode)
        //{
        //    try
        //    {
        //        return Ok(await _qrCodeServices.GenQrCodeForSms(qrCode));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}
    }
}
