using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvance
{
    public class Utils
    {
        [Test(new int[] { -1, 0, 3, 5, 9, 12 }, 9, 4)]
        [Test(new int[] { -1, 0, 3, 5, 9, 12 }, 2, -1)]
        [Test(new int[] { 5 }, -5, -1)]
        [Test(new int[] { 4 }, 4, 0)]
        public static int BinarySearch(int[] arr, int target)
        {
            int left = 0;
            int right = arr.Length;

            while (left < right)
            {
                int mid = (left + right) / 2;

                if (arr[mid] == target)
                {
                    return mid;
                }
                else if (arr[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid;
                }
            }

            return -1;
        }

        public static void MergeSort(int[] nums, bool reverse = false)
        {
            if (reverse)
            {
                mergeSort(nums, 0, nums.Length, (int a, int b) => { return a > b; });
            }
            else
            {
                mergeSort(nums, 0, nums.Length, (int a, int b) => { return a < b; });
            }
        }

        private static void mergeSort(int[] nums, int start, int end, Func<int, int, bool> sortFunc)
        {
            // len = 1
            if(start == end - 1)
            {
                return;
            }

            // len = 2
            else if (start == end - 2)
            {
                end--;

                if (!sortFunc(nums[start], nums[end]))
                {
                    int temp = nums[start];
                    nums[start] = nums[end];
                    nums[end] = temp;
                }
            }
            else
            {
                int mid = (start + end) / 2;
                mergeSort(nums, start, mid, sortFunc);
                mergeSort(nums, mid, end, sortFunc);
                merge(nums, start, mid, mid, end, sortFunc);
            }
        }

        public static void merge(int[] nums, int s1, int e1, int s2, int e2, Func<int, int, bool> sortFunc)
        {
            int i = 0, start = s1, len = e2 - s1;
            int[] temp = new int[len];

            while((s1 < e1) && (s2 < e2))
            {
                Console.WriteLine($"({s1} < {e1}) && ({s2} < {e2})");

                if(sortFunc(nums[s1], nums[s2]))
                {
                    temp[i] = nums[s1];
                    s1++;
                }
                else
                {
                    temp[i] = nums[s2];
                    s2++;
                }

                i++;
            }

            while (s1 < e1)
            {
                Console.WriteLine($"1) {s1} < {e1}");
                temp[i] = nums[s1];
                s1++;
                i++;
            }

            while (s2 < e2)
            {
                Console.WriteLine($"2) {s2} < {e2}");
                temp[i] = nums[s2];
                s2++;
                i++;
            }

            for(i = 0; i < len; i++)
            {
                nums[i + start] = temp[i];
            }
        }

        public static void MergeSort<T>(T[] elements, Func<T, T, bool> sortFunc)
        {

        }
    }
}
