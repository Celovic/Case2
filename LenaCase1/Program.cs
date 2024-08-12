namespace LenaCase1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hoşgeldiniz");
            Console.WriteLine();
            Console.WriteLine("Lütfen Etkinlik bilgilerini giriniz");
            string proc = "";

            var events = new List<Event>();
            var durations = new List<LocationDuration>();
            int id = 1;

            #region Event Info

            do
            {
                var eventModel = new Event();

                TimeSpan startTime;
                TimeSpan endTime;

                Console.WriteLine("Lütfen etkinlik başlangıç saati giriniz. Örnek: 10:30");
                TimeSpan.TryParse(Console.ReadLine(), out startTime);
                eventModel.StartTime = startTime;

                Console.WriteLine();

                Console.WriteLine("Lütfen etkinlik bitiş saati giriniz. Örnek: 12:30");

                TimeSpan.TryParse(Console.ReadLine(), out endTime);
                eventModel.EndTime = endTime;

                Console.WriteLine();

                Console.WriteLine("Lütfen lokasyon giriniz . Örnek: Bakırköy");
                eventModel.Location = Console.ReadLine();

                Console.WriteLine();

                Console.WriteLine("Lütfen önem derecesini giriniz. Örnek: 70");
                eventModel.Priority = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine();

                eventModel.Id += id;

                events.Add(eventModel);
                id++;
                Console.WriteLine("Etkinlik başarıyla eklendi");

                Console.WriteLine();

                Console.WriteLine("Yeni bir etkinlik eklemek istiyor musunuz? y/n");
                proc = Console.ReadLine();

                Console.WriteLine();

            } while (proc.ToLower() == "y");

            #endregion

            #region LocationDuration Info

            var location = events.Select(x => x.Location).Distinct().ToList();

            for (int i = 0; i < location.Count; i++)
            {
                for (int j = i + 1; j < location.Count; j++)
                {
                    var duration = new LocationDuration();
                    var reverseLocation = new LocationDuration();

                    Console.WriteLine("Lütfen " + location[i] + " lokasyonundan " + location[j] + " lokasyonuna varış süresini giriniz");

                    duration.DurationMinutes = reverseLocation.DurationMinutes = Convert.ToInt32(Console.ReadLine());
                    duration.From = reverseLocation.To = location[i];
                    duration.To = reverseLocation.From = location[j];

                    durations.Add(duration);
                    durations.Add(reverseLocation);

                    Console.WriteLine();
                }
            }
            #endregion

            Console.WriteLine("Kayıtlar başarıyla kaydedildi");

            Console.WriteLine();

            var scheduler = new EventScheduler(events, durations);
            var result = scheduler.ScheduleEvents();

            Console.WriteLine($"Katılınabilecek Maksimum Etkinlik Sayısı: {result.eventIds.Count}");
            Console.WriteLine($"Katılınabilecek Etkinliklerin ID'leri: {string.Join(", ", result.eventIds)}");
            Console.WriteLine($"Toplam Değer: {result.totalValue}");

        }
    }
}