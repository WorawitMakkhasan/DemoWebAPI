using System.Text.Json.Serialization;

namespace WebApiDemo.Model
{
    public class Inventory
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        [JsonIgnore]
        public Student Student { get; set; }

        public int StudentId { get; set; }
    }
}
