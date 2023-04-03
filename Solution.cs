namespace CSharpAdvance
{
    using System;
    using System.Collections.Generic;

    public class Solution
    {
        //[Test(new int[] { 1, 2 }, 3, 1)]
        //[Test(new int[] { 3, 2, 2, 1 }, 3, 3)]
        //[Test(new int[] { 3, 5, 3, 4 }, 5, 4)]
        public int NumRescueBoats(int[] people, int limit)
        {
            int len = people.Length;
            //mergeSort(people, 0, len);
            Array.Sort(people);
            int i = 0, j = len - 1, count = 0;
            while(i <= j)
            {
                count++;

                if (i == j)
                {
                    break;
                }

                if(people[i] + people[j] <= limit)
                {
                    i++;
                    j--;
                }
                else
                {
                    j--;
                }
            }
            return count;
        }

        [Test(new int[] { 8, 6, 1, 4, 7 }, new int[] { 1, 4, 6, 7, 8 })]
        [Test(new int[] { 1, 0 }, new int[] { 0, 1 })]
        public int[] MergeSort(int[] nums)
        {
            mergeSort(nums, 0, nums.Length);
            return nums;
        }

        private void mergeSort(int[] nums, int left, int right)
        {
            if (left < right)
            {
                // len = 1
                if (left == right - 1)
                {
                    return;
                }
                else
                {
                    int mid = (left + right) / 2;
                    mergeSort(nums, left, mid);
                    mergeSort(nums, mid, right);
                    merge(nums, left, mid, right);
                }
            }
        }

        public void merge(int[] nums, int s1, int mid, int e2)
        {
            int i = 0, start = s1, s2 = mid, len = e2 - s1;
            int[] temp = new int[len];

            while ((s1 < mid) && (s2 < e2))
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

            while (s1 < mid)
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

        
    }
}
