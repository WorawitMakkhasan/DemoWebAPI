using System.Text.Json.Serialization;

namespace WebApiDemo.Model
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Username { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        [JsonIgnore]
        public Location? Location { get; set; } = null;

        [JsonIgnore]
        public List<Inventory>? Invertory { get; set; } = null;

        [JsonIgnore]
        public List<Course>? Courses { get; set; } = null;

    }
}
