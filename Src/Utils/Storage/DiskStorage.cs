using Interfaces;

namespace Utils {
    public class DiskStorage : IStorage {
        private readonly string path = Environment.CurrentDirectory + "\\Tmp\\"; 
        private readonly ICrypt _crypt;
        public DiskStorage (ICrypt crypt) {
            _crypt = crypt;
        }
        public string upload(IFormFile file) {
            string newFileName = _crypt.encrypt(file.FileName) + "_" + file.FileName;

            while (newFileName.Contains("/")) {
                newFileName = _crypt.encrypt(file.FileName) + "_" + file.FileName;
            }

            if (! Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }

            using var stream = File.Create(path + newFileName);
            file.CopyToAsync(stream);

            return newFileName;
        }
        public string getUrl(string ?fileName) {
            if (fileName == null) {
                return path + "default";
            }
            return path + fileName;
        }
        public bool delete(string path) {
            return true;
        }
    }
}