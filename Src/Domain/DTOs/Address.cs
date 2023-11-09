namespace DTOs {
    public class CreateAddressDTO {
        public required string city { get; set; }
        public string ?street { get; set; }
        public string ?number { get; set; }
        public string ?complement { get; set; }
    }
}