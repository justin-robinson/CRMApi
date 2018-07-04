using System;
using System.Security.Cryptography;
using System.Text;

namespace CRMApi.Utils {
    public class MD5Helper {
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            foreach (var d in data) {
                sBuilder.Append(d.ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        public static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            return StringComparer.OrdinalIgnoreCase
                       .Compare(GetMd5Hash(md5Hash, input), hash) == 0;
        }
    }
}