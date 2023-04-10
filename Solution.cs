namespace CSharpAdvance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Solution
    {
        //[Test("()", true)]
        //[Test("()[]{}", true)]
        //[Test("(]", false)]
        [Test("([]){}", true)]
        [Test("(){}", true)]
        [Test("({})", true)]
        [Test("(", false)]
        public bool IsValid(string s)
        {
            int len = s.Length;
            if(len == 0)
            {
                return true;
            }
            // 若 s 長度為奇數，則返回 false
            else if ((len & 1) == 1)
            {
                return false;
            }
            char left = s[0], right;
            switch(left)
            {
                case '(':
                    right = ')';
                    break;
                case '[':
                    right = ']';
                    break;
                case '{':
                    right = '}';
                    break;
                default:
                    return false;
            }
            int i, count = 1;
            for(i = 1; i < len; i++)
            {
                if(s[i] == left)
                {
                    count++;
                }
                else if(s[i] == right)
                {
                    count--;

                    if(count == 0)
                    {
                        break;
                    }
                }
            }

            if(count != 0)
            {
                return false;
            }

            if(i == 1)
            {
                if (i + 1 < len)
                {
                    return true && IsValid(s.Substring(i + 1, len - 2));
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (i + 1 < len)
                {
                    return true && IsValid(s.Substring(1, i - 1)) && IsValid(s.Substring(i + 1, len - i - 1));
                }
                else
                {
                    return true && IsValid(s.Substring(1, len - 2));
                }
            }
        }

        //[Test(new int[] { 8, 6, 1, 4, 7 }, new int[] { 1, 4, 6, 7, 8 })]
        //[Test(new int[] { 1, 0 }, new int[] { 0, 1 })]
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
