using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Extensions
{
    public static class LongExtensions
    {
        public static string FormatNumber(long num)
        {
            // Ensure number has max 3 significant digits (no rounding up can happen)
            if (num > 0)
            {
                long i = (long)Math.Pow(10, (int)Math.Max(0, Math.Log10(num) - 2));
                num = num / i * i;


                if (num >= 1000000000000000000)
                    return (num / 1000000000000000000D).ToString("0.##") + "Qi";
                if (num >= 1000000000000000)
                    return (num / 1000000000000000D).ToString("0.##") + "Qa";
                if (num >= 1000000000000)
                    return (num / 1000000000000D).ToString("0.##") + "T";
                if (num >= 1000000000)
                    return (num / 1000000000D).ToString("0.##") + "B";
                if (num >= 1000000)
                    return (num / 1000000D).ToString("0.##") + "M";
                if (num >= 1000)
                    return (num / 1000D).ToString("0.##") + "K";
            }

            return num.ToString("#,0");
        }
    }
}
