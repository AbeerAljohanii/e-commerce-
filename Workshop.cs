namespace e_commerce_
{
    public class Workshop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal HourlyRate { get; set; }
        public Boolean Availability { get; set; }
    }
}