using System;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class MazeGeneratorStatic : MonoBehaviour
{
    public List<Tuple<int, int>> paths_pos; //posições dos blocos de path
    public List<Tuple<int, int>> item_pos; //posições dos blocos de items
    public List<Node> path_nodes { get; set; } // lista de nos que representa o caminho de inicio - fim
    public List<Node> graph_nodes; //lista auxiliar com todos os nos do grafo
    public Dictionary<(int, int), List<((int, int), double)>> graph;
    public bool scenarioIsInit = false;
    public GameObject wall;
    public GameObject path;
    public GameObject pathFind;
    public GameObject item;
    public GameObject door;
    public GameObject enemy;


    private void InitializeMazeStatic(int[,] maze, int rows, int columns){

        paths_pos = new List<Tuple<int, int>>();
        graph_nodes = new List<Node>();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (maze[i, j] == 1)
                {
                    GameObject obj = Instantiate(wall, new Vector2(i, j), Quaternion.identity);
                    obj.name = $"Wall {i} {j}";
                }
                else if (maze[i, j] == 0)
                {
                    GameObject obj = Instantiate(path, new Vector2(i, j), Quaternion.identity);
                    obj.name =  $"Path {i} {j}";
                    paths_pos.Add(new Tuple<int, int>(i, j));
                    graph_nodes.Add(new Node(i,j));
                    
                }
            }
        }

    }

    private void GeneratePointsStartEnd() {
        GameObject start = Instantiate(door, new Vector2(0,0), Quaternion.identity); // Define os ponto de inicio
        GameObject end = Instantiate(door, new Vector2(10, 9), Quaternion.identity); // Define os ponto de fim 
        start.name = "start";
        end.name = "end";
    }

    private void GenerateRandomItems(int totalItems){
        item_pos = new List<Tuple<int, int>>();

        for (int i = 0; i < totalItems; i++)
        {
            int index = UnityEngine.Random.Range(0, paths_pos.Count);
            int x = paths_pos[index].Item1;
            int y = paths_pos[index].Item2;

            GameObject obj = Instantiate(item, new Vector2(x,y), Quaternion.identity); // Define os pontos como caminhos no labirinto
            obj.name = $"item {x} {y}";
            obj.tag = "item";
            item_pos.Add(new Tuple<int, int>(x, y));
        }

    }

     private void GenerateRandomEnemy(){

        bool validPos = false;
        int x = 0;
        int y = 0;

        while(!validPos) {
            int index = UnityEngine.Random.Range(0, paths_pos.Count);
            x = paths_pos[index].Item1;
            y = paths_pos[index].Item2;
            
            if(!path_nodes.Contains(new Node(x,y))){
                validPos = true;
            }
        }

        GameObject obj = Instantiate(enemy, new Vector2(x,y), Quaternion.identity); // Define os pontos como caminhos no labirinto
        obj.name = $"enemy";
        obj.tag = "enemy";
    }


    private void GeneratePath(List<Node> path){

        foreach (Node node in path) {
            GameObject obj = Instantiate(pathFind, new Vector2(node.x, node.y), Quaternion.identity); // Define os pontos como caminhos no labirinto
            obj.name = $"PathFind {node.x} {node.y}";
        }

        this.scenarioIsInit = true;
    }

    private void DisablePrefabs(){
        GameObject.Find("wall").SetActive(false);
        GameObject.Find("path").SetActive(false);
        GameObject.Find("pathFind").SetActive(false);
        GameObject.Find("door").SetActive(false);
        GameObject.Find("item").SetActive(false);
    }

    void Start()
    {
        int rows = 11; // número de linhas
        int columns = 10; // número de colunas
        int totalItems = 4; // número de caminhos a serem gerados

        int[,] maze = 
        {
            {0, 1, 0, 0, 0, 0, 0, 0, 0, 1},
            {0, 1, 1, 1, 1, 0, 1, 1, 0, 1},
            {0, 0, 0, 0, 1, 0, 1, 0, 0, 1},
            {0, 1, 1, 1, 1, 0, 0, 1, 0, 0},
            {0, 1, 1, 1, 1, 1, 0, 1, 1, 0},
            {0, 0, 1, 1, 0, 0, 0, 1, 0, 0},
            {1, 0, 0, 0, 0, 1, 1, 1, 0, 1},
            {1, 1, 1, 0, 1, 0, 0, 1, 0, 0},
            {0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
            {0, 0, 1, 0, 1, 0, 0, 1, 0, 0},
            {0, 0, 1, 0, 1, 0, 0, 1, 0, 0}
        };
    
        InitializeMazeStatic(maze, rows, columns);
        GeneratePointsStartEnd();
        GenerateRandomItems(totalItems);

        GraphGenerator graphGenerator = new GraphGenerator();
        this.graph = graphGenerator.CreatGraph(graph_nodes);
        //graphGenerator.PrintGraph(graph);

        A_Star a_Star = new A_Star();
        path_nodes = a_Star.FindPath(graph, new Node(0,0), new Node(10, 9));
    
       // GeneratePath(path_nodes);
        GenerateRandomEnemy();
       // DisablePrefabs();
       
    }

    void Update(){}
}


