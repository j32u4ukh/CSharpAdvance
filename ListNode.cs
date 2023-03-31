namespace CSharpAdvance
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }

        public static ListNode NewListNode(int[] numbers)
        {
            ListNode node = new ListNode(numbers[0]);
            int i, len = numbers.Length;
            for (i = 1; i < len; i++)
            {
                node.Add(numbers[i]);
            }
            return node;
        }

        public ListNode Add(int val)
        {
            ListNode node = this;

            while (node.next != null)
            {
                node = node.next;
            }

            node.next = new ListNode(val);
            return node.next;
        }

        public override string ToString()
        {
            string info = $"ListNode({val}";
            ListNode node = this;

            while (node.next != null)
            {
                node = node.next;
                info += $" -> {node.val}";
            }

            info += ")";
            return info;
        }
    }

}
