namespace CSharpAdvance
{
    using System;

    public class Solution
    {
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
