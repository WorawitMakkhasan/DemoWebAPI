namespace WebApiDemo.Model.Dto
{
    public class LocationDto
    {
        public string Address { get; set; } = string.Empty;
        public int Postcode { get; set; } = 0;

        public int StudentId { get; set; }
    }
}
