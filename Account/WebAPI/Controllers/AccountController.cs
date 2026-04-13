using Account.Application.Dtos.User;
using Account.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Account.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var result = await _userService.LoginAsync(request);
            return Ok();
        }

        [HttpGet("Encrypt")]
        public IEnumerable<string> Encrypt(string message)
        {
            using var receiver = ECDiffieHellman.Create(ECCurve.NamedCurves.nistP256);
          //  byte[] receiverPublicKey1 = receiver.PublicKey.ToByteArray();
            byte[] receiverPublicKey = receiver.ExportSubjectPublicKeyInfo();


            // ===== Sender generates ECC key pair =====
            using var sender = ECDiffieHellman.Create(ECCurve.NamedCurves.nistP256);
            byte[] senderPublicKey = sender.ExportSubjectPublicKeyInfo();

            // ===== Both derive the same shared secret =====
            using var receiverPub = ECDiffieHellman.Create();
            receiverPub.ImportSubjectPublicKeyInfo(receiverPublicKey, out _);

            using var senderPub = ECDiffieHellman.Create();
            senderPub.ImportSubjectPublicKeyInfo(senderPublicKey, out _);

            byte[] senderSecret = sender.DeriveKeyMaterial(receiverPub.PublicKey);
            byte[] receiverSecret = receiver.DeriveKeyMaterial(senderPub.PublicKey);

            // Both secrets should match
            Console.WriteLine(Convert.ToBase64String(senderSecret) ==
                              Convert.ToBase64String(receiverSecret));

            // ===== Use shared secret as AES key =====
            byte[] aesKey = SHA256.HashData(senderSecret);

            byte[] encrypted = EncryptAES(message, aesKey);
            string text = Convert.ToBase64String(encrypted);
            return [text, Convert.ToBase64String(aesKey)];
        }

         private static byte[] EncryptAES(string data, byte[] key)
        {
            using Aes aes = Aes.Create();
            aes.Key = key;
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor();

            byte[] plaintext = Encoding.UTF8.GetBytes(data);
            byte[] cipher = encryptor.TransformFinalBlock(plaintext, 0, plaintext.Length);

            // prepend IV
            byte[] result = new byte[aes.IV.Length + cipher.Length];
            Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
            Buffer.BlockCopy(cipher, 0, result, aes.IV.Length, cipher.Length);

            return result;
        }

        [HttpGet("Decrypt")]
        public string Decrypt(string encrypted,string key)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encrypted);
            byte[] keyBytes = Convert.FromBase64String(key);

            string text = DecryptAES(encryptedBytes, keyBytes);
            return text;
        }

        [HttpGet("GetKey")]
        public string GetKey(string key)
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
        }
        private static string DecryptAES(byte[] data, byte[] key)
        {
            using Aes aes = Aes.Create();
            aes.Key = key;

            byte[] iv = new byte[16];
            byte[] cipher = new byte[data.Length - 16];

            Buffer.BlockCopy(data, 0, iv, 0, 16);
            Buffer.BlockCopy(data, 16, cipher, 0, cipher.Length);

            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();
            byte[] plain = decryptor.TransformFinalBlock(cipher, 0, cipher.Length);

            return Encoding.UTF8.GetString(plain);
        }

        [HttpGet("Encryptv2")]
        public string Encryptv2(string tenantId, string plainText)
        {
            var key = Convert.FromBase64String(GetKey(tenantId));

            var nonce = RandomNumberGenerator.GetBytes(12);
            var plaintextBytes = Encoding.UTF8.GetBytes(plainText);

            var cipher = new byte[plaintextBytes.Length];
            var tag = new byte[16];

            using var aes = new AesGcm(key,16);
            aes.Encrypt(nonce, plaintextBytes, cipher, tag);

            var result = new byte[nonce.Length + tag.Length + cipher.Length];
            Buffer.BlockCopy(nonce, 0, result, 0, nonce.Length);
            Buffer.BlockCopy(tag, 0, result, nonce.Length, tag.Length);
            Buffer.BlockCopy(cipher, 0, result, nonce.Length + tag.Length, cipher.Length);

            var text = Convert.ToBase64String(result);

            return text;
        }

        [HttpGet("Decryptv2")]
        public string Decryptv2(string tenantId, string cipherText)
        {
            //var key = Convert.FromBase64String(GetKey(tenantId));
            var key = Convert.FromBase64String(tenantId); // Example key, replace with actual key retrieval logic

            var data = Convert.FromBase64String(cipherText);

            var nonce = data[..12];
            var tag = data[12..28];
            var cipher = data[28..];

            var plaintext = new byte[cipher.Length];

            using var aes = new AesGcm(key,16);
            aes.Decrypt(nonce, cipher, tag, plaintext);
            var text = Encoding.UTF8.GetString(plaintext);

            return text;
        }

        // GET: api/<AccountController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {  
            return "value";
        }

        // POST api/<AccountController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
