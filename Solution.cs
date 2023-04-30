using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvance
{
    public class DSU
    {
        private int[] parents;
        private int[] ranks;

        public DSU(int n)
        {
            parents = new int[n];
            ranks = new int[n];

            for(int i = 0; i < n; i++)
            {
                parents[i] = i;
            }
        }

        public int Find(int i)
        {
            if(parents[i] == i)
            {
                return i;
            }

            parents[i] = Find(parents[i]);
            return parents[i];
        }

        public bool Union(int i, int j)
        {
            int pi = Find(i), pj = Find(j);

            if(pi == pj)
            {
                return false;
            }

            if(ranks[pi] < ranks[pj])
            {
                parents[pi] = pj;
            }
            else
            {
                parents[pj] = pi;

                if(ranks[pi] == ranks[pj])
                {
                    ranks[pi]++;
                }
            }

            return true;
        }
    }

    public class Solution
    {

        [Test(4, "[[3,1,2], [3,3,4], [1,1,3],[2,2,4]]", 0)]
        [Test(4, "[[3,1,2],[3,2,3],[1,1,3],[1,2,4],[1,1,2],[2,3,4]]", 2)]
        [Test(4, "[[3,1,2],[3,2,3],[1,1,4],[2,1,4]]", 0)]
        [Test(4, "[[3,2,3],[1,1,2],[2,3,4]]", -1)]
        [Test(13, "[[1,1,2],[2,1,3],[3,2,4],[3,2,5],[1,2,6],[3,6,7],[3,7,8],[3,6,9],[3,4,10],[2,3,11],[1,5,12],[3,3,13],[2,1,10],[2,6,11],[3,5,13],[1,9,12],[1,6,8],[3,6,13],[2,1,4],[1,1,13],[2,9,10],[2,1,6],[2,10,13],[2,2,9],[3,4,12],[2,4,7],[1,1,10],[1,3,7],[1,7,11],[3,3,12],[2,4,8],[3,8,9],[1,9,13],[2,4,10],[1,6,9],[3,10,13],[1,7,10],[1,1,11],[2,4,9],[3,5,11],[3,2,6],[2,1,5],[2,5,11],[2,1,7],[2,3,8],[2,8,9],[3,4,13],[3,3,8],[3,3,11],[2,9,11],[3,1,8],[2,1,8],[3,8,13],[2,10,11],[3,1,5],[1,10,11],[1,7,12],[2,3,5],[3,1,13],[2,4,11],[2,3,9],[2,6,9],[2,1,13],[3,1,12],[2,7,8],[2,5,6],[3,1,9],[1,5,10],[3,2,13],[2,3,6],[2,2,10],[3,4,11],[1,4,13],[3,5,10],[1,4,10],[1,1,8],[3,3,4],[2,4,6],[2,7,11],[2,7,10],[2,3,12],[3,7,11],[3,9,10],[2,11,13],[1,1,12],[2,10,12],[1,7,13],[1,4,11],[2,4,5],[1,3,10],[2,12,13],[3,3,10],[1,6,12],[3,6,10],[1,3,4],[2,7,9],[1,3,11],[2,2,8],[1,2,8],[1,11,13],[1,2,13],[2,2,6],[1,4,6],[1,6,11],[3,1,2],[1,1,3],[2,11,12],[3,2,11],[1,9,10],[2,6,12],[3,1,7],[1,4,9],[1,10,12],[2,6,13],[2,2,12],[2,1,11],[2,5,9],[1,3,8],[1,7,8],[1,2,12],[1,5,11],[2,7,12],[3,1,11],[3,9,12],[3,2,9],[3,10,11]]", 114)]
        public int MaxNumEdgesToRemove_(int n, string edgeString)
        {
            int[][] edges = JsonConvert.DeserializeObject<int[][]>(edgeString);
            return MaxNumEdgesToRemove(n, edges);
        }

        public class DSU
        {
            private int[] parents;
            private int[] ranks;
            private int n;

            public DSU(int n)
            {
                this.n = n;
                parents = new int[n + 1];
                ranks = new int[n + 1];

                for (int i = 0; i <= n; i++)
                {
                    parents[i] = i;
                }
            }

            public int Find(int i)
            {
                if (parents[i] == i)
                {
                    return i;
                }

                int parent = Find(parents[i]);

                if(parents[i] != parent)
                {
                    parents[i] = parent;
                }

                return parent;
            }

            public bool Union(int i, int j)
            {
                int pi = Find(i), pj = Find(j);

                if (pi == pj)
                {
                    return false;
                }

                if (ranks[pi] < ranks[pj])
                {
                    parents[pi] = pj;
                }
                else
                {
                    parents[pj] = pi;

                    if (ranks[pi] == ranks[pj])
                    {
                        ranks[pi]++;
                    }
                }

                return true;
            }

            public int GetGroupNum()
            {
                HashSet<int> groups = new HashSet<int>();
                int parent;
                for (int i = 1; i <= n; i++)
                {
                    parent = Find(i);
                    groups.Add(parent);
                }
                return groups.Count;
            }

            public DSU Clone()
            {
                DSU dsu = new DSU(n);

                for (int i = 0; i <= n; i++)
                {
                    dsu.parents[i] = parents[i];
                    dsu.ranks[i] = ranks[i];
                }

                return dsu;
            }

            public Dictionary<int, List<int>> GetGroups()
            {
                Dictionary<int, List<int>> groups = new Dictionary<int, List<int>>();
                int i, group;
                for (i = 1; i <= n; i++)
                {
                    group = parents[i];

                    if (!groups.ContainsKey(group))
                    {
                        groups.Add(group, new List<int>());
                    }

                    groups[group].Add(i);
                }
                return groups;
            }
        }

        public int MaxNumEdgesToRemove(int n, int[][] edges)
        {
            Dictionary<int, List<(int, int)>> edgeMap = new Dictionary<int, List<(int, int)>>()
            {
                {1, new List<(int, int)>() },
                {2, new List<(int, int)>() },
                {3, new List<(int, int)>() },
            };

            DSU dsu = new DSU(n: n);
            int nEdge = 0, len = edges.Length;

            // AB
            foreach (int[] edge in edges)
            {
                (int, int) coord = edge[1] < edge[2] ? (edge[1], edge[2]) : (edge[2], edge[1]);
                edgeMap[edge[0]].Add(coord);

                if (edge[0] == 3)
                {
                    if(dsu.Union(coord.Item1, coord.Item2))
                    {
                        nEdge++;
                    }
                }
            }

            if (dsu.GetGroupNum() == 1)
            {
                return len - nEdge;
            }

            //Console.WriteLine($"#dsu: {dsu.GetGroupNum()}");

            //foreach((int, int) coord in edgeMap[3])
            //{
            //    edgeMap[1].Remove(coord);
            //    edgeMap[2].Remove(coord);
            //}

            DSU dsuA = dsu.Clone();

            foreach ((int, int) coord in edgeMap[1])
            {
                if(dsuA.Union(coord.Item1, coord.Item2))
                {
                    nEdge++;
                }
            }

            if (dsuA.GetGroupNum() != 1)
            {
                //Console.WriteLine($"#dsuA: {dsuA.GetGroupNum()}");
                return -1;
            }

            DSU dsuB = dsu.Clone();

            foreach ((int, int) coord in edgeMap[2])
            {
                if (dsuB.Union(coord.Item1, coord.Item2))
                {
                    nEdge++;
                }
            }

            if (dsuB.GetGroupNum() != 1)
            {
                //Console.WriteLine($"#dsuB: {dsuB.GetGroupNum()}");
                return -1;
            }

            return len - nEdge;
        }

        //[Test("[[1, 2], [3, 4]]", "[[1, 1], [2, 2]]", "[[2, 3], [5, 6]]")]
        public int[][] Add_(string strA, string strB)
        {
            int[][] a = JsonConvert.DeserializeObject<int[][]>(strA);
            int[][] b = JsonConvert.DeserializeObject<int[][]>(strB);
            return Add(a, b);
        }

        public int[][] Add(int[][] a, int[][] b)
        {
            int row, col, ROW = a.Length, COL;
            int[][] result = new int[ROW][];
            for(row = 0; row < ROW; row++)
            {
                COL = a[row].Length;
                result[row] = new int[COL];

                for (col = 0; col < COL; col++)
                {
                    result[row][col] = a[row][col] + b[row][col];
                }
            }
            return result;
        }

        //[Test("/home/", "/home")]
        //[Test("/../", "/")]
        //[Test("/..", "/")]
        //[Test("/home//foo/", "/home/foo")]
        //[Test("/.../hr", "/.../hr")]
        //[Test("/home/a/../b", "/home/b")]
        //[Test("/a/./b/../../c/", "/c")]
        //[Test("/...", "/...")]
        //[Test("/.", "/")]
        //[Test("/a/../../b/../c//.//", "/c")]
        //[Test("/a//b////c/d//././/..", "/a/b/c")]
        //[Test("/..hidden", "/..hidden")]
        //[Test("/hello../world", "/hello../world")]
        //[Test("/home/../../..", "/")]
        //[Test("/home/of/foo/../../bar/../../is/./here/.", "/is/here")]
        public string SimplifyPath(string path)
        {
            List<string> nodes = new List<string>();
            // state | 0: /, 1: ., 2: default
            int i, len = path.Length, state = 0;
            string buffer = "";
            char c;
            for(i = 0; i < len; i++)
            {
                c = path[i];
                switch (state)
                {
                    // '/'
                    case 0:
                        switch (c)
                        {
                            case '/':
                                break;
                            // '/' -> '.'
                            case '.':
                                state = 1;
                                buffer = ".";
                                break;
                            default:
                                state = 2;
                                buffer = c.ToString();
                                break;
                        }
                        break;
                    // '.'
                    case 1:
                        switch (c)
                        {
                            // '.' -> '/'
                            case '/':
                                state = 0;

                                switch (buffer)
                                {
                                    case ".":
                                        break;
                                    case "..":
                                        // 往上一層
                                        nodes = nodes.GetRange(0, Math.Max(nodes.Count - 1, 0));
                                        break;
                                    default:
                                        nodes.Add(buffer);
                                        break;
                                }
                                buffer = "";
                                break;
                            case '.':
                                state = 1;
                                buffer += ".";
                                break;
                            default:
                                state = 2;
                                buffer += c.ToString();
                                break;
                        }
                        break;
                    default:
                        switch (c)
                        {
                            case '/':
                                state = 0;
                                nodes.Add(buffer);
                                buffer = "";
                                break;
                            case '.':
                                state = 1;
                                buffer += ".";
                                break;
                            default:
                                state = 2;
                                buffer += c.ToString();
                                break;
                        }
                        break;
                }
            }
            switch (state)
            {
                // '/'
                case 0:
                    break;
                // '.'
                case 1:
                    switch (buffer)
                    {
                        case ".":
                            break;
                        case "..":
                            // 往上一層
                            nodes = nodes.GetRange(0, Math.Max(nodes.Count - 1, 0));
                            break;
                        default:
                            nodes.Add(buffer);
                            break;
                    }
                    break;
                default:
                    nodes.Add(buffer);
                    break;
            }
            string joinPath = string.Join("/", nodes);
            return $"/{joinPath}";
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
