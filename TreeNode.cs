using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSharpAdvance
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }

        public override string ToString()
        {
            List<int?> values = new List<int?>();
            List<TreeNode> nodes = new List<TreeNode>() { this };
            TreeNode node;
            int i = 0;
            while(i < nodes.Count)
            {
                node = nodes[i];

                if(node == null)
                {
                    values.Add(null);
                }
                else
                {
                    values.Add(node.val);

                    if(node.left != null || node.right != null)
                    {
                        nodes.Add(node.left);
                        nodes.Add(node.right);
                    }
                }

                i++;
            }
            return JsonConvert.SerializeObject(values);
        }

        public static TreeNode NewTreeNodes(int?[] numbers)
        {
            TreeNode root = new TreeNode((int)numbers[0]);
            List<TreeNode> nodes = new List<TreeNode>() { root };
            int i, len = numbers.Length, idx = 0;
            for (i = 1; i < len; i += 2)
            {
                if (numbers[i] != null)
                {
                    nodes[idx].left = new TreeNode((int)numbers[i]);
                    nodes.Add(nodes[idx].left);
                }

                if ((i + 1 < len) && numbers[i + 1] != null)
                {
                    nodes[idx].right = new TreeNode((int)numbers[i + 1]);
                    nodes.Add(nodes[idx].right);
                }

                idx++;
            }
            return root;
        }

        public static IList<int> PreorderTraversal(TreeNode root)
        {
            List<int> numbers = new List<int>();

            if (root != null)
            {
                numbers.Add(root.val);

                if (root.left != null)
                {
                    IList<int> left = PreorderTraversal(root.left);
                    foreach (int val in left)
                    {
                        numbers.Add(val);
                    }
                }

                if (root.right != null)
                {
                    IList<int> right = PreorderTraversal(root.right);
                    foreach (int val in right)
                    {
                        numbers.Add(val);
                    }
                }
            }

            return numbers;
        }
    }

}
