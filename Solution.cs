namespace CSharpAdvance
{
    using System;
    using System.Collections.Generic;

    public class Solution
    {
        [Test(new int[] { -1, 0, 3, 5, 9, 12 }, 9, 4)]
        [Test(new int[] { -1, 0, 3, 5, 9, 12 }, 2, -1)]
        [Test(new int[] { 5 }, -5, -1)]
        [Test(new int[] { 4 }, 4, 0)]
        public int Search(int[] nums, int target)
        {
            //return search(nums, target, 0, nums.Length);
            return BinarySearch(nums, target);
        }

        int BinarySearch(int[] arr, int target)
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

        private int search(int[] nums, int target, int left, int right)
        {
            if(left < right)
            {
                if (left == right - 1)
                {
                    if (nums[left] == target)
                    {
                        return left;
                    }
                }
                else
                {
                    int mid = (left + right) / 2, value = nums[mid];

                    if (value == target)
                    {
                        return mid;
                    }
                    else if (value < target)
                    {
                        return search(nums, target, mid + 1, right);
                    }
                    else
                    {
                        return search(nums, target, 0, mid);
                    }
                }
            }

            return -1;
        }

        [Test("PAYPALISHIRING", 3, "PAHNAPLSIIGYIR")]
        [Test("PAYPALISHIRING", 4, "PINALSIGYAHRPI")]
        [Test("A", 1, "A")]
        [Test("AB", 1, "AB")]
        [Test("ABC", 1, "ABC")]
        [Test("ABCDEF", 1, "ABCDEF")]
        [Test("ABCD", 2, "ACBD")]
        [Test("ABCDEF", 2, "ACEBDF")]
        public string Convert(string s, int numRows)
        {
            int i = 0, len = s.Length, row = 0, col = 0, state = 0;
            int nCol = GetColumnNumber(length: len, nRow: numRows);
            char[,] results = new char[numRows, nCol];
            while(i < len)
            {
                results[row, col] = s[i];

                switch (state)
                {
                    // down-ward
                    case 0:
                        if (row + 1 >= numRows)
                        {
                            if(row >= 1)
                            {
                                row--;
                            }

                            col++;
                            state = 1;
                        }
                        else
                        {
                            row++;
                        }
                        break;
                    // right-ward & up-ward
                    case 1:
                        if(row == 0)
                        {
                            if (row + 1 >= numRows)
                            {
                                if (row >= 1)
                                {
                                    row--;
                                }

                                col++;
                                state = 1;
                            }
                            else
                            {
                                state = 0;
                                row++;
                            }
                        }
                        else
                        {
                            row--;
                            col++;
                        }
                        break;
                }
                i++;
            }
            string result = "";
            for(row = 0; row < numRows; row++)
            {
                for (col = 0; col < nCol; col++)
                {
                    if(results[row, col] != default(char))
                    {
                        result += results[row, col];
                    }
                }
            }
            return result;
        }

        public int GetColumnNumber(int length, int nRow)
        {
            if(nRow == 1)
            {
                return length;
            }
            else if(nRow == 2)
            {
                if((length&1) == 1)
                {
                    return length / 2 + 1;
                }
                else
                {
                    return length / 2;
                }
            }

            int groupSize = nRow + (nRow - 2);
            int nColumn = (length / groupSize) * (1 + nRow - 2);
            length %= groupSize;

            if (length < nRow)
            {
                nColumn++;
            }
            else
            {
                nColumn += (1 + length - nRow);
            }
            return nColumn;
        }
    
    }
}
