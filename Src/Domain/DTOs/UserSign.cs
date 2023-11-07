using Models;

namespace DTOs {
    public class UserSignUpDTO {
        public required string name { get; set; }
        public required string email { get; set; }
        public required string password { get; set; }
        }

     public class UserSignInDTO {
        public required string email { get; set; }
        public required string password { get; set; }
    }

    public class SignResponse {
        public User user { get; set; }
        public string token { get; set; }
        public SignResponse (User user, string token) {
            this.user = user;
            this.token = token;
        }
    }
}