using Interfaces;

namespace Utils {
    public class AwsStorage : IStorage {
        public string upload(IFormFile file) {
            return "";
        }
        public string getUrl(string ?fileName) {
            return "";
        }
        public bool delete(string path) {
            return true;
        }
    }
}