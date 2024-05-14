using System.Security.Cryptography;
using System;
using System.Text;

public static class Encryption {    
    private static string hash = "JunkHash@foo"; // Change Here;
    public static string Encrypt(string input) {
        byte[] data = UTF8Encoding.UTF8.GetBytes(input);
        using(MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider()) 
        {
            byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            using(TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider(){Key = key,Mode = CipherMode.ECB,Padding = PaddingMode.PKCS7}) 
            {
                ICryptoTransform tr = trip.CreateEncryptor();
                byte[] results = tr.TransformFinalBlock(data,0,data.Length);
                return Convert.ToBase64String(results);
            }
        }
    }
    public static string Decrypt(string input) {
        byte[] data = Convert.FromBase64String(input);
        using(MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider()) 
        {
            byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            using(TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider() {Key = key,Mode = CipherMode.ECB,Padding = PaddingMode.PKCS7}) 
            {
                ICryptoTransform tr = trip.CreateDecryptor();
                byte[] results = tr.TransformFinalBlock(data,0,data.Length);
                return UTF8Encoding.UTF8.GetString(results);
            }
        }
    }
}
