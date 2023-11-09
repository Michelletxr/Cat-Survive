public class A_Star{
    Node[] OPEN = [];
    Node[] CLOSED= [];
    Node star;
    Node go;
    public int calculate_heuristic(Node node){

    }

    public Start(){
        star.g_dist = 0;
        star.h_dist = calculate_heuristic(star);
        OPEN[0] = star;
        while(OPEN.length != 0){

        }

    }
}