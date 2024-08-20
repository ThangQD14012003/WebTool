using ImportExcelDapper.Model;

namespace ImportExcelDapper.Services
{
    public interface ITokenServices
    {
        public string EncodeBase64(string value); 
        public string ComputeMD5Hash(string value); 
        public string ComputeSHA256Hash(string value);  
        public string ComputeSHA512Hash(string value);
        public Task<string[]> GenerateToken(TokenModel model); 
    }
}
