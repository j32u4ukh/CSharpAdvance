namespace CSharpAdvance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Node
    {
        public Node root = null;
        public string Name { get; private set; }
        public List<Node> nodes;

        public Node(Node root, string name) 
        {
            this.root = root;
            Name = name;
            nodes = new List<Node>();
        }

        public void AddNode(string name)
        {
            nodes.Add(new Node(this, name));
        }

        public void AddNode(Node node)
        {
            node.root = this;
            nodes.Add(node);
        }

        public override string ToString()
        {
            List<Node> nodes = new List<Node>() { this };
            Node node = this.root;
            while(node != null)
            {
                nodes.Insert(0, node);
                node = node.root;
            }
            StringBuilder sb = new StringBuilder();
            int i, len = nodes.Count;
            sb.Append("/");
            if (len > 1)
            {
                node = nodes[1];
                sb.Append($"{node.Name}");

                for (i = 2; i < len; i++)
                {
                    node = nodes[i];
                    sb.Append($"/{node.Name}");
                }
            }
            return sb.ToString();
        }
    }

    public class Solution
    {
        [Test("/home/", "/home")]
        [Test("/../", "/")]
        [Test("/..", "/")]
        [Test("/home//foo/", "/home/foo")]
        [Test("/.../hr", "/.../hr")]
        [Test("/home/a/../b", "/home/b")]
        [Test("/a/./b/../../c/", "/c")]
        [Test("/...", "/...")]
        [Test("/.", "/")]
        [Test("/a/../../b/../c//.//", "/c")]
        [Test("/a//b////c/d//././/..", "/a/b/c")]
        [Test("/..hidden", "/..hidden")]
        [Test("/hello../world", "/hello../world")]
        [Test("/home/../../..", "/")]
        [Test("/home/of/foo/../../bar/../../is/./here/.", "/is/here")]
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
