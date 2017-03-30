using System.Collections.Generic;

namespace Multithreading
{
    public class Statistic
    {
        public Dictionary<char, int> Items { get; set; }

        public int TotalItems { get; set; }

        public long CalculationTime { get; set; }
    }
}
