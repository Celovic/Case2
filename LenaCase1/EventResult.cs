namespace LenaCase1
{
    public class EventResult
    {
        public int TotalValue { get; set; }
        public List<Event> Events { get; set; }

        public EventResult()
        {
            Events = new List<Event>();
        }
    }
}
