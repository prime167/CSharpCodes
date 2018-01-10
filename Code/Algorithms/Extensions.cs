using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Algorithms
{
    public static class Extensions
    {
        public static void ForAll<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach (T item in sequence)
            {
                action(item);
            }
        }

        /// <summary>
        /// 连接所有元素，用,分割 (1,2,3,4,5,6)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Concat<T>(this IEnumerable<T> s)
        {
            string str = string.Empty;
            foreach (T item in s)
            {
                str += item;
                str += ",";
            }

            str = str.Remove(str.Length - 1);
            return str;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadLocalRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        #region Char Extention
        public static bool IsDigit(this char c)
        {
            return char.IsDigit(c);
        }

        public static bool IsLetter(this char c)
        {
            return char.IsLetter(c);
        }

        public static bool IsSpace(this char c)
        {
            return char.IsWhiteSpace(c);
        }

        public static bool IsLetterOrDigit(this char c)
        {
            return char.IsLetterOrDigit(c);
        }

        public static bool IsLower(this char c)
        {
            return char.IsLower(c);
        }

        public static bool IsUpper(this char c)
        {
            return char.IsUpper(c);
        }

        public static bool IsSpecialChar(this char c)
        {
            return char.IsSymbol(c) || char.IsPunctuation(c);
        }

        #endregion

        #region String Extention
        public static int ToInt32(this string s)
        {
            return IsAllDigit(s) ? Convert.ToInt32(s) : int.MinValue;
        }

        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool IsBeginWithDigital(this string s)
        {
            return s[0].IsDigit();
        }

        public static bool IsContainUpperLetter(this string s)
        {
            return s.Any(t => t.IsUpper());
        }

        public static bool IsContainLowerLetter(this string s)
        {
            return s.Any(t => t.IsLower());
        }

        public static bool IsContainSpecialLetter(this string s)
        {
            return s.Any(t => t.IsSpecialChar());
        }

        public static bool IsContainDigit(this string s)
        {
            return s.Any(t => t.IsDigit());
        }

        public static bool IsAllUpperLetter(this string s)
        {
            return s.All(t => t.IsUpper());
        }

        public static bool IsAllLowerLetter(this string s)
        {
            return s.All(t => t.IsLower());
        }

        public static bool IsAllSpecialLetter(this string s)
        {
            return s.All(t => t.IsSpecialChar());
        }

        public static bool IsAllDigit(this string s)
        {
            //var r = new Regex(@"\d+");
            //return r.IsMatch(s);
            return s.All(c => c.IsDigit());
        }

        public static bool IsNotContainUpperLetter(this string s)
        {
            return s.All(t => !t.IsUpper());
        }

        public static bool IsNotContainLowerLetter(this string s)
        {
            return s.All(t => !t.IsLower());
        }

        public static bool IsNotContainSpecialLetter(this string s)
        {
            return s.All(t => !t.IsSpecialChar());
        }

        public static bool IsNotContainDigit(this string s)
        {
            return s.All(t => !t.IsDigit());
        }

        public static bool IsValidPassword(this string s)
        {
            var goodCondition = 0;
            if (s.Length < 6)
            {
                return false;
            }

            if (s.IsContainSpecialLetter())
            {
                goodCondition++;
            }
            if (s.IsContainDigit())
            {
                goodCondition++;
            }
            if (s.IsContainLowerLetter())
            {
                goodCondition++;
            }
            if (s.IsContainUpperLetter())
            {
                goodCondition++;
            }
            if (goodCondition >= 3)
            {
                return true;
            }

            return false;
        }

        public static bool IsValidEmail(this string s)
        {
            var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

        public static bool IsValidGUID(this string s)
        {
            var regex = new Regex(@"(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}");
            return regex.IsMatch(s);
        }

        public static int GetDitigalCount(this string s)
        {
            return s.Count(t => t.IsDigit());
        }

        public static string GetMd5Hash(this string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            var md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        /// <summary>
        /// Convert "hello world" to "Hello World"
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToTitleCase(this string s)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(s);
        }
        #endregion

        #region  Int Extention
        public static bool IsPrime(this int integer)
        {
            if (integer == 1)
            {
                return false;
            }
            for (var i = 2; i <= Math.Sqrt(integer); i++)
            {
                if (integer % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static int GetSumOfProperDivisors(this int integer)
        {
            var sum = 0;
            if (integer == 1)
            {
                return 0;
            }
            for (var i = 1; i <= Math.Sqrt(integer); i++)
            {
                if (integer % i != 0) continue;
                var another = integer / i;
                sum += i;
                if (another != integer && another != i)
                {
                    sum += another;
                }
            }
            return sum;
        }

        public static bool IsPerfectNumber(this int integer)
        {
            return integer.GetSumOfProperDivisors() == integer;
        }

        #endregion

        #region Collection Extention
        public static bool In(this object o, IEnumerable c)
        {
            return c.Cast<object>().Contains(o);
        }

        #endregion

        #region Enum Extention
        public static string GetEnumDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            if (fi == null)
            {
                return "error";
            }
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }
        #endregion

        public static int ToInt32(this decimal d)
        {
            // Convert the argument to an int value.
            return (int)d;
        }
        # region decimal Extention

        # endregion

        # region DateTime Extention
        public static string ToTimeStamp(this DateTime d)
        {
            return d.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }
        # endregion
    }

    public static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }

    /// <summary> 
    /// Convenience class for dealing with randomness. 
    /// </summary> 
    public static class ThreadLocalRandom
    {
        /// <summary> 
        /// Random number generator used to generate seeds, 
        /// which are then used to create new random number 
        /// generators on a per-thread basis. 
        /// </summary> 
        private static readonly Random GlobalRandom = new Random();
        private static readonly object GlobalLock = new object();

        /// <summary> 
        /// Random number generator 
        /// </summary> 
        private static readonly ThreadLocal<Random> ThreadRandom = new ThreadLocal<Random>( () => new Random (Guid.NewGuid().GetHashCode()) );

        /// <summary> 
        /// Creates a new instance of Random. The seed is derived 
        /// from a global (static) instance of Random, rather 
        /// than time. 
        /// </summary> 
        public static Random NewRandom()
        {
            lock (GlobalLock)
            {
                return new Random(GlobalRandom.Next());
            }
        }

        /// <summary> 
        /// Returns an instance of Random which can be used freely 
        /// within the current thread. 
        /// </summary> 
        public static Random Instance => ThreadRandom.Value;

        /// <summary>See <see cref="Random.Next()" /></summary> 
        public static int Next()
        {
            return Instance.Next();
        }

        /// <summary>See <see cref="Random.Next(int)" /></summary> 
        public static int Next(int maxValue)
        {
            return Instance.Next(maxValue);
        }

        /// <summary>See <see cref="Random.Next(int, int)" /></summary> 
        public static int Next(int minValue, int maxValue)
        {
            return Instance.Next(minValue, maxValue);
        }

        /// <summary>See <see cref="Random.NextDouble()" /></summary> 
        public static double NextDouble()
        {
            return Instance.NextDouble();
        }

        /// <summary>See <see cref="Random.NextBytes(byte[])" /></summary> 
        public static void NextBytes(byte[] buffer)
        {
            Instance.NextBytes(buffer);
        }
    } 
}
