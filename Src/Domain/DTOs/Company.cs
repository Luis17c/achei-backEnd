namespace DTOs {
    public class CreateCompanyDTO {
        public required string name { get; set; }
        public string ?description { get; set; }
        public string[] ?categories { get; set; }
    }
}