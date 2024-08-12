namespace LenaCase1
{
    public class EventScheduler
    {
        private List<Event> _events;
        private List<LocationDuration> _durations;

        public EventScheduler(List<Event> events, List<LocationDuration> durations)
        {
            _events = events;
            _durations = durations;
        }

        public (List<int> eventIds, int totalValue) ScheduleEvents()
        {
            var eventIds = new List<int>();
            var totalValue = 0;
            var ev = new Event();
            var nextLocation = string.Empty;
            var preTime = TimeSpan.Zero;

            var events = _events;
            var currentTime = TimeSpan.Zero;
            var currentLocation = string.Empty;

            FindEvents:

            if (!events.Any())
                return (eventIds, totalValue);

            events = events.OrderBy(x => x.StartTime).ToList();
            currentTime = events.Select(x => x.StartTime).FirstOrDefault();

            if (preTime >= currentTime)
            {
                events.RemoveAll(x => x.StartTime == currentTime);
                goto FindEvents;
            }

            ev = events.Where(x => x.StartTime == currentTime).OrderByDescending(x => x.Priority).First();

            if (eventIds.Count > 0 && totalValue > 0)
            {
                if (currentLocation != ev.Location)
                {
                    var checkTime = TravelTimeCalculate(currentLocation, ev.Location, preTime, currentTime);
                    if (!checkTime)
                    {
                        events.Remove(ev);
                        goto FindEvents;
                    }
                }
            }
            eventIds.Add(ev.Id);
            totalValue += ev.Priority;

            currentLocation = ev.Location;
            events.RemoveAll(x => x.StartTime == currentTime);

            currentTime = ev.EndTime;
            preTime = currentTime;

            //events = events.Where(x => x.StartTime != preTime).ToList();
            goto FindEvents;
        }

        private bool TravelTimeCalculate(string from, string to, TimeSpan preTime, TimeSpan currentTime)
        {
            var duration = _durations.FirstOrDefault(x => x.From == from && x.To == to);

            if (preTime.TotalMinutes + duration.DurationMinutes >= currentTime.TotalMinutes)
                return false;
            return true;
        }
    }
}