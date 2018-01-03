using System;
using System.Collections.Generic;

namespace Algorithms
{
    public static class Sort
    {
        /// <summary>
        /// 冒泡排序
        /// 总共需循环 (1+2+3+...+count-1) = (count)(count-1)/2 次, 既时间复杂度是O(n^2)
        /// </summary>
        /// <param name="list"></param>
        public static List<T> BubbleSort<T>(List<T> list) where T : IComparable
        {
            var len = list.Count;
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len - i - 1; j++) // 最后 i 个已经排好序，不需要再比较
                {
                    if (Gt(list[j], list[j + 1]))
                    {
                        var temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 选择排序，每次在剩余的集合里找到最小(大)的，再交换
        /// 最坏总共需循环 (1+2+3+...+count-1) = (count)(count-1)/2 次, 既时间复杂度是O(n^2)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static List<T> SellectionSort<T>(List<T> list) where T : IComparable
        {
            var len = list.Count;
            for (int i = 0; i < len; i++)
            {
                int max = i; // 每次都默认第一个是最小的
                for (int j = i + 1; j < len; j++)
                {
                    if (Gt(list[max], list[j]))
                    {
                        max = j;
                    }
                }

                if (Ne(list[max],list[i])) // 已排好，不需要交换
                {
                    var temp = list[max];
                    list[max] = list[i];
                    list[i] = temp;
                }
            }

            return list;
        }

        /// <summary>
        /// 插入排序
        /// 最坏总共需循环 (1+2+3+...+count-1) = (count)(count-1)/2 次, 既时间复杂度是O(n^2)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static List<T> InsertionSort<T>(List<T> list) where T : IComparable
        {
            for (int i = 1; i < list.Count; i++)
            {
                // 默认第一个是有序的
                var temp = list[i]; // 拿出要插入的数据
                int j = i;

                // 寻找插入位置, 元素后移
                while (j > 0 && Lt(temp, list[j -1]))
                {
                    list[j] = list[j - 1];
                    j--;
                }

                list[j] = temp;
            }

            return list;
        }

        /// <summary>
        /// Shell 排序
        /// 最坏总共需循环 (1+2+3+...+count-1) = (count)(count-1)/2 次, 既时间复杂度是O(n^2)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static List<T> ShellSort<T>(this List<T> list) where T : IComparable
        {
            var len = list.Count;
            for (int step = len / 2; step > 0; step /= 2)
            {
                Console.WriteLine("当前步长：{0}", step);
                for (int i = step; i < len; i++)
                {
                    var temp = list[i];
                    int j = i;
                    while (j >= step && Lt(temp, list[j - step]))
                    {
                        list[j] = list[j - step];
                        j -= step;
                    }

                    list[j] = temp;
                }
            }

            return list;
        }

        /// <summary>
        /// 归并排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> MergeSort<T>(List<T> list) where T : IComparable
        {
            if (list.Count <= 1) return list;

            List<T> left = list.GetRange(0, list.Count / 2);
            List<T> right = list.GetRange(left.Count, list.Count - left.Count);
            return Merge(MergeSort(left), MergeSort(right));
        }

        public static List<T> Merge<T>(List<T> left, List<T> right) where T : IComparable
        {
            List<T> result = new List<T>();
            while (left.Count > 0 && right.Count > 0)
            {
                if (Le(left[0], right[0])) 
                {
                    result.Add(left[0]);
                    left.RemoveAt(0);
                }
                else
                {
                    result.Add(right[0]);
                    right.RemoveAt(0);
                }
            }
            result.AddRange(left);
            result.AddRange(right);
            return result;
        }

        /// <summary>
        /// 快速排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> QuickSort<T>(List<T> list) where T : IComparable
        {
            QuickSort_Recursive(list, 0, list.Count - 1);
            return list;
        }

        static public void QuickSort_Recursive<T>(List<T> arr, int left, int right) where T : IComparable
        {
            // For Recusrion
            if (left < right)
            {
                int pivot = Partition(arr, left, right);

                if (pivot > 1)
                    QuickSort_Recursive(arr, left, pivot - 1);

                if (pivot + 1 < right)
                    QuickSort_Recursive(arr, pivot + 1, right);
            }
        }

        static public int Partition<T>(List<T> numbers, int left, int right) where T : IComparable
        {
            T pivot = numbers[left];
            while (true)
            {
                while (Lt(numbers[left],pivot))
                    left++;

                while (Gt(numbers[right], pivot))
                    right--;

                if (left < right)
                {
                    T temp = numbers[right];
                    numbers[right] = numbers[left];
                    numbers[left] = temp;
                }
                else
                {
                    return right;
                }
            }
        }

        /// <summary>
        /// less than
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Lt<T>(T a, T b) where T : IComparable
        {
            return a.CompareTo(b) < 0;
        }

        /// <summary>
        /// less than or equal to
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Le<T>(T a, T b) where T : IComparable
        {
            return a.CompareTo(b) <= 0;
        }

        /// <summary>
        /// greater than
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Gt<T>(T a, T b) where T : IComparable
        {
            return a.CompareTo(b) > 0;
        }

        /// <summary>
        /// greater than or equal to
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Ge<T>(T a, T b) where T : IComparable
        {
            return a.CompareTo(b) >= 0;
        }

        /// <summary>
        /// not equal
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Ne<T>(T a, T b) where T : IComparable
        {
            return a.CompareTo(b) != 0;
        }

        public static bool Eq<T>(T a, T b) where T : IComparable
        {
            return a.CompareTo(b) == 0;
        }
    }
}
