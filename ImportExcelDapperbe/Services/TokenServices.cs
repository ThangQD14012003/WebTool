using ImportExcelDapper.Model;
using System.CodeDom.Compiler;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
namespace ImportExcelDapper.Services
{
    public class TokenServices : ITokenServices
    {
        private static readonly Random random = new Random();
        public string EncodeBase64(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(bytes);
        }

        public string ComputeMD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
                //  Hex Chuẩn: ABCD - 1234 - EF56(với dấu gạch ngang)
                //  Hex Chuẩn Sau Khi Xử Lý: abcd1234ef56(không có dấu gạch ngang và tất cả chữ thường)
                //  return bytes.ToString(); 
            }
        }

        public string ComputeSHA256Hash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        public string ComputeSHA512Hash(string input)
        {
            using (var sha512 = SHA512.Create())
            {
                var bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
        public async Task<string[]> GenerateToken(TokenModel model)
        {
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string number = "0123456789";
            const string special = "!@#$%^&*()-_=+[]{}|;:'\",.<>?/`~";
            var characterSet = new StringBuilder();
            var random = new Random();
            var token = new List<char>();
            //if (!string.IsNullOrEmpty(model.prefix))
            //{
            //    foreach (char c in model.prefix)
            //    {
            //        token.Add(c);
            //    }
            //}
            model.length = model.length - model.prefix.Length;
            if (model.includeUppercase)
            {
                characterSet.Append(upper);
                token.Add(upper[random.Next(upper.Length)]);
                //random.Next(maxValue) trả về một số nguyên ngẫu nhiên trong khoảng từ 0 đến maxValue - 1.
                //Ví dụ, nếu maxValue là 10, random.Next(10) sẽ trả về các giá trị từ 0 đến 9.
            }
            if (model.includeLowercase)
            {
                characterSet.Append(lower);
                token.Add(lower[random.Next(lower.Length)]);
            }
            if (model.includeNumbers)
            {
                characterSet.Append(number);
                token.Add(number[random.Next(number.Length)]);
            }
          if(model.includeSpecialChars)
            {
                token.Add(special[random.Next(special.Length)]);
                characterSet.Append(special); 
            }
            while (token.Count < model.length)
            {
                token.Add(characterSet[random.Next(characterSet.Length)]);  
            }
            string apiToken =  new string(token.OrderBy(_ => random.Next()).ToArray());
            //var fixedPrefix = token.Take(3).ToList();
            //var remainingChars = token.Skip(3).ToList();

            //// Xáo trộn các ký tự còn lại
            //var shuffledRemaining = remainingChars.OrderBy(_ => random.Next()).ToList();

            //// Kết hợp ba ký tự đầu tiên với các ký tự đã xáo trộn
            //fixedPrefix.AddRange(shuffledRemaining);

            //// Trả về chuỗi kết quả
            //return new string(fixedPrefix.ToArray());
            // nếu k dùng fontend để code đoạn prefix thì có thể dùng be để code như trên
            string base64Token = EncodeBase64(apiToken);
            string md5Hash = ComputeMD5Hash(apiToken);
            string sha256Hash = ComputeSHA256Hash(apiToken);
            string sha512Hash = ComputeSHA512Hash(apiToken);
            return new[] { apiToken, base64Token, md5Hash, sha256Hash, sha512Hash };
        }

       
        //public string GenerateTokenv1(int length, bool includeUpperCase, bool includeLowerCase, bool includeNumbers, bool includeSpecialChars)
        //{
        //    const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //    const string lower = "abcdefghijklmnopqrstuvwxyz";
        //    const string number = "0123456789";
        //    const string special = "!@#$%^&*()-_=+[]{}|;:'\",.<>?/`~";
        //    var characterSet = new StringBuilder();
        //    var random = new Random();
        //    var token = new List<char>();

        //    if (includeUpperCase)
        //    {
        //        characterSet.Append(upper);
        //        token.Add(upper[random.Next(upper.Length)]);
        //random.Next(maxValue) trả về một số nguyên ngẫu nhiên trong khoảng từ 0 đến (maxValue - 1).
        //Ví dụ, nếu maxValue là 10, random.Next(10) sẽ trả về các giá trị từ 0 đến 9.
        //    }
        //    if (includeLowerCase)
        //    {
        //        characterSet.Append(lower);
        //        token.Add(lower[random.Next(lower.Length)]);
        //    }
        //    if (includeNumbers)
        //    {
        //        characterSet.Append(number);
        //        token.Add(number[random.Next(number.Length)]);
        //    }
        //    if (includeSpecialChars)
        //    {
        //        token.Add(special[random.Next(special.Length)]);
        //        characterSet.Append(special);
        //    }
        //    while (token.Count < length)
        //    {
        //        token.Add(characterSet[random.Next(characterSet.Length)]);
        //    }
        //    return new string(token.OrderBy(_ => random.Next()).ToArray());
        //}
    }
}
