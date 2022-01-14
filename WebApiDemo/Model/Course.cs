using System.Text.Json.Serialization;

namespace WebApiDemo.Model
{
    public class Course
    {
        public int Id { get; set; }
        public string coursename { get; set; } = String.Empty;
        public double credit { get; set; } = 0.00;
        [JsonIgnore]
        public List<Student> student { get; set; }
    }
}
