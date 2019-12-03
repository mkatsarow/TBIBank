using System;
using System.Security.Cryptography;
using System.Text;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.Services.Services
{
    public class EncryptService: IEncryptService
    {
        private static byte[] key = new byte[8] { 2,3,1,58,9,5,6,120 };
        private static byte[] iv = new byte[8] { 10,42,98,51,102,127,124,71 };
        //TODO move the salt in invisible site
        private static string salt = ".ivomishotelerik";

        public string EncryptString(string text)
        {
            text += salt;
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
            byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Convert.ToBase64String(outputBuffer);
        }
        public string DecryptString(string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();  
            ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
            byte[] inputbuffer = Convert.FromBase64String(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return  Encoding.Unicode.GetString(outputBuffer).Split('.')[0];
            
        }
    }
}
