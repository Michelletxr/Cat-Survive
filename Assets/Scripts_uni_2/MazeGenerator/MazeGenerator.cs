using System;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class MazeGeneratorRandomPaths : MonoBehaviour
{
    private int rows;
    private int columns;
    List<Tuple<int, int>> points;
    public GameObject[,] matriz;
    public GameObject wall;
    public GameObject path;

    public GameObject door;
    public char[,] maze;
    
    private void InitializeMaze()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject obj = Instantiate(wall, new Vector2(i,j), Quaternion.identity);
                Debug.Log(obj.transform.position);
                obj.name = $"wall {i} {j}"; // Preenche todo o labirinto com paredes
            }
        }
    }

    public void GeneratePaths(int numPaths)
    {
        points = GenerateRandomPoints(numPaths);

        for (int i = 0; i < points.Count - 1; i++)
        {
            Tuple<int, int> start = points[i];
            Tuple<int, int> end = points[i + 1];
            ConnectPoints(start, end);
        }
    }

    private List<Tuple<int, int>> GenerateRandomPoints(int numPoints)
    {
        var points = new List<Tuple<int, int>>();

        for (int i = 0; i < numPoints; i++)
        {
            int x = UnityEngine.Random.Range(0, rows);
            int y = UnityEngine.Random.Range(0, columns);
            points.Add(new Tuple<int, int>(x, y));

            GameObject obj = Instantiate(path, new Vector2(x,y), Quaternion.identity); // Define os pontos como caminhos no labirinto
            obj.name = $"path {x} {y}";
            matriz[x, y] = obj;
        }

        return points;
    }

    private void GenerateRandomPointsStartEnd(){

        int index = UnityEngine.Random.Range(0, points.Count);
        int x = points[index].Item1;
        int y = points[index].Item2;
        GameObject obj = Instantiate(door, new Vector2(x,y), Quaternion.identity); // Define os pontos como caminhos no labirinto
        obj.name = $"door {x} {y}";
        matriz[x, y] = obj;


    }

    private void ConnectPoints(Tuple<int, int> start, Tuple<int, int> end)
    {
        int x = start.Item1;
        int y = start.Item2;

        while (x != end.Item1 || y != end.Item2)
        {
            maze[x, y] = ' '; // Define os caminhos entre os pontos
            GameObject obj = Instantiate(path, new Vector2(x,y), Quaternion.identity);

            if (x < end.Item1)
                x++;
            else if (x > end.Item1)
                x--;
            else if (y < end.Item2)
                y++;
            else if (y > end.Item2)
                y--;
        }
    }

    void Start()
    {
        int rows = 8; // número de linhas
        int columns = 8; // número de colunas
        int numPaths = 10; // número de caminhos a serem gerados
        this.rows = rows;
        this.columns = columns;
        maze = new char[rows, columns];
        matriz = new GameObject[numPaths, numPaths];
        
        //InitializeMaze();
        //GeneratePaths(numPaths);
        //GenerateRandomPointsStartEnd();
        //GenerateRandomPointsStartEnd();
    }
}


