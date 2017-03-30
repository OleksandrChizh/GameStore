namespace Multithreading
{
    public class Program
    {
        private const string TextFileName = @"D:\text.txt";

        public static void Main(string[] args)
        {
            var statisticCalculator = new StatisticCalculator(TextFileName);
            IStatisticPrinter printer = new ConsolePrinter();

            Statistic statistic = statisticCalculator.CalculateUsingSingleThread();
            printer.Print(statistic);

            statistic = statisticCalculator.CalculateUsingAllThreads();
            printer.Print(statistic);
        }
    }
}
