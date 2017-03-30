using System;

namespace Multithreading
{
    public class ConsolePrinter : IStatisticPrinter
    {
        public void Print(Statistic statistic)
        {
            Console.WriteLine($"Time: {statistic.CalculationTime}ms");
            foreach (var item in statistic.Items)
            {
                Console.WriteLine($"Symbol: {item.Key}, count: {item.Value}, percentage: {(float)item.Value / statistic.TotalItems * 100:F2}%");
            }
        }
    }
}
