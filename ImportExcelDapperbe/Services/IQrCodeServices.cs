using ImportExcelDapper.Model;
using Microsoft.AspNetCore.Mvc;

namespace ImportExcelDapper.Services
{
    public interface IQrCodeServices
    {
        public byte[] DecodeBase64String(string base64String);
        public Task<IActionResult> GenQrCode(QrcodeDto model);
        public bool IsCorner(int x, int y, int matrixSize);
    }
}
