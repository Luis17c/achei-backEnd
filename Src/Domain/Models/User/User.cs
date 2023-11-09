using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Models {
    [Table("users")]
    public class User : ActiveBaseEntity {
        public string name { get; set; }
        public string email { get; set; }
        public string ?photo { get; set;}
        [JsonIgnore]
        public string ?password { get; set; }
        public Address ?address { get; set; }
        public ICollection<Company> ?companies { get; } = new List<Company>();       
        public User(string name, string email, string ?password, string? photo) {
            this.name = name;
            this.email = email;
            if (password != null)
                this.password = password;
            if (photo != null)
                this.photo = photo;
        }
    }
}