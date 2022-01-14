namespace WebApiDemo.Model
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public Location Location { get; set; }

        public List<Inventory> Invertory { get; set; }

        public List<Course> Courses { get; set; }
    }
}
