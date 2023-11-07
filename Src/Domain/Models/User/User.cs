using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models {
    [Table("users")]
    public class User : BaseEntity {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string ?photo { get; set;}
        [JsonIgnore]
        public string ?password { get; set; }
        
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