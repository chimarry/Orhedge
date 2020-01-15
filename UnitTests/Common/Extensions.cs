using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace UnitTests.Common
{
    public static class Extensions
    {
        public static bool IsSorted<T, TKey>(this T[] arr, Func<T, TKey> keySelector, bool desc = false)
        {
            for(int i = 0; i < arr.Length - 1; ++i)
            {
                TKey key1 = keySelector(arr[i]);
                TKey key2 = keySelector(arr[i + 1]);
                int result = Comparer<TKey>.Default.Compare(key1, key2);
                if (desc && result < 0)
                    return false;
                if(!desc && result > 0) 
                    return false;
            }
            return true;
        }
    }
}
