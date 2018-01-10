using System;
using System.Collections.Generic;

namespace Algorithms
{
    public static class Other
    {
        /// <summary>
        /// 从两个已排序数组中找到相同的元素，等同于 a.Intersect(b)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void FindSame(int[] a, int[] b)
        {
            int i = 0, j = 0;

            while (i < a.Length && j < b.Length)
            {
                if (a[i] > b[j])
                {
                    j++;
                }
                else if (a[i] < b[j])
                {
                    i++;
                }
                else
                {
                    Console.WriteLine(a[i]);
                    i++;
                    j++;
                }
            }
        }

        /// <summary>
        /// 判断两个已排序数组是否含有相同的元素
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool HasSame(int[] a, int[] b)
        {
            int i = 0;
            int j = 0;
            while (i < a.Length && j < b.Length)
            {
                if (a[i] == b[j])
                {
                    return true;
                }
                if (a[i] < b[j])
                {
                    i++;
                }
                if (a[i] > b[j])
                {
                    j++;
                }
            }
            return false;
        }

        public static string ReverseSentenceByWord(string s)
        {
            int last = s.Length - 1;
            char[] reversed = new char[s.Length];
            for (int k = 0; k < s.Length / 2; k++)
            {
                char temp = s[k];
                reversed[k] = s[last - k];
                reversed[last - k] = temp;
            }

            int nextWordStart = 0;
            int len = s.Length;
            while (nextWordStart < len)
            {
                var wordStart = nextWordStart;
                var wordEnd = nextWordStart;
                while (reversed[wordEnd] != ' ')
                {
                    wordEnd++;
                    if (wordEnd == len)
                    {
                        break;
                    }
                }
                nextWordStart = wordEnd + 1;
                wordEnd--;
                for (; wordStart < wordEnd; wordStart++, wordEnd--)
                {
                    char temp = reversed[wordStart];
                    reversed[wordStart] = reversed[wordEnd];
                    reversed[wordEnd] = temp;
                }
            }

            return new string(reversed);
        }

        /// <summary>
        /// 最大子序列，动态规划 O(n)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public static decimal FindBestSubsequence(this IEnumerable<decimal> source, out int startIndex, out int endIndex)
        {
            decimal result = decimal.MinValue;
            decimal sum = 0;
            int tempStart = 0;

            var tempList = new List<decimal>(source);

            startIndex = 0;
            endIndex = 0;

            for (int index = 0; index < tempList.Count; index++)
            {
                sum += tempList[index];
                if (sum > result)
                {
                    result = sum;
                    startIndex = tempStart;
                    endIndex = index;
                }
                if (sum < 0)
                {
                    sum = 0;
                    tempStart = index + 1;
                }
            }

            return result;
        }
    }
}
