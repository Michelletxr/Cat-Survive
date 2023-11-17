using System;
using System.Collections.Generic;
using UnityEngine;

public class MainMaze :  MonoBehaviour {
    public List<Tuple<int, int>> itemsPoints;
    public Dictionary<(int, int), List<((int, int), double)>> graphGenerate;
    private MazeGeneratorStatic mazeGenerator;

    void Start() {
        GameObject maze = GameObject.FindGameObjectWithTag("maze");
        if (maze != null){
           this.mazeGenerator = maze.GetComponent<MazeGeneratorStatic>();
        }
    }


    void Update() {
        
        if (mazeGenerator != null && itemsPoints == null) {
            this.graphGenerate = mazeGenerator.graph;
            this.itemsPoints = mazeGenerator.item_pos;
        }
    }
}