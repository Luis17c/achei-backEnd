using System.ComponentModel.DataAnnotations.Schema;

namespace Models {
    [Table("companies")]
    public class Company : ActiveBaseEntity {
        public string name { get; set; }
        public string ?description { get; set; }
        public string ?logo { get; set; }
        public int ?rating { get; set; }
        public string[] ?categories { get; set; }
        public ICollection<Address> ?addresses { get; set; }
        public User user { get; set; }
        public Company (string name, User user, string ?description, string[] ?categories) {
            this.name = name;
            this.description = description;
            this.user = user;
            this.categories = categories;
        }
    }
}