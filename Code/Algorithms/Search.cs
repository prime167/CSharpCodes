using System;
using System.Collections.Generic;

namespace Algorithms
{
    public class Search
    {
        /// <summary>
        /// 二分查找
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="find"></param>
        /// <returns></returns>
        public static int BinarySearch<T>(List<T> list, T find) where T : IComparable
        {
            int low = 0, high = list.Count - 1;
            while (low <= high)
            {
                int mid = (low + high) / 2, cmp = list[mid].CompareTo(find);
                if (cmp < 0) { low = mid + 1; }
                else if (cmp > 0) { high = mid - 1; }
                else { return mid; }
            }

            return -(low + 1);
        }

        public static int binary_search_first_position(int[] A, int n, int target)
        {
            int low = -1, high = n;
            while (low + 1 < high)
            {
                int mid = (low + high) >> 1;
                if (A[mid] < target)
                    low = mid;
                else
                    high = mid;
            }

            if (high >= n || A[high] != target)
                return -high - 1;

            return high;  // high == low + 1
        }

        public static int binary_search_last_position(int[] A, int n, int target)
        {
            int low = -1, high = n;
            while (low + 1 < high)
            {
                int mid = (low + high) >> 1;
                if (A[mid] > target)
                    high = mid;
                else
                    low = mid;
            }

            if (low < 0 || A[low] != target)
                return -low - 2;

            return low;  // low == high - 1
        }

        public static int binary_search_first_position1(int[] A, int n, int target)
        {
            int[] end = { -1, n };
            while (end[0] + 1 < end[1])
            {
                int mid = (end[0] + end[1]) / 2;
                uint sign = (uint)(A[mid] - target) >> 31;
                end[1 - sign] = mid;
            }
            int high = end[1];
            if (high >= n || A[high] != target)
                return -high - 1;

            return high;
        }

        public static int binary_search_last_position1(int[] A, int n, int target)
        {
            int[] end = { -1, n };
            while (end[0] + 1 < end[1])
            {
                int mid = (end[0] + end[1]) / 2;
                int sign = (target - A[mid]) >> 31;
                end[sign] = mid;
            }
            int low = end[0];
            if (low < 0 || A[low] != target)
                return -low - 2;

            return low;
        }

        /// <summary>
        /// 一趟扫描，找到第二大
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T FindSecond<T>(List<T> list) where T : IComparable
        {
            T biggest = default(T);
            T second = default(T);
            foreach (var i in list)
            {
                if (i.CompareTo(second) > 0)
                {
                    second = i;
                    if (second.CompareTo(biggest) > 0)
                    {
                        var temp = second;
                        second = biggest;
                        biggest = temp;
                    }
                }
            }

            return second;
        }
    }
}
