using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models {
    [Table("addresses")]
    public class Address : ActiveBaseEntity {
        public string city { get; set; }
        public string ?street { get; set; }
        public string ?number { get; set; }
        public string ?complement { get; set; }
        public int ?userId { get; set; }
        public int ?companyId { get; set; }
        public Address (string city, string ?street, string ?number, string ?complement) {
            this.city = city;
            this.street = street;
            this.number = number;
            this.complement = complement;
        }
    }
}