namespace Models.Classes
{
    public class Lecture(uint duration, string description)
    {
        public uint Duration { get; set; } = duration;
        public string Description { get; set; } = description;
        public TimeSpan StartTime { get; set; } = TimeSpan.Zero;

        public override string ToString()
        {
            return $"{StartTime.Hours:00}:{StartTime.Minutes:00}H {Description} {Duration:00}min";
        }
    }
}
