namespace CSharpAdvance
{
    using System;
    using System.Collections.Generic;

    public class Solution
    {

        public int[] CountProducts(int[] spells, int[] potions, long success)
        {
            int len = spells.Length;
            int[] results = new int[len];
            Array.Sort(potions);

            for (int i = 0; i < len; i++)
            {
                if (spells[i] == 0) continue;

                long quotient = success / (long)spells[i];
                int index = BinarySearch(potions, quotient);

                if (index < 0)
                {
                    index = ~index;
                }

                results[i] = potions.Length - index;
            }

            return results;
        }


        [Test(new int[] { 5, 1, 3 }, new int[] { 1, 2, 3, 4, 5 }, 7, new int[] { 4, 0, 3 })]
        [Test(new int[] { 3, 1, 2 }, new int[] { 8, 5, 8 }, 16, new int[] { 2, 0, 2 })]
        public int[] SuccessfulPairs(int[] spells, int[] potions, long success)
        {
            int i, idx, len = spells.Length, nPosition = potions.Length;
            long spell, value;
            int[] results = new int[len];
            mergeSort(potions, 0, nPosition);

            for (i = 0; i < len; i++)
            {
                spell = spells[i];
                value = success / spell;

                if (success % spell != 0)
                {
                    value++;
                }

                results[i] = CountGreaterThanTarget(potions, value);
            }
            return results;
        }

        private void mergeSort(int[] nums, int start, int end)
        {
            // len = 1
            if (start == end - 1)
            {
                return;
            }

            // len = 2
            else if (start == end - 2)
            {
                end--;

                if (nums[start] > nums[end])
                {
                    int temp = nums[start];
                    nums[start] = nums[end];
                    nums[end] = temp;
                }
            }
            else
            {
                int mid = (start + end) / 2;
                mergeSort(nums, start, mid);
                mergeSort(nums, mid, end);
                merge(nums, start, mid, mid, end);
            }
        }

        public void merge(int[] nums, int s1, int e1, int s2, int e2)
        {
            int i = 0, start = s1, len = e2 - s1;
            int[] temp = new int[len];

            while ((s1 < e1) && (s2 < e2))
            {
                if (nums[s1] < nums[s2])
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
                temp[i] = nums[s1];
                s1++;
                i++;
            }

            while (s2 < e2)
            {
                temp[i] = nums[s2];
                s2++;
                i++;
            }

            for (i = 0; i < len; i++)
            {
                nums[i + start] = temp[i];
            }
        }


        public int CountGreaterThanTarget(int[] arr, long target)
        {
            if (arr == null || arr.Length == 0)
            {
                return 0;
            }

            int left = 0;
            int right = arr.Length - 1;
            int mid;

            while (left <= right)
            {
                mid = left + (right - left) / 2;

                if (arr[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return arr.Length - left;
        }

        [Test(new int[] { 1, 2, 3 }, 0, 0)]
        [Test(new int[] { 1, 2, 3 }, 1, 0)]
        [Test(new int[] { 1, 2, 3 }, 2, 1)]
        [Test(new int[] { 1, 2, 3 }, 3, 2)]
        [Test(new int[] { 1, 2, 3 }, 4, 3)]
        public int BinarySearch(int[] arr, long target)
        {
            int left = 0;
            int right = arr.Length - 1;

            while (left <= right)
            {
                // 為避免 (left + right) 溢位
                int mid = left + (right - left) / 2;

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
                    right = mid - 1;
                }
            }

            // 如果目標元素不在數列中，則返回其應該插入的位置
            return left;
        }
    }
}
