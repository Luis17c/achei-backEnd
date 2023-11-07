namespace Interfaces {
    public interface IStorage {
        public string upload(IFormFile file);
        public string getUrl(string ?fileName);
        public bool delete(string path);
    }
}