using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ServiceLayer.DTO;
using ServiceLayer.DTO.Registration;

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

        public static bool Compare(this RegistrationDTO regDTO, RegisterFormDTO regFormDTO)
            => regDTO.Email == regFormDTO.Email
                && regDTO.Privilege == regFormDTO.Privilege
                && regDTO.FirstName == regFormDTO.FirstName
                && regDTO.LastName == regFormDTO.LastName
                && regDTO.Index == regFormDTO.Index;

    }
}
