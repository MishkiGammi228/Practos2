using System;
using System.Data;

namespace Lib_4
{
    public static class Prectic
    {
        public static string RootNumber(int[] array)
        {
            double root = double.MinValue;
            string nambers = string.Empty;
            for (int i = 0; i < array.Length; i++)
            {
                root = Math.Sqrt(array[i]);
                nambers += " " + root;
            }
            return nambers;
        }
    }
}
