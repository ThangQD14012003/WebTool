using ImportExcelDapper.Model;
using ImportExcelDapper.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImportExcelDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenServices _tokenServices;
        public TokenController(ITokenServices tokenServices)
        {
            _tokenServices = tokenServices;
        }
        [HttpPost]
        [Route("generate")]
        public async Task<IActionResult> GennerateTokenctl(TokenModel model)
        {
            var extend = "my extended";
            var token = await _tokenServices.GenerateToken(model);
            return Ok(new { token, extend });
        }

        //[HttpGet("generatev1")]
        //public async Task<IActionResult> GenerateTokenctlv1(int length, bool includeUppercase = true, bool includeLowercase = true, bool includeNumbers = true, bool includeSpecialChars = true)
        //{
        //    // này mặc định sẽ là fromquey 
            
        //    var token = _tokenServices.GenerateTokenv1(length, includeUppercase, includeLowercase, includeNumbers, includeSpecialChars);   
        //    return Ok(new { 
        //        token
        //    });   
        //}
    }
}
