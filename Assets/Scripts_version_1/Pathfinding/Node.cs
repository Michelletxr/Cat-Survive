public class Node{
    public int g_dist { get; set; }
    public int h_dist { get; set; }
    public int f_dist { get; set; }
    public Node parent { get; set; }
    public int y { get;}
    public int x { get; }

    public Node(int x, int y){
        x = x;
        y = y;
    }

    public override bool Equals(object obj){
        if (obj is Node _node){
            return this.x == _node.x && this.y == _node.y;
        }
        return false;
    }

     public override int GetHashCode(){
        return 0;
       // return x.GetHashCode() ^ y.GetHashCode();
    }

}