using System;

namespace PlutoRover.Models.Extensions
{
    public static class EnumExtensions
    {
        public static T Next<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException($"Argument {typeof(T).FullName} is not an Enum");

            var arr = (T[])Enum.GetValues(src.GetType());
            var j = Array.IndexOf<T>(arr, src) + 1;
            return (j == arr.Length) ? arr[0] : arr[j];
        }

        public static T Previous<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException($"Argument {typeof(T).FullName} is not an Enum");

            var arr = (T[])Enum.GetValues(src.GetType());
            var j = Array.IndexOf<T>(arr, src) - 1;
            return (j == -1) ? arr[arr.Length - 1] : arr[j];
        }
    }
}
