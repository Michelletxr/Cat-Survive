using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgroud : MonoBehaviour
{
    // Start is called before the first frame update
     private Vector3 scale;


    void Start()
    {
        scale = transform.localScale;
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;
       // transform.localScale = new Vector3(width, height, scale.z);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
