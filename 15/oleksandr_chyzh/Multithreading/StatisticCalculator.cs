using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading
{
    public class StatisticCalculator
    {
        private readonly string _fileName;

        public StatisticCalculator(string fileName)
        {
            _fileName = fileName;
        }

        public Statistic CalculateUsingSingleThread()
        {
            return Calculate((statistic, symbols, uniqueSymbols) =>
            {
                foreach (var symbol in uniqueSymbols)
                {
                    statistic[symbol] += symbols.Count(s => s == symbol);
                }
            });
        }

        public Statistic CalculateUsingAllThreads()
        {
            return Calculate((statistic, symbols, uniqueSymbols) =>
            {
                Parallel.ForEach(
                    uniqueSymbols, 
                    symbol =>
                {
                    statistic[symbol] += symbols.Count(s => s == symbol);
                });
            });
        }

        private Statistic Calculate(Action<Dictionary<char, int>, char[], char[]> forAction)
        {
            char[] symbols = GetSymbols();
            var statistic = new Dictionary<char, int>();
            var uniqueSymbols = symbols.Distinct().ToArray();
            foreach (var symbol in uniqueSymbols)
            {
                statistic[symbol] = 0;
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            forAction(statistic, symbols, uniqueSymbols);
            stopwatch.Stop();

            return new Statistic
            {
                Items = statistic,
                CalculationTime = stopwatch.ElapsedMilliseconds,
                TotalItems = symbols.Length
            };
        }

        private char[] GetSymbols()
        {
            string text;
            using (var sr = new StreamReader(_fileName, Encoding.Default))
            {
                text = sr.ReadToEnd();
            }

            return text.Replace("\r\n", string.Empty).ToCharArray();
        }
    }
}
