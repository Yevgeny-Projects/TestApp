using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CopyTreeConsole
{
    public static class QueueMapper
    {
        public static TreeB Map(TreeA tree)
        {
            List<(int, string, object)> a = new List<(int, string, object)>();
            a.Add((1, "5", 1));

            if (tree == null) return null;
            var mapQueue = new Queue<(TreeA, TreeB)>();
            var copy = new TreeB();
            mapQueue.Enqueue((tree, copy));

            while (mapQueue.TryDequeue(out var nodes))
            {
                var (nodeA, nodeB) = nodes;
                nodeB.Payload = nodeA.Payload;
                
                if (nodeA.LeftBranch !=null)
                {
                    nodeB.LeftBranch = new TreeB();
                    mapQueue.Enqueue((nodeA.LeftBranch, nodeB.LeftBranch));
                }
                if (nodeA.RightBranch != null)
                {
                    nodeB.RightBranch = new TreeB();
                    mapQueue.Enqueue((nodeA.RightBranch, nodeB.RightBranch));
                }
            }
            return copy;
        }
    }

    public class TreeA
    {
        public string Payload;
        public TreeA LeftBranch;
        public TreeA RightBranch;
    }

    public class TreeB
    {
        public string Payload;
        public TreeB LeftBranch;
        public TreeB RightBranch;
    }
}
