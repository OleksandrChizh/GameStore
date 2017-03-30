using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Dto.Utils
{
    public static class DictionaryExtension
    {
        public static bool IsEqual(this IDictionary<string, string> first, IDictionary<string, string> second)
        {
            if (first.Count != second.Count)
            {
                return false;
            }

            for (int i = 0; i < first.Count; i++)
            {
                string key = first.ElementAt(i).Key;

                if (!second.ContainsKey(key))
                {
                    return false;
                }

                if (first[key] != second[key])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
