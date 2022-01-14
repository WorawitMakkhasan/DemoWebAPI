using System.Text.Json.Serialization;

namespace WebApiDemo.Model
{
    public class Location
    {
        public int Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public int Postcode { get; set; } = 0;
        [JsonIgnore]
        public Student Student { get; set; }

        public int StudentId { get; set; }
    }
}
