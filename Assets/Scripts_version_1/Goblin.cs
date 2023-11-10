//implementar primeiro o comportamento do goblin
//montar cenario de labirinto
using UnityEngine;
public class Goblin_v1 : MonoBehaviour{

        //definir as condições
        public bool isPlayerAround(){
            return posPlayer.gameObject != null;

        }

        public bool isLifeZero(){
            //return life == 0;
            return false;
        }

        public bool isPlayerOnCollisionEnter2D(){
            //int distancePositions = this.transform.position - this.posPlayer.position;
            //return distancePositions <= 1;
            return false;
        }


        public Rigidbody2D rb;
        public Transform posPlayer { get; set; }
        public int life = 100;

        public GameObject gameObject { get; set; }


        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            posPlayer = GameObject.FindWithTag("player").transform;
        }

        void Update()
        {
            
            
    }


}
