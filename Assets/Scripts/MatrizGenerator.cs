using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
public class MatrizGenerator : MonoBehaviour
{
    //cada posiçao da matriz é um rato que deve ser coletado
    public Vector3[,] matriz;
    public int numMouses;
    public Coordinate[] mousesPos;
    public GameObject square;
    public Transform cameraTransform;

    public int width, height = 3;

     void Start()
    {
        matriz = new Vector3[10, 10];
        this.InitializeMap();
    }


    public void InitializeMap(){
        LoangindGrid();
        Debug.Log("inicializando Grid");
    }

    public void LoangindGrid(){
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject obj = Instantiate(square, new Vector2(i,j), Quaternion.identity);
                obj.name = $"Square {i} {j}";
                
            }
        }

        cameraTransform.position = new Vector3((float) width/2 - 0.5f, (float) height/2 - 0.5f, -10);
    }


    private int GenerateRandom(int init, int end)
    {
        return Random.Range(init, end);
    }


    //dropar ratos de forma procedural no mapa
    public void FindMouses(){
        //busca por largura
    }

}