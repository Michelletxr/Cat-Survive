using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public GameObject rat = new GameObject();
    void Start()
    {
        GameObject obj = Instantiate(rat, new Vector2(-1,1), Quaternion.identity);
        int index = GenerateRandom(1,100);
        obj.name = $"Rat {index}";
    }

    void Update()
    {
        
    }

    private int GenerateRandom(int init, int end)
    {
        return Random.Range(init, end);
    }
}
