namespace Interfaces {
    public interface ICrypt {
        public string encrypt(string password);
        public bool compare(string password, string bdPassword);
    }
}