using System;
using System.Collections.Generic;

public class Node {
    public double g_dist { get; set; }
    public double h_dist { get; set; }
    public double f_dist { get; set; }
    public Node parent { get; set; }
    public int y { get; set; }
    public int x { get; set; }

    public Node(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public override string ToString() {
        return $"({x}, {y})";
    }

    public override bool Equals(object obj) {
        if (obj is Node _node) {
            return this.x == _node.x && this.y == _node.y;
        }
        return false;
    }

     public override int GetHashCode() {
        return 0;
       // return x.GetHashCode() ^ y.GetHashCode();
    }

}


class NodeComparer : IComparer<Node>
{
    public int Compare(Node node1, Node node2)
    {
        if (node1.x != node2.x)
        {
            return node1.x.CompareTo(node2.x); // Compara a propriedade X
        }
        else
        {
            return node1.y.CompareTo(node2.y); // Se X for igual, compara a propriedade Y
        }
    }
}




