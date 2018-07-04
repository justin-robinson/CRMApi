using System;
using System.Security.Cryptography;

namespace CRMApi.Utils {
    public class Crypto {
        public static string GenerateRandomCryptographicKey(int keyLength) {
            var randomBytes = new byte[keyLength];
            new RNGCryptoServiceProvider().GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public static string Hash(string plaintext) {
            using (var md5Hash = MD5.Create()) {
                return MD5Helper.GetMd5Hash(md5Hash, plaintext);
            }
        }
    }
}