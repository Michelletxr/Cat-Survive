using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    public float speed = 0.2f;

    public int maxDistance = 5;

    public Vector2 wayToPoint;

    void Start()
    {
        ChooseMoveDirection();
    }
 
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayToPoint, 
        speed * Time.deltaTime);
    }
 
    void ChooseMoveDirection()
    {
       wayToPoint = new Vector2(Random.Range(-1, 5), Random.Range(-1, 5));
    }
}
