using System;

namespace HIEDDAZ.Data
{
    public class Plant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime WateredOn { get; set; }
        public int WaterFrequencyInHours { get; set; }
    }
}
