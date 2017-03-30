using System;
using System.Text;

namespace GameStore.Infrastructure.EFDataAccess.Utils
{
    public static class SaltGenerator
    {
        private static readonly Random Random = new Random((int)DateTime.Now.Ticks);

        public static string GetSalt(int size)
        {
            var builder = new StringBuilder();

            for (int i = 0; i < size; i++)
            {
                builder.Append(Convert.ToChar(Convert.ToInt32(Math.Floor((26 * Random.NextDouble()) + 65))));
            }

            return builder.ToString();
        }
    }
}