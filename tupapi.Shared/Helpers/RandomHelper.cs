using System;
using System.Text;

namespace tupapiService.Helpers
{
    public static class RandomHelper
    {
        public static string RandomString(int size)
        {
            var random = new Random((int) DateTime.Now.Ticks);
            var builder = new StringBuilder();
            for (var i = 0; i < size; i++)
            {
                var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26*random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public static string RandomNumString(int size)
        {
            var random = new Random((int) DateTime.Now.Ticks);
            var builder = new StringBuilder();
            for (var i = 0; i < size; i++)
            {
                var str = random.Next(0, 9).ToString();
                var ch = str[0];
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}