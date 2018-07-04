using System;

namespace CRMApi.Utils {
    public class Password {
        private string PlainText { get; }
        private string Salt { get; }
        public string Hash() => IsValidPlainText() ? Crypto.Hash(PlainText + Salt) : "";

        public Password(string plainText, string salt) {
            PlainText = plainText;
            Salt = salt;
        }

        private bool IsValidPlainText() {
            return !String.IsNullOrEmpty(PlainText);
        }
    }
}