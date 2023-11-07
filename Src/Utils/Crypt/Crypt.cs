using CryptSharp;
using Interfaces;

namespace Utils {
    public class Crypt : ICrypt {
        public string encrypt(string password) {
            return Crypter.Sha256.Crypt(password);
        }
        public bool compare(string password, string bdPassword) {
            return Crypter.CheckPassword(password, bdPassword);
        }
    }
}